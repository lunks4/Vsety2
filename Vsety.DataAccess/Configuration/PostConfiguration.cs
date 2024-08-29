using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsety.DataAccess.Entities;

namespace Vsety.DataAccess.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<PostEntity>
    {
        public void Configure(EntityTypeBuilder<PostEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(a => a.User)
                .WithMany(a => a.Posts)
                .HasForeignKey(a => a.UserId);

            builder.HasOne(a => a.Img)
                .WithOne(a => a.Post)
                .HasForeignKey<PostEntity>(a => a.ImgId);

            builder.HasMany(c => c.UserLikes)
                .WithMany(c => c.PostLikes);

            builder.
                HasMany(c => c.UserReposts)
                .WithMany(c => c.PostReposts);

            builder.HasMany(c => c.UsersComments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId);

        }
    }
}
