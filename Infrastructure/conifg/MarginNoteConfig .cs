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
    public class MarginNoteConfig : IEntityTypeConfiguration<MarginNote>
    {
        public void Configure(EntityTypeBuilder<MarginNote> builder)
        {
            builder.ToTable("MarginNotes");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Text).HasMaxLength(2000).IsRequired(false);
            builder.Property(x => x.CreatedAt).IsRequired();

            builder.HasOne(x => x.Chapter)
                   .WithMany(c => c.Notes)
                   .HasForeignKey(x => x.ChapterId)
                   .OnDelete(DeleteBehavior.Cascade);

            // لعرض ملاحظات الفصل مرتبة زمنياً
            builder.HasIndex(x => new { x.ChapterId, x.CreatedAt });
        }
    }

}
