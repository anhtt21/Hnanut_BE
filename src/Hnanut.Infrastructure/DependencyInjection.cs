using Hnanut.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hnanut.Infrastructure;

public static class DependencyInjection // Hàm gom toàn bộ đăng ký service của Infrastructure.
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Connection string 'DefaultConnection' is missing.");
        }

        services.AddDbContext<HnanutDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        return services;
    }
}