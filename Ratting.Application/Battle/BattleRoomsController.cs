using Ratting.Application.Battle;
using Ratting.Application.Common.Exceptions;
using Ratting.Application.Interfaces;
using Ratting.Domain;

namespace Ratting.Aplication.Battle;

public class BattleRoomsController
{
    private const int BATTLE_REWARD = 10;

    private readonly IRattingDBContext m_dbContext;
    private readonly List<BattleRoom> m_rooms;

    public BattleRoomsController(IRattingDBContext rattingDbContext)
    {
        m_dbContext = rattingDbContext;
    }

    public void OnRoomCreated(BattleRoom room)
    {
        m_rooms.Add(room);
    }

    public async void OnBattleFinishedAsync(Guid roomId)
    {
        BattleRoom battleRoom = m_rooms.FirstOrDefault(room => room.roomId == roomId);
        if (battleRoom == null)
        {
            throw new NotFoundException($"{nameof(BattleRoom)}", roomId);
        }

        foreach (var particapant in battleRoom.Participants)
        {
            particapant.Player.Money += BATTLE_REWARD;
        }

        var cts = new CancellationTokenSource();
        await m_dbContext.SaveChangeAsync(cts.Token);

        m_rooms.Remove(battleRoom);
    }
}