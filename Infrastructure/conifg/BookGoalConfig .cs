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
    public class BookGoalConfig : IEntityTypeConfiguration<BookGoal>
    {
        public void Configure(EntityTypeBuilder<BookGoal> builder)
        {
            builder.ToTable("BookGoals");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Text).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Order).IsRequired();
            builder.Property(x => x.IsAchieved).IsRequired(false);

            builder.HasOne(x => x.Book)
                   .WithMany(b => b.Goals)
                   .HasForeignKey(x => x.BookId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.BookId, x.Order });
        }
    }

}
