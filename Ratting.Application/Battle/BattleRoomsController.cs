using Microsoft.Extensions.DependencyInjection;
using Ratting.Application.Battle;
using Ratting.Application.Common.Exceptions;
using Ratting.Application.Interfaces;

namespace Ratting.Aplication.Battle;

public class BattleRoomsController
{
    private const int BATTLE_REWARD = 10;

    private readonly IServiceScopeFactory m_serviceScopeFactory;
    private readonly List<BattleRoom> m_rooms;

    public BattleRoomsController(IServiceScopeFactory serviceScopeFactory)
    {
        m_serviceScopeFactory = serviceScopeFactory;
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
        SaveChangeAsync(cts);

        m_rooms.Remove(battleRoom);
    }

    private async void SaveChangeAsync(CancellationTokenSource cts)
    {
        using var scope = m_serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IRattingDBContext>();
        await dbContext.SaveChangeAsync(cts.Token);
    }
}