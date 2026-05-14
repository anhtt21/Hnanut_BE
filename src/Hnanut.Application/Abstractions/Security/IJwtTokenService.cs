using Hnanut.Domain.Entities;

namespace Hnanut.Application.Abstractions.Security;

public interface IJwtTokenService
{
    string CreateAccessToken(User user);
}
