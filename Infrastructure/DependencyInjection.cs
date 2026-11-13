using Application.IRepo;
using Infrastructure.conifg;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // بيقول للتطبيق: كل ما حدا طلب السياق، اعمله نسخة
            // واستخدم خادم مايكروسوفت مع نص الاتصال اللي اسمه الافتراضي
            services.AddDbContext<AppDbContext>(options =>
     options.UseSqlServer(configuration.GetConnectionString("Default")));

            services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());

            return services;
        }


    }
}
