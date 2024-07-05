namespace Ratting.Application.Common.Exceptions;

public class PlayerAlreadyLeaveFromRoom: Exception
{
    public PlayerAlreadyLeaveFromRoom(Guid playerId, Guid roomId)
        : base($"Player {playerId} already leave from room {roomId}") {}
}