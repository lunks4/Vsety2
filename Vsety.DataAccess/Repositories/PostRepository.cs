using Microsoft.AspNetCore.Http;
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
        public PostRepository(ApplicationContext context, IImageRepository imageRepository)
        {
            _context = context;
            _imageRepository = imageRepository;
        }

        public async Task AddPost(Guid userId, Post post)
        {
            var user = _context.Users.FirstOrDefault(c => c.Id == userId)
                ?? throw new Exception();

            var id = Guid.NewGuid();
            var postEntity = new PostEntity()
            {
                Id = post.Id,
                Time = post.Time,
                Description = post.Description,
                Img = new ImgEntity()
                {
                    Id = id,
                    Path = "C:/Users/ilyap/source/repos/Vsety/Vsety.APINew/wwwroot/img/" + id + ".jpg",
                },
                User = user,
            };
            await _imageRepository.AddFile(postEntity.Img.Id, post.file, postEntity.Img.Path);

            _context.ChangeTracker.Clear();

            _context.Add(postEntity);

            await _context.SaveChangesAsync();
        }
    }
}
