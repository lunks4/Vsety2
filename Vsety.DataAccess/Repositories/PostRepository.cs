using Google.Protobuf.Compiler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsety.Core.Models;
using Vsety.DataAccess.Entities;
using Vsety.DataAccess.Repositories.Interfaces;

namespace Vsety.DataAccess.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationContext _context;
        private readonly IImageRepository _imageRepository;
        private readonly IPersonsRepository _personsRepository;
        public PostRepository(ApplicationContext context, IImageRepository imageRepository, IPersonsRepository personsRepository)
        {
            _context = context;
            _imageRepository = imageRepository;
            _personsRepository = personsRepository;
        }

        public async Task<PostEntity> AddPost(Guid userId, Post post)
        {
            var user = _context.Users.FirstOrDefault(c => c.Id == userId)
                ?? throw new Exception();

            PersonEntity person = await _personsRepository.GetByUserId(userId);
            ImgEntity img = await _imageRepository.GetByPersonId(person.Id);
            var id = Guid.NewGuid();
            var path = "C:/Users/ilyap/source/repos/Vsety/Vsety.APINew/wwwroot/img/" + id + ".jpg";
            var postEntity = new PostEntity()
            {
                Id = post.Id,
                Time = post.Time,
                Description = post.Description,
      
                UserId = userId,
                User = user,
            };
            
            var file = await _imageRepository.AddFile(id, post.file, path);

            postEntity.Img = file;
            postEntity.ImgId = file.Id;
            //_context.ChangeTracker.Clear();

            postEntity.Img.Post = postEntity;

            user.Posts.Add(postEntity);

            _context.Update(user);
            _context.Update(postEntity.Img);

            _context.Add(postEntity);

            await _context.SaveChangesAsync();

            return postEntity;
        }

        public async Task<PostEntity?> GetById(Guid id)
        {
            return await _context.Posts
               .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<CommentEntity?> AddCommentary(PostEntity post, UserEntity user, string comment)
        {
            Guid id = Guid.NewGuid();
            var commentEntity = new CommentEntity()
            {
                Id = id,
                UserId = user.Id,
                User = user,
                Time = DateTime.Now,
                Comment = comment,
                PostId = post.Id,
                Post = post,
            };

            post.UsersComments.Add(commentEntity);
            post.countComments = post.UsersComments.Count;

            user.Comments.Add(commentEntity);

            _context.Update(user);
            _context.Update(post);

            _context.Add(commentEntity);

            await _context.SaveChangesAsync();

            return commentEntity;
        }

        public async Task<PostEntity?> AddLike(PostEntity post, UserEntity user)
        {

            post.UserLikes.Add(user);
            post.countLikes = post.UserLikes.Count;

            user.PostLikes.Add(post);

            _context.Update(user);
            _context.Update(post);


            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<PostEntity?> AddRepost(PostEntity post, UserEntity user)
        {

            post.UserReposts.Add(user);
            post.countReposts = post.UserReposts.Count;

            user.PostReposts.Add(post);

            _context.Update(user);
            _context.Update(post);


            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<List<PostEntity>> GetAllPosts(int page,int count)
        {
            var posts = await _context.Posts
                                    .OrderByDescending(p => p.Time)
                                    .Skip(page)
                                    .Take(count)
                                    .ToListAsync();

            return posts;
        }

    }
}
