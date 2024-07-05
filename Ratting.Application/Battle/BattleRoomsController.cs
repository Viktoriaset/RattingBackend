using Microsoft.Extensions.DependencyInjection;
using Ratting.Application.Battle;
using Ratting.Application.Common.Exceptions;
using Ratting.Application.Common.MessageParamsName;
using Ratting.Application.Interfaces;
using Ratting.Domain;

namespace Ratting.Aplication.Battle;

public class BattleRoomsController
{
    private const int TIME_WAIT_USERS = 30000;

    private readonly List<BattleRoom> m_rooms = new ();
    private readonly HttpClient m_client;
    private CancellationTokenSource m_cts;

    public BattleRoomsController(HttpClient httpClient)
    {
        m_client = httpClient;
        m_cts = new CancellationTokenSource();
    }

    public void OnRoomCreated(BattleRoom room)
    {
        m_rooms.Add(room);
    }

    public async void OnBattleFinishedAsync(Guid roomId, Player player)
    {
        BattleRoom battleRoom = m_rooms.FirstOrDefault(room => room.roomId == roomId);
        if (battleRoom == null)
        {
            throw new NotFoundException($"{nameof(BattleRoom)}", roomId);
        }

        BattleParticipant p = battleRoom.Participants.FirstOrDefault(partic => partic.Player.Id == player.Id);
        if (p == null)
        {
            throw new PlayerAlreadyLeaveFromRoom(player.Id, roomId);
        }

        battleRoom.Participants.Remove(p);
        if (battleRoom.Participants.Count == 0)
        {
            m_cts.Cancel();
            m_rooms.Remove(battleRoom);
            return;
        }

        RemoveRoomIfTimeExpired(battleRoom, m_cts.Token);
    }
    
    private async void RemoveRoomIfTimeExpired(BattleRoom battleRoom, CancellationToken ct)
    {
        try
        {
            await Task.Delay(TIME_WAIT_USERS, ct);
            if (m_rooms.Contains(battleRoom))
            {
                foreach (var participant in battleRoom.Participants)
                {
                    SendRoomDestroyd(participant);
                }

                m_rooms.Remove(battleRoom);
            }
        }
        catch (TaskCanceledException e)
        {
            // Ожидаемая ошибка при отмене автоудаления комнаты.
        }
    }

    private void SendRoomDestroyd(BattleParticipant participant)
    {
        var values = new Dictionary<string, string>()
        {
            { MessageParamNameS.RoomDestroyd, "" }
        };
        
        var content = new FormUrlEncodedContent(values);
        m_client.PostAsync(participant.PlayerAddress, content);
    }
}