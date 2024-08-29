using Microsoft.EntityFrameworkCore;
using Vsety.DataAccess.Entities;
using Vsety.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Vsety.DataAccess.Repositories.Interfaces;

namespace Vsety.DataAccess.Repositories
{
    public class PersonsRepository : IPersonsRepository
    {
        private readonly ApplicationContext _context;
        private readonly IImageRepository _imageRepository;
        public PersonsRepository(ApplicationContext context, IImageRepository imageRepository)
        {
            _context = context;
            _imageRepository = imageRepository;
        }

        public async Task<PersonEntity?> GetById(Guid id)
        {
            return await _context.Persons
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<PersonEntity?> GetByUserId(Guid id)
        {
            return await _context.Persons
                .FirstOrDefaultAsync(c => c.UserId == id);
        }

        public async Task AddPerson(string userLogin, Person person)
        {
            var user = _context.Users.FirstOrDefault(c => c.Mail == userLogin)
                ?? throw new Exception();

            var id = Guid.NewGuid();
            var Path = "C:/Users/ilyap/source/repos/Vsety/Vsety.APINew/wwwroot/img/" + id + ".jpg";
            var personEntity = new PersonEntity()
            {
                Id = Guid.NewGuid(),
                Name = person.Name,
                Surname = person.Surname,
                Gender = person.Gender,
                City = person.City,
                Birthday = person.Birthday,
                Nickname = person.Nickname,
                Description = person.Description,
                UserId = user.Id,
                User = user,
            };
            var file = await _imageRepository.AddFile(id, person.avatar, Path);
            
            personEntity.Img = file;

            personEntity.Img.PersonId = personEntity.Id;
            personEntity.Img.Person = personEntity;

            

            _context.Update(personEntity.Img);

            user.Person = personEntity;

            _context.Add(personEntity);

            _context.Update(user);

            //_context.ChangeTracker.Clear();
            await _context.SaveChangesAsync();
        }

        public async Task<ImgEntity?> GetFileByIdLogo(Guid id)
        {
            return await _context.Imgs
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Guid> Update(Guid id, string name, string surname, string gender, string city, DateTime birthday, string nick)
        {
            _context.ChangeTracker.Clear();
            await _context.Persons
                .Where(b => b.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.Name, b => name)
                    .SetProperty(b => b.Surname, b => surname)
                    .SetProperty(b => b.Gender, b => gender)
                    .SetProperty(b => b.City, b => city)
                    .SetProperty(b => b.Birthday, b => birthday)
                    .SetProperty(b => b.Nickname, b => nick));

            return id;
        }

        //public Person EntityToPerson(PersonEntity person)
        //{
        //    var personEntity = new PersonEntity()
        //    {
        //        Id = (Guid)person.Id,
        //        Name = person.Name,
        //        Surname = person.Surname,
        //        Gender = person.Gender,
        //        City = person.City,
        //        Birthday = person.Birthday,
        //        Nickname = person.Nickname,
        //        ImgId = person.,
        //    };
        //}
    }
}
