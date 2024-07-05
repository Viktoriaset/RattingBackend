using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratting.Application.Common.Exceptions;
using Ratting.Application.Interfaces;
using Ratting.Domain;

namespace Ratting.Application.Players.Commands.CreatePlayer
{
    internal class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, Guid>
    {
        private readonly IRattingDBContext m_dbContext;

        public CreatePlayerCommandHandler(IRattingDBContext dbContext)
        {
            m_dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var player = new Player
            {
                Id = request.Id,
                Name = request.Name,
            };

            var existPlayer = m_dbContext.players.FirstOrDefault(p => p.Name == player.Name);
            if (existPlayer != null)
            {
                throw new PlayerAlreadyExist(player.Name);
            }

            await m_dbContext.players.AddAsync(player, cancellationToken);
            await m_dbContext.SaveChangeAsync(cancellationToken);

            return player.Id;
        }
    }
}
