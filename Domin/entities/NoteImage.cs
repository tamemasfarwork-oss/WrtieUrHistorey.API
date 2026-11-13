using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.entities
{
    public class NoteImage
    {
        // المعرّف الفريد للصورة
        public Guid Id { get; set; }

        // مفتاح أجنبي: الملاحظة اللي بتنتمي إلها
        public Guid MarginNoteId { get; set; }

        // مسار الملف بمكان التخزين (الصورة نفسها مو بقاعدة البيانات)
        public string StoragePath { get; set; } = default!;

        // نوع الملف، للتحقق وللعرض الصحيح
        public string ContentType { get; set; } = default!;

        // حجم الملف بالبايت، للتحقق من الحد الأقصى
        public long SizeInBytes { get; set; }

        // ترتيب الصورة جوا الملاحظة
        public int Order { get; set; }

        // متى انرفعت
        public DateTime UploadedAt { get; set; }

        // علاقة: الصورة بتنتمي لملاحظة وحدة
        public MarginNote MarginNote { get; set; } = default!;
    }
}
