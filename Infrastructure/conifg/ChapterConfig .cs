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
    public class ChapterConfig : IEntityTypeConfiguration<Chapter>
    {
        public void Configure(EntityTypeBuilder<Chapter> builder)
        {
            builder.ToTable("Chapters", t =>
            {
                t.HasCheckConstraint("CK_Chapters_Satisfaction", "[Satisfaction] BETWEEN 0 AND 100");
                t.HasCheckConstraint("CK_Chapters_Dates", "[EndsAt] > [StartsAt]");
            });

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Number).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(150).IsRequired();
            builder.Property(x => x.VisibleText).HasMaxLength(4000).IsRequired();

            // بدون حد أقصى لأنه لما نضيف التشفير بعدين، النص المشفّر بيكون أطول من الأصلي
            builder.Property(x => x.SealedText).IsRequired();

            builder.Property(x => x.StartsAt).IsRequired();
            builder.Property(x => x.EndsAt).IsRequired();
            builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(30).IsRequired();
            builder.Property(x => x.Achievements).HasMaxLength(4000).IsRequired(false);
            builder.Property(x => x.Satisfaction).IsRequired(false);
            builder.Property(x => x.ReviewedAt).IsRequired(false);

            builder.HasOne(x => x.Book)
                   .WithMany(b => b.Chapters)
                   .HasForeignKey(x => x.BookId)
                   .OnDelete(DeleteBehavior.Cascade);

            // ما في فصلين بنفس الرقم بنفس الكتاب
            builder.HasIndex(x => new { x.BookId, x.Number }).IsUnique();
        }
    }
}
