using Vsety.DataAccess.Entities;

namespace Vsety.DataAccess.Repositories
{
    public class PersonsRepository
    {
        private readonly ApplicationContext _context;
        public PersonsRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddPerson(Guid userId, PersonEntity person)
        {
            var user = _context.Users.FirstOrDefault(c => c.Id == userId)
                ?? throw new Exception();
            user.Person = person;
            person.user = user;

            await _context.SaveChangesAsync(); 
        }
    }
}
