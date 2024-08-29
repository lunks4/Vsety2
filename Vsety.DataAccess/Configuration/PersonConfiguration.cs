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
    public class PersonConfiguration : IEntityTypeConfiguration<PersonEntity>
    {
        public void Configure(EntityTypeBuilder<PersonEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(a => a.User)
                .WithOne(a => a.Person)
                .HasForeignKey<PersonEntity>(a => a.UserId);

            builder.HasOne(a => a.Img)
                .WithOne(a => a.Person)
                .HasForeignKey<ImgEntity>(a => a.PersonId);
        }
    }
}
