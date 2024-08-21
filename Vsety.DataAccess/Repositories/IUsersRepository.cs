using Vsety.Core.Models;
using Vsety.DataAccess.Entities;

namespace Vsety.DataAccess.Repositories
{
    public interface IUsersRepository
    {
        Task Add(User user);
        Task<UserEntity?> GetByMail(string mail);
        Task<bool> UserExist(string login);
        Task<UserEntity?> GetById(Guid id);
    }
}