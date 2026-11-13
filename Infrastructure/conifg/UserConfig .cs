using Domin.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.conifg
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.FullName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(256).IsRequired();
            builder.Property(x => x.PasswordHash).HasMaxLength(500).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();

            // ما في حسابين بنفس الإيميل
            builder.HasIndex(x => x.Email).IsUnique();
        }
    }

}
