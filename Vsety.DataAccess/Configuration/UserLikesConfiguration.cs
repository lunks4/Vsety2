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
    public class UserLikesConfiguration : IEntityTypeConfiguration<UserLikesEntity>
    {
        public void Configure(EntityTypeBuilder<UserLikesEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(c => c.User);

            builder.HasMany(c => c.Posts);
        }
    }
}
