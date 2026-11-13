using Domin.entities.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.entities
{
    public class Chapter
    {
        // المعرّف الفريد للفصل
        public Guid Id { get; set; }

        // مفتاح أجنبي: الكتاب اللي بينتمي إله
        public Guid BookId { get; set; }

        // رقم الفصل بالكتاب (الأول، التاني...)
        public int Number { get; set; }

        // عنوان الفصل
        public string Title { get; set; } = default!;

        // الحقل الظاهر: بيضل المستخدم يشوفه طول مدة الفصل
        public string VisibleText { get; set; } = default!;

        // الحقل المختوم: ما بينقرأ لحتى تخلص مدة الفصل (بينخزن مشفّر)
        public string SealedText { get; set; } = default!;

        // بداية الفصل
        public DateTime StartsAt { get; set; }

        // نهاية الفصل: بعدها بينفتح الحقل المختوم وبتصير المراجعة مسموحة
        public DateTime EndsAt { get; set; }

        // حالة الفصل: شغال، تمت مراجعته
        public ChapterStatus Status { get; set; }

        // المراجعة: شو أنجز وشو عمل خلال الفصل
        public string? Achievements { get; set; }

        // نسبة رضاه عن نفسه بهالفصل من صفر لمية
        public int? Satisfaction { get; set; }

        // متى كتب المراجعة
        public DateTime? ReviewedAt { get; set; }

        // علاقة: الفصل بينتمي لكتاب واحد
        public Book Book { get; set; } = default!;

        // علاقة: الفصل إله أهداف كتير
        public ICollection<ChapterGoal> Goals { get; set; } = new List<ChapterGoal>();

        // علاقة: الفصل إله ملاحظات هامش كتير
        public ICollection<MarginNote> Notes { get; set; } = new List<MarginNote>();
    }
}
