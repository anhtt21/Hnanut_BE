using Hnanut.Domain.Common;

namespace Hnanut.Domain.Entities;

public class MealImage : BaseEntity
{
    private static readonly string[] AllowedContentTypes =
    {
        "image/jpeg",
        "image/png",
        "image/webp"
    };

    private const long MaxSizeBytes = 5 * 1024 * 1024;

    private MealImage()
    {
    }

    private MealImage(
        Guid mealId,
        string objectKey,
        string contentType,
        long sizeBytes,
        int? width,
        int? height)
    {
        if (mealId == Guid.Empty)
            throw new ArgumentException("Meal id is required.", nameof(mealId));

        if (string.IsNullOrWhiteSpace(objectKey))
            throw new ArgumentException("Object key is required.", nameof(objectKey));

        if (string.IsNullOrWhiteSpace(contentType))
            throw new ArgumentException("Content type is required.", nameof(contentType));

        if (!AllowedContentTypes.Contains(contentType))
            throw new ArgumentException("Unsupported image content type.", nameof(contentType));

        if (sizeBytes <= 0)
            throw new ArgumentException("Image size must be greater than 0.", nameof(sizeBytes));

        if (sizeBytes > MaxSizeBytes)
            throw new ArgumentException("Image size must be less than or equal to 5MB.", nameof(sizeBytes));

        if (width.HasValue && width <= 0)
            throw new ArgumentException("Image width must be greater than 0.", nameof(width));

        if (height.HasValue && height <= 0)
            throw new ArgumentException("Image height must be greater than 0.", nameof(height));

        MealId = mealId;
        ObjectKey = objectKey.Trim();
        ContentType = contentType.Trim();
        SizeBytes = sizeBytes;
        Width = width;
        Height = height;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid MealId { get; private set; }
    public string ObjectKey { get; private set; } = string.Empty;
    public string ContentType { get; private set; } = string.Empty;
    public long SizeBytes { get; private set; }
    public int? Width { get; private set; }
    public int? Height { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Meal? Meal { get; private set; }

    public static MealImage Create(
        Guid mealId,
        string objectKey,
        string contentType,
        long sizeBytes,
        int? width = null,
        int? height = null)
    {
        return new MealImage(
            mealId,
            objectKey,
            contentType,
            sizeBytes,
            width,
            height);
    }
}