using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.entities
{
    public class BookGoal
    {
        // المعرّف الفريد للهدف
        public Guid Id { get; set; }

        // مفتاح أجنبي: الكتاب اللي بينتمي إله
        public Guid BookId { get; set; }

        // نص الهدف
        public string Text { get; set; } = default!;

        // ترتيب الهدف بالعرض
        public int Order { get; set; }

        // هل تحقق؟ فاضي = لسا ما انقيّم، بيتعبى بالخاتمة
        public bool? IsAchieved { get; set; }

        // علاقة: الهدف بينتمي لكتاب واحد
        public Book Book { get; set; } = default!;

        // علاقة: أهداف الفصول اللي بتخدم هالهدف الكبير
    }
}
