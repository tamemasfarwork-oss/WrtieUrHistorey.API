using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.entities
{
    public class ChapterGoal
    {
        // المعرّف الفريد للهدف
        public Guid Id { get; set; }

        // مفتاح أجنبي: الفصل اللي بينتمي إله
        public Guid ChapterId { get; set; }

        // مفتاح أجنبي اختياري: هدف الكتاب اللي بيخدمه هالهدف

        // نص الهدف
        public string Text { get; set; } = default!;

        // ترتيب الهدف بالعرض
        public int Order { get; set; }

        // هل تحقق؟ فاضي = لسا ما انقيّم، بيتعبى بمراجعة الفصل
        public bool? IsAchieved { get; set; }

        // علاقة: الهدف بينتمي لفصل واحد
        public Chapter Chapter { get; set; } = default!;

    }
}
