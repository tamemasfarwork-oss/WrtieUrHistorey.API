using Domin.entities.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.entities
{
    public class Book
    {
        // المعرّف الفريد للكتاب
        public Guid Id { get; set; }

        // مفتاح أجنبي: صاحب الكتاب
        public Guid UserId { get; set; }

        // عنوان الكتاب
        public string Title { get; set; } = default!;

        // مقدمة الكتاب: ليش بلّش المستخدم هالكتاب (اختيارية)
        public string? Introduction { get; set; }

        // عدد الفصول اللي قرر المستخدم يكتبها
        public int PlannedChaptersCount { get; set; }

        // حالة الكتاب: مفتوح، ناطر الخاتمة، مكتمل
        public BookStatus Status { get; set; }

        // الخاتمة: كلمة المستخدم الأخيرة عن الرحلة كلها
        public string? FinalReflection { get; set; }

        // نسبة رضاه عن الكتاب كله من صفر لمية
        public int? FinalSatisfaction { get; set; }

        // متى انفتح الكتاب
        public DateTime CreatedAt { get; set; }

        // متى اكتمل الكتاب (فاضي إذا لسا ما خلص)
        public DateTime? CompletedAt { get; set; }

        // علاقة: الكتاب بينتمي لمستخدم واحد
        public User User { get; set; } = default!;

        // علاقة: الكتاب إله أهداف عامة كتير
        public ICollection<BookGoal> Goals { get; set; } = new List<BookGoal>();

        // علاقة: الكتاب إله فصول كتير
        public ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();

        // علاقة: الكتاب إله محاولات فيديو كتير (إذا فشلت وحدة أو طلب نسخة جديدة)
        public ICollection<BookVideo> Videos { get; set; } = new List<BookVideo>();
    }

}
