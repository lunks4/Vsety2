using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsety.DataAccess.Entities;

namespace Vsety.DataAccess.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(a => a.Person)
                .WithOne(a => a.User)
                .HasForeignKey<PersonEntity>(a => a.UserId);

            builder.HasMany(a => a.PostLikes)
                .WithMany(a => a.UserLikes);

            builder.HasMany(a => a.PostReposts)
                .WithMany(a => a.UserReposts);

            builder.HasMany(a => a.Posts)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            builder.HasMany(a => a.Comments)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);
        }


    }
}
