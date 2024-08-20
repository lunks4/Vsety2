using Vsety.DataAccess.Entities;

namespace Vsety.Infrastructure
{
    public interface IJwtProvider
    {
        string GenerateToken(UserEntity user);
    }
}