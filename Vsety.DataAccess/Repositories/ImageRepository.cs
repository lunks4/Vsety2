using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsety.DataAccess.Entities;
using Vsety.DataAccess.Repositories.Interfaces;

namespace Vsety.DataAccess.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationContext _context;

        public ImageRepository(ApplicationContext context)
        {
            _context = context;

        }
        public async Task<ImgEntity?> AddFile(Guid ImgId, IFormFile uploadedFile, string path)
        {
            if (uploadedFile != null)
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                ImgEntity file = new ImgEntity { Id = ImgId, Name = ImgId + ".jpg", Path = path };
    
                _context.Imgs.Add(file);
                await _context.SaveChangesAsync();
                return file;
            }

            return null;

        }

        public async Task<ImgEntity?> GetByPersonId(Guid personId)
        {
            return await _context.Imgs
               .FirstOrDefaultAsync(c => c.PersonId == personId);
        }

        public async Task<ImgEntity?> GetById(Guid imgId)
        {
            return await _context.Imgs
               .FirstOrDefaultAsync(c => c.Id == imgId);
        }

        public string GetContentType(string path)
        {
            var types = new Dictionary<string, string>
            {
                {".pdf", "application/pdf"},
                {".txt", "text/plain"},
                {".jpg", "image/jpeg"},
                {".png", "image/png"}
                // Добавьте другие MIME-типы при необходимости
            }; 
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        public async Task DeleteFile(Guid ImgId)
        {

        }

        public async Task UpdateFile(Guid ImgId)
        {

        }
    }
}

