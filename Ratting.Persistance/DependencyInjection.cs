using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ratting.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratting.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["Data Source=ratting.sqlite"];
            services.AddDbContext<RattingDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });
            services.AddScoped<IRattingDBContext>(provider => provider.GetService<RattingDbContext>() ?? throw new InvalidOperationException());
            return services;
        }
    }
}
