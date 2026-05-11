using Hnanut.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// =======================
// Service registrations
// =======================

// Đăng ký Infrastructure Layer:
// - HnanutDbContext
// - SQL Server connection
// - Các service hạ tầng sau này: Redis, MinIO, JWT, Repository...
builder.Services.AddInfrastructure(builder.Configuration);

// Đăng ký Controller để sau này tạo AuthController, FoodController, MealController...
builder.Services.AddControllers();

// Đăng ký OpenAPI cho môi trường development.
builder.Services.AddOpenApi();

// CORS cho frontend trong lúc dev.
// React Native app chạy trên điện thoại thật thường không bị CORS như browser,
// nhưng nếu bạn test Expo Web hoặc web client thì vẫn cần.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173", // React/Vite nếu có dùng web test
                "http://localhost:8081", // Expo/Metro dev server
                "http://localhost:19006" // Expo web cũ nếu có
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// =======================
// HTTP pipeline
// =======================

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Tạm thời tắt HTTPS redirect trong dev nếu bạn test bằng mobile/emulator.
// Khi deploy staging/production thì bật lại HTTPS.
// app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

// Sau này khi làm Auth ở Day 15-20 sẽ bật thêm:
// app.UseAuthentication();
// app.UseAuthorization();

app.UseAuthorization();

// Endpoint kiểm tra API có chạy không.
app.MapGet("/", () => Results.Ok(new
{
    app = "Hnanut Backend",
    status = "Running",
    environment = app.Environment.EnvironmentName,
    timeUtc = DateTime.UtcNow
}));

// Endpoint health check đơn giản.
// Dùng để test nhanh bằng browser/Postman/GitHub Actions sau này.
app.MapGet("/health", () => Results.Ok(new
{
    status = "Healthy",
    service = "Hnanut.Api",
    timeUtc = DateTime.UtcNow
}));

// Map các controller sau này:
// AuthController, ProfileController, FoodsController, MealsController, MediaController...
app.MapControllers();

app.Run();