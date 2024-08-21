using Vsety.Infrastructure;
using Vsety.Core.Models;
using Vsety.DataAccess.Repositories;

namespace Vsety.Application.Services
{
    public class UserService
    {
        private readonly IPasswordHasher passwordHasher;
        private readonly IUsersRepository usersRepository;
        private readonly IJwtProvider jwtProvider;

        public UserService(
            IUsersRepository usersRepository,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider)
        {
            this.passwordHasher = passwordHasher;
            this.usersRepository = usersRepository;
            this.jwtProvider = jwtProvider;
        }

        public async Task Register(string mail, string password)
        {
            var hashedPassword = passwordHasher.Generate(password);

            var user = new User(Guid.NewGuid(), mail, hashedPassword);

            await usersRepository.Add(user);
        }

        public async Task<string> Login(string mail, string password)
        {
            var user = await usersRepository.GetByMail(mail);

            var result = passwordHasher.Verify(password, user.PasswordHash);

            if (result == false)
            {
                return "";
            }

            var token = jwtProvider.GenerateToken(user);

            return token;
        }
    }
}
