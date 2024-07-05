using MediatR;
using Ratting.Application.Common.Exceptions;
using Ratting.Application.Interfaces;
using Ratting.Domain;

namespace Ratting.Aplication.Battle.Commands;

public class FinishBattleCommandHandler: IRequestHandler<FinishBattleCommand>
{
    private readonly BattleRoomsController m_battleRoomsController;
    private readonly IRattingDBContext m_dbContext;
    private readonly BattleRewardConfig m_battleRewardConfig;

    public FinishBattleCommandHandler(BattleRoomsController battleRoomsController, IRattingDBContext rattingDbContext,
        BattleRewardConfig battleRewardConfig)
    {
        m_battleRoomsController = battleRoomsController;
        m_dbContext = rattingDbContext;
        m_battleRewardConfig = battleRewardConfig;
    }
    
    public async Task Handle(FinishBattleCommand request, CancellationToken cancellationToken)
    {
        var player = m_dbContext.players.FirstOrDefault(player => player.Id == request.PlayerId);
        if (player == null)
        {
            throw new NotFoundException($"{nameof(Player)}", request.PlayerId);
        }
        
        try
        {
            m_battleRoomsController.OnBattleFinishedAsync(request.roomId, player);
            player.Money += m_battleRewardConfig.GetReward(request.PlayerPosition);
            player.BestResult = request.PlayerResult > player.BestResult ? request.PlayerResult : player.BestResult;
        }
        finally
        {
           await m_dbContext.SaveChangeAsync(cancellationToken);
        }
        
    }
}