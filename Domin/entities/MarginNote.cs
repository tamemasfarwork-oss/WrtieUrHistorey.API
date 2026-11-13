using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.entities
{
    public class MarginNote
    {
        // المعرّف الفريد للملاحظة
        public Guid Id { get; set; }

        // مفتاح أجنبي: الفصل اللي انكتبت فيه
        public Guid ChapterId { get; set; }

        // نص الملاحظة (ممكن يكون فاضي إذا الملاحظة صور بس)
        public string? Text { get; set; }

        // متى انكتبت، مهم لترتيب اللحظات زمنياً بالفيديو
        public DateTime CreatedAt { get; set; }

        // علاقة: الملاحظة بتنتمي لفصل واحد
        public Chapter Chapter { get; set; } = default!;

        // علاقة: الملاحظة إلها صور كتير
        public ICollection<NoteImage> Images { get; set; } = new List<NoteImage>();
    }

}
