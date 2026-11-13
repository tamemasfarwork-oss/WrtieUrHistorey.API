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
    public class BookVideoConfig : IEntityTypeConfiguration<BookVideo>
    {
        public void Configure(EntityTypeBuilder<BookVideo> builder)
        {
            builder.ToTable("BookVideos");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(30).IsRequired();
            builder.Property(x => x.Script).IsRequired(false);
            builder.Property(x => x.VideoPath).HasMaxLength(500).IsRequired(false);
            builder.Property(x => x.FailureReason).HasMaxLength(1000).IsRequired(false);
            builder.Property(x => x.RequestedAt).IsRequired();
            builder.Property(x => x.StartedAt).IsRequired(false);
            builder.Property(x => x.FinishedAt).IsRequired(false);

            builder.HasOne(x => x.Book)
                   .WithMany(b => b.Videos)
                   .HasForeignKey(x => x.BookId)
                   .OnDelete(DeleteBehavior.Cascade);

            // للمهمة الخلفية لتلاقي الفيديوهات اللي بالانتظار بسرعة
            builder.HasIndex(x => new { x.BookId, x.Status });
        }
    }
}
