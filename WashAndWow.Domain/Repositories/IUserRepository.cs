using Wash_Wow.Domain.Entities;

namespace Wash_Wow.Domain.Repositories
{
    public interface IUserRepository : IEFRepository<UserEntity, UserEntity>
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
        string GeneratePassword();
    }
}
