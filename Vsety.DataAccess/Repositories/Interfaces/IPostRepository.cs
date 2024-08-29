using Vsety.Core.Models;

namespace Vsety.DataAccess.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task AddPost(Guid userId, Post post);
    }
}