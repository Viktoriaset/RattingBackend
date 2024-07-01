using Microsoft.EntityFrameworkCore;
using Ratting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratting.Aplication.Interfaces
{
    public interface IRattingDBContext
    {
        DbSet<Player> players { get; set; }
        Task<int> SaveChangeAsync(CancellationToken cancellationToken);
    }
}
