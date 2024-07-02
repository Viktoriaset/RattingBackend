using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratting.Application.Players.Commands.UpdatePlayer
{
    public class UpdatePlayerCommand: IRequest
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int BestResult { get; set; }
    }
}
