using Microsoft.EntityFrameworkCore;
using System.IO;
using Vsety.DataAccess.Entities;

namespace Vsety.DataAccess
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<PersonEntity> Persons { get; set; }

        public DbSet<ImgEntity> Imgs { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=localhost;port=3306;Database=db_vsety;User=root;Password=admin;",
                    new MySqlServerVersion(new Version(8, 0, 21)));

            }
        }
    }
}
