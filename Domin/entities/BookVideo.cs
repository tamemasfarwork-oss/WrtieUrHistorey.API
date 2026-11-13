using Domin.entities.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.entities
{
    public class BookVideo
    {
        // المعرّف الفريد لمحاولة الفيديو
        public Guid Id { get; set; }

        // مفتاح أجنبي: الكتاب اللي انعمل إله الفيديو
        public Guid BookId { get; set; }

        // حالة التوليد: بالانتظار، عم يتجهز، جاهز، فشل
        public VideoStatus Status { get; set; }

        // السيناريو اللي كتبه الذكاء الاصطناعي، منحتفظ فيه لحتى ما نعيد الطلب إذا فشل التركيب
        public string? Script { get; set; }

        // مسار الفيديو النهائي بمكان التخزين
        public string? VideoPath { get; set; }

        // سبب الفشل إذا فشل
        public string? FailureReason { get; set; }

        // متى طلبه المستخدم
        public DateTime RequestedAt { get; set; }

        // متى بلّش التوليد
        public DateTime? StartedAt { get; set; }

        // متى خلص (نجاح أو فشل)
        public DateTime? FinishedAt { get; set; }

        // علاقة: الفيديو بينتمي لكتاب واحد
        public Book Book { get; set; } = default!;
    }
}
