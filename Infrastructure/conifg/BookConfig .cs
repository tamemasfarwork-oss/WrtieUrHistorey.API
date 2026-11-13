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
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books", t =>
            {
                t.HasCheckConstraint("CK_Books_PlannedChapters", "[PlannedChaptersCount] >= 1");
                t.HasCheckConstraint("CK_Books_FinalSatisfaction", "[FinalSatisfaction] BETWEEN 0 AND 100");
            });

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Title).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Introduction).HasMaxLength(2000).IsRequired(false);
            builder.Property(x => x.PlannedChaptersCount).IsRequired();
            builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(30).IsRequired();
            builder.Property(x => x.FinalReflection).HasMaxLength(4000).IsRequired(false);
            builder.Property(x => x.FinalSatisfaction).IsRequired(false);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CompletedAt).IsRequired(false);

            // المستخدم إله كتب كتير، ولما ينحذف المستخدم بتنحذف كتبه
            builder.HasOne(x => x.User)
                   .WithMany(u => u.Books)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // لعرض كتب المستخدم بسرعة
            builder.HasIndex(x => x.UserId);
        }
    }
}
