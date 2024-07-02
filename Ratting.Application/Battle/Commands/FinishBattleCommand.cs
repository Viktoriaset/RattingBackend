using MediatR;

namespace Ratting.Aplication.Battle.Commands;

public class FinishBattleCommand: IRequest
{
    public Guid roomId;
    public Guid PlayerId;
    public int PlayerResult;
}