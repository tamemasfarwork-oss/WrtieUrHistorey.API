using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.entities
{
    public class User
    {
        // المعرّف الفريد للمستخدم
        public Guid Id { get; set; }

        // اسم المستخدم اللي بيظهر بالكتب
        public string FullName { get; set; } = default!;

        // البريد الإلكتروني لتسجيل الدخول والإشعارات
        public string Email { get; set; } = default!;

        // كلمة السر بعد التشفير، ما بتنخزن كنص عادي أبداً
        public string PasswordHash { get; set; } = default!;

        // تاريخ إنشاء الحساب
        public DateTime CreatedAt { get; set; }

        // علاقة: المستخدم إله كتب كتير
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
