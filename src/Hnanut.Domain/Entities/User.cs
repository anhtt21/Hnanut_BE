using Hnanut.Domain.Common;
using Hnanut.Domain.Enums;

namespace Hnanut.Domain.Entities;

public class User : BaseEntity
{
    private readonly List<RefreshToken> _refreshTokens = new();
    private readonly List<Meal> _meals = new();

    private User()
    {
    }

    private User(
        string email,
        string normalizedEmail,
        string passwordHash,
        string fullName,
        UserRole role)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.", nameof(email));

        if (string.IsNullOrWhiteSpace(normalizedEmail))
            throw new ArgumentException("Normalized email is required.", nameof(normalizedEmail));

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash is required.", nameof(passwordHash));

        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name is required.", nameof(fullName));

        Email = email.Trim();
        NormalizedEmail = normalizedEmail.Trim();
        PasswordHash = passwordHash;
        FullName = fullName.Trim();
        Role = role;
        CreatedAt = DateTime.UtcNow;
    }

    public string Email { get; private set; } = string.Empty;
    public string NormalizedEmail { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string FullName { get; private set; } = string.Empty;
    public UserRole Role { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastLoginAt { get; private set; }

    public UserProfile? Profile { get; private set; }

    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();
    public IReadOnlyCollection<Meal> Meals => _meals.AsReadOnly();

    public static User Register(
        string email,
        string normalizedEmail,
        string passwordHash,
        string fullName)
    {
        return new User(
            email,
            normalizedEmail,
            passwordHash,
            fullName,
            UserRole.User);
    }

    public void UpdateProfile(UserProfile profile)
    {
        if (profile.UserId != Id)
            throw new InvalidOperationException("Profile does not belong to this user.");

        Profile = profile;
    }

    public void MarkLoginSuccess()
    {
        LastLoginAt = DateTime.UtcNow;
    }

    public RefreshToken AddRefreshToken(string tokenHash, DateTime expiresAt)
    {
        var refreshToken = RefreshToken.Create(Id, tokenHash, expiresAt);
        _refreshTokens.Add(refreshToken);

        return refreshToken;
    }

    public void ChangeFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name is required.", nameof(fullName));

        FullName = fullName.Trim();
    }

    public void ChangePasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash is required.", nameof(passwordHash));

        PasswordHash = passwordHash;
    }
}