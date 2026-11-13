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
    public class ChapterGoalConfig : IEntityTypeConfiguration<ChapterGoal>
    {
        public void Configure(EntityTypeBuilder<ChapterGoal> builder)
        {
            builder.ToTable("ChapterGoals");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Text).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Order).IsRequired();
            builder.Property(x => x.IsAchieved).IsRequired(false);

            builder.HasOne(x => x.Chapter)
                   .WithMany(c => c.Goals)
                   .HasForeignKey(x => x.ChapterId)
                   .OnDelete(DeleteBehavior.Cascade);

            // انتبه: هون ما فينا نحط حذف متتالي (الشرح تحت الكود)
          

            builder.HasIndex(x => new { x.ChapterId, x.Order });
        }
    }

}
