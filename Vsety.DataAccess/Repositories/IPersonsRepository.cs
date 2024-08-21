using Vsety.Core.Models;
using Vsety.DataAccess.Entities;

namespace Vsety.DataAccess.Repositories
{
    public interface IPersonsRepository
    {
        Task AddPerson(string userLogin, Person person);
        Task<Guid> Update(Guid id, string name, string surname, string gender, string city, DateTime birthday, string nick);
        Task<PersonEntity?> GetById(Guid id);
        Task<ImgEntity?> GetFileByIdLogo(Guid id);
    }
}