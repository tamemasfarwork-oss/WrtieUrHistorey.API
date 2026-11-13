using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.entities.events
{
    public enum BookStatus
    {
        Open = 1,                 // مفتوح والمستخدم عم يكتب فصوله
        AwaitingFinalReview = 2,  // كل الفصول انراجعت وناطر الخاتمة
        Completed = 3             // مكتمل وجاهز للفيديو
    }

    public enum ChapterStatus
    {
        InProgress = 1,  // شغال
        Reviewed = 2     // انراجع وانقفل
    }

    public enum VideoStatus
    {
        Pending = 1,     // بالانتظار
        Processing = 2,  // عم يتجهز
        Ready = 3,       // جاهز
        Failed = 4       // فشل
    }
}
