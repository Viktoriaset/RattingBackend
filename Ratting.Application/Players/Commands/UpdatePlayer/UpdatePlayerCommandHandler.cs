using MediatR;
using Microsoft.EntityFrameworkCore;
using Ratting.Application.Common.Exceptions;
using Ratting.Application.Interfaces;

namespace Ratting.Application.Players.Commands.UpdatePlayer
{
    internal class UpdatePlayerCommandHandler : IRequestHandler<UpdatePlayerCommand>
    {
        private readonly IRattingDBContext m_dbContext;

        public UpdatePlayerCommandHandler(IRattingDBContext dbContext)
        {
            m_dbContext = dbContext;
        }

        public async Task Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
        {
            var entity = await m_dbContext.players.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

            if (entity == null || entity.Id != request.UserId)
            {
                throw new NotFoundException(nameof(entity), request.UserId);
            }

            entity.Id = request.UserId;
            entity.Name = request.Name;
            entity.BestResult = request.BestResult;

            await m_dbContext.SaveChangeAsync(cancellationToken);
        }
    }
}
