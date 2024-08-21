using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsety.Core.Models;
using Vsety.DataAccess.Repositories;
using Vsety.Infrastructure;

namespace Vsety.Application.Services
{
    public class PersonServices
    {
        private readonly IPersonsRepository personsRepository;

        public PersonServices(IPersonsRepository personsRepository)
        {
            this.personsRepository = personsRepository;
        }

        //public async Task Register(string name, string surname, str)
        //{
        //    var hashedPassword = passwordHasher.Generate(password);

        //    var user = new User(Guid.NewGuid(), mail, hashedPassword);

        //    await usersRepository.Add(user);
        //}
    }
}
