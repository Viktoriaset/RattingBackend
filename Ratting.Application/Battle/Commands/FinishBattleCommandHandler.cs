using MediatR;
using Ratting.Application.Common.Exceptions;
using Ratting.Application.Interfaces;
using Ratting.Application.MatchMaking;
using Ratting.Domain;

namespace Ratting.Aplication.Battle.Commands;

public class FinishBattleCommandHandler: IRequestHandler<FinishBattleCommand>
{
    private readonly BattleRoomsController m_battleRoomsController;
    private readonly IRattingDBContext m_dbContext;

    public FinishBattleCommandHandler(BattleRoomsController battleRoomsController, IRattingDBContext rattingDbContext)
    {
        m_battleRoomsController = battleRoomsController;
        m_dbContext = rattingDbContext;
    }
    
    public async Task Handle(FinishBattleCommand request, CancellationToken cancellationToken)
    {
        var player = m_dbContext.players.FirstOrDefault(player => player.Id == request.PlayerId);
        if (player == null)
        {
            throw new NotFoundException($"{nameof(Player)}", request.PlayerId);
        }

        player.BestResult = request.PlayerResult > player.BestResult ? request.PlayerResult : player.BestResult;

        try
        {
            m_battleRoomsController.OnBattleFinishedAsync(request.roomId);
        }
        catch (NotFoundException e)
        {
            // Ожидаемая ошибка при отправке уведомлений о завершении битвы от всех ее участников.
        }
        catch (Exception e)
        {
            throw;
        }
        finally
        {
           await m_dbContext.SaveChangeAsync(cancellationToken);
        }
        
    }
}