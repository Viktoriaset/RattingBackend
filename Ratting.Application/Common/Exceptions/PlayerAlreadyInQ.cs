namespace Ratting.Application.Common.Exceptions;

public class PlayerAlreadyInQ: Exception
{
    public PlayerAlreadyInQ(Guid playerId) 
        : base($"Player [{playerId.ToString()} is already in query]") {}
}