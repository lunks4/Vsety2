using Microsoft.EntityFrameworkCore;
using Vsety.DataAccess.Entities;
using Vsety.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Vsety.DataAccess.Repositories
{
    public class PersonsRepository : IPersonsRepository
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
        public PersonsRepository(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public async Task<PersonEntity?> GetById(Guid id)
        {
            return await _context.Persons
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddPerson(string userLogin, Person person)
        {
            var user = _context.Users.FirstOrDefault(c => c.Mail == userLogin)
                ?? throw new Exception();

            var personEntity = new PersonEntity()
            {
                Id = Guid.NewGuid(),
                Name = person.Name,
                Surname = person.Surname,
                Gender = person.Gender,
                City = person.City,
                Birthday = person.Birthday,
                Nickname = person.Nickname,
                ImgId = Guid.NewGuid(),
            };
            await AddAvatarFile(personEntity.ImgId, person.avatar, personEntity);
            user.Person = personEntity;
            user.PersonId = personEntity.Id;

            _context.Update(user);

            _context.Add(personEntity);

 

            await _context.SaveChangesAsync();
        }

        public async Task AddAvatarFile(Guid ImgId, IFormFile uploadedFile, PersonEntity person)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/img/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                ImgEntity file = new ImgEntity {Id = ImgId, Name = uploadedFile.FileName, Path = path };
                _context.Imgs.Add(file);
                _context.SaveChanges();
            }

            await _context.SaveChangesAsync();
        }

        public async Task AddPostFile(Guid ImgId, IFormFile uploadedFile, PersonEntity person)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/img/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                ImgEntity file = new ImgEntity { Id = ImgId, Name = uploadedFile.FileName, Path = path };
                _context.Imgs.Add(file);
                _context.SaveChanges();
            }

            await _context.SaveChangesAsync();
        }

        public async Task<ImgEntity?> GetFileByIdLogo(Guid id)
        {
            return await _context.Imgs
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Guid> Update(Guid id, string name, string surname, string gender, string city, DateTime birthday, string nick)
        {
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
