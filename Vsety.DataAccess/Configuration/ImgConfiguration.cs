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
    public class ImgConfiguration : IEntityTypeConfiguration<ImgEntity>
    {
        public void Configure(EntityTypeBuilder<ImgEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(a => a.Person)
                .WithOne(a => a.Img)
                .HasForeignKey<ImgEntity>(a => a.PersonId);

            builder.HasOne(a => a.Post)
                .WithOne(a => a.Img)
                .HasForeignKey<PostEntity>(a => a.ImgId);
        }
    }
}
