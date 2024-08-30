using Microsoft.AspNetCore.Mvc;
using Vsety.Core.Models;
using Vsety.DataAccess.Entities;

namespace Vsety.DataAccess.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<CommentEntity?> AddCommentary(PostEntity post, UserEntity user, string comment);
        Task<PostEntity?> AddLike(PostEntity post, UserEntity user);
        Task<PostEntity> AddPost(Guid userId, Post post);
        Task<PostEntity?> AddRepost(PostEntity post, UserEntity user);
        Task<List<PostEntity>> GetAllPosts(int count);
        Task<PostEntity?> GetById(Guid id);
    }
}