using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratting.Aplication.Players.Queries.GetPlayer;

namespace Ratting.Aplication.Players.Queries
{
    public class GetPlayerDetailsQuery: IRequest<PlayerDetailVm>
    {
        public Guid PlayerId { get; set; }
    }
}
