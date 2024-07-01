using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ratting.Persistance
{
    public class DBInitializer
    {
        public static void Initialize(RattingDbContext context)
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }
    }
}
