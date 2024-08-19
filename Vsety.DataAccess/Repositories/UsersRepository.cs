﻿using Vsety.Core.Models;
using Microsoft.EntityFrameworkCore;
using Vsety.DataAccess.Entities;

namespace Vsety.DataAccess.Repositories
{
    public class UsersRepository
    {
        private readonly ApplicationContext _context;
        public UsersRepository(ApplicationContext context)
        {
            _context = context;
        }

        //public async Task<List<UserEntity>> Get()
        //{
        //    return await _context.Users
        //        .AsNoTracking()
        //        .ToListAsync();
        //}

        //public async Task<List<UserEntity>> GetWithPerson()
        //{
        //    return await _context.Users
        //        .AsNoTracking()
        //        .Include(c => c.Person)
        //        .ToListAsync();
        //}
        //public async Task<UserEntity?> GetById(Guid id)
        //{
        //    return await _context.Users
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(c => c.Id == id);
        //}

        public async Task<UserEntity?> GetByMail(string mail)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Mail == mail) ?? throw new Exception();
        }

        //public async Task Update(Guid id, string password)
        //{
        //    await _context.Users
        //        .Where(c => c.Id == id)
        //        .ExecuteUpdateAsync(s => s
        //            .SetProperty(c => c.PasswordHash, password));
        //}

        //public async Task Delete(Guid id)
        //{
        //    await _context.Users
        //        .Where (c => c.Id == id)
        //        .ExecuteDeleteAsync();
        //}

        public async Task Add(User user)
        {
            var userEntity = new UserEntity()
            {
                Id = user.Id,
                Mail = user.Mail,
                PasswordHash = user.PasswordHash,
                //Person = user.PersonEntity
            };
            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }
    }
}
