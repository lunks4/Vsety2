using Microsoft.EntityFrameworkCore;
using System.IO;
using Vsety.DataAccess.Configuration;
using Vsety.DataAccess.Entities;
using Vsety.DataAccess.Entities.ManyToMany;

namespace Vsety.DataAccess
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<PersonEntity> Persons { get; set; }

        public DbSet<ImgEntity> Imgs { get; set; }

        public DbSet<PostEntity> Posts { get; set; }

        //public DbSet<UserLikesEntity> UserLikes { get; set; }

        public DbSet<CommentEntity> Comments { get; set; }

        //public DbSet<UserRepostsEntity> UserReposts { get; set; }

        //public DbSet<PostLikesEntity> PostLikes { get; set; }

        //public DbSet<PostRepostEntity> PostReposts { get; set; }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new ImgConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            //modelBuilder.ApplyConfiguration(new UserLikesConfiguration());
            //modelBuilder.ApplyConfiguration(new UserRepostsConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
