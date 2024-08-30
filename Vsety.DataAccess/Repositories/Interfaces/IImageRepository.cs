using Microsoft.AspNetCore.Http;
using Vsety.DataAccess.Entities;

namespace Vsety.DataAccess.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<ImgEntity?> AddFile(Guid ImgId, IFormFile uploadedFile, string path);
        Task DeleteFile(Guid ImgId);
        Task<ImgEntity?> GetById(Guid imgId);
        Task<ImgEntity?> GetByPersonId(Guid personId);
        string GetContentType(string path);
        Task UpdateFile(Guid ImgId);
    }
}