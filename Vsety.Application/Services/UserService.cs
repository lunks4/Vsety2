using Vsety.Infrastructure;
using Vsety.Core.Models;
using Vsety.DataAccess.Repositories;

namespace Vsety.Application.Services
{
    public class UserService
    {
        private readonly PasswordHasher passwordHasher;
        private readonly UsersRepository usersRepository;
        private readonly JwtProvider jwtProvider;

        public UserService(
            UsersRepository usersRepository,
            PasswordHasher passwordHasher,
            JwtProvider jwtProvider)
        {
            this.passwordHasher = passwordHasher;
            this.usersRepository = usersRepository;
            this.jwtProvider = jwtProvider;
        }

        public async Task Register(string mail, string password)
        {
            var hashedPassword = passwordHasher.Generate(password);

            var user = User.Create(Guid.NewGuid(), mail, hashedPassword, null);

            await usersRepository.Add(user);
        }

        public async Task<string> Login(string mail, string password)
        {
            var user = await usersRepository.GetByMail(mail);

            var result = passwordHasher.Verify(password, user.PasswordHash);

            if (result == false)
            {
                throw new Exception("Ошибка");
            }

            var token = jwtProvider.GenerateToken(user);

            return token;
        }
    }
}
