﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ratting.Aplication.Common.Exceptions;
using Ratting.Aplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratting.Aplication.Players.Queries.GetPlayer;

namespace Ratting.Aplication.Players.Queries
{
    internal class GetPlayerDetailsQueryHandler : IRequestHandler<GetPlayerDetailsQuery, PlayerDetailVm>
    {
        private readonly IRattingDBContext m_dbContext;
        private readonly IMapper m_mapper;
        
        public GetPlayerDetailsQueryHandler(IRattingDBContext dbContext, IMapper mapper)
        {
            m_dbContext = dbContext;
            m_mapper = mapper;
        }

        public async Task<PlayerDetailVm> Handle(GetPlayerDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await m_dbContext.players.FirstOrDefaultAsync(player => player.Id == request.PlayerId, cancellationToken);

            if (entity == null || entity.Id != request.PlayerId)
            {
                throw new NotFoundException(nameof(entity), request.PlayerId);
            }

            return m_mapper.Map<PlayerDetailVm>(entity);
        }
    }
}
