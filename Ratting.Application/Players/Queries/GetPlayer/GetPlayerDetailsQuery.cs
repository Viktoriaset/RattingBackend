using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratting.Application.Players.Queries.GetPlayer;

namespace Ratting.Application.Players.Queries
{
    public class GetPlayerDetailsQuery: IRequest<PlayerDetailVm>
    {
        public Guid PlayerId { get; set; }
    }
}
