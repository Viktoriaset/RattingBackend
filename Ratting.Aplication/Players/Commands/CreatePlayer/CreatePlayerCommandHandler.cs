﻿using MediatR;
using MediatR.Pipeline;
using Ratting.Aplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratting.Domain;

namespace Ratting.Aplication.Players.Commands.CreatePlayer
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

            await m_dbContext.players.AddAsync(player, cancellationToken);
            await m_dbContext.SaveChangeAsync(cancellationToken);

            return player.Id;
        }
    }
}
