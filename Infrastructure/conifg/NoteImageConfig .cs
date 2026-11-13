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
    public class NoteImageConfig : IEntityTypeConfiguration<NoteImage>
    {
        public void Configure(EntityTypeBuilder<NoteImage> builder)
        {
            builder.ToTable("NoteImages", t =>
            {
                t.HasCheckConstraint("CK_NoteImages_Size", "[SizeInBytes] > 0");
            });

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.StoragePath).HasMaxLength(500).IsRequired();
            builder.Property(x => x.ContentType).HasMaxLength(100).IsRequired();
            builder.Property(x => x.SizeInBytes).IsRequired();
            builder.Property(x => x.Order).IsRequired();
            builder.Property(x => x.UploadedAt).IsRequired();

            builder.HasOne(x => x.MarginNote)
                   .WithMany(n => n.Images)
                   .HasForeignKey(x => x.MarginNoteId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.MarginNoteId, x.Order });
        }
    }
}
