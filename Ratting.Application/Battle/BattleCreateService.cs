using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Ratting.Aplication.Battle;
using Ratting.Application.Common.MessageParamsName;
using Ratting.Application.MatchMaking;

namespace Ratting.Application.Battle;

public class BattleCreateService
{
    private readonly MatchMakingConfiguration m_matchMakingConfiguration;
    private readonly HttpClient m_client;
    private readonly BattleRoomsController m_battleRoomsController;

    public BattleCreateService(MatchMakingConfiguration makingConfiguration, HttpClient client,
        BattleRoomsController battleRoomsController)
    {
        m_matchMakingConfiguration = makingConfiguration;
        m_client = client;
        m_battleRoomsController = battleRoomsController;
    }
    
    public async void CreateBattle(List<BattleParticipant> participants)
    {
        var host = participants[0];
        Guid roomName = Guid.NewGuid();
        var values = new Dictionary<string, string>()
        {
            { MessageParamNameS.RoomName, roomName.ToString() }
        };

        try
        {
            await SendCreateRoom(values, host);

            List<Task> waitingPlayers = new List<Task>();
            for (int i = 1; i < participants.Count; i++)
            {
               waitingPlayers.Add(SendConnectToRoom(values, participants[i], roomName.ToString()));
            }

            await Task.WhenAll(waitingPlayers);
            OnRoomCreated(participants, roomName);
        }
        catch (Exception e)
        {
            CancelBattle(participants);
        }
    }
    
    private async Task SendCreateRoom(Dictionary<string, string> values, BattleParticipant participant)
    {
        values.Add(MessageParamNameI.MaxPlayers, m_matchMakingConfiguration.MaxPlayer.ToString());
        values.Add(MessageParamNameB.IsHost, "1");

        var json = JsonConvert.SerializeObject(values);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await m_client.PostAsync(participant.PlayerAddress + "roomCreate", content);
        response.EnsureSuccessStatusCode();
    }

    private async Task SendConnectToRoom(Dictionary<string, string> values, BattleParticipant participant, string roomName)
    {
        values[MessageParamNameB.IsHost] = "0";
        
        var json = JsonConvert.SerializeObject(values);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var responce = await m_client.PostAsync(participant.PlayerAddress + "roomCreate", content);
        responce.EnsureSuccessStatusCode();
    }
    
    private void OnRoomCreated(List<BattleParticipant> participants, Guid roomId)
    {
        BattleRoom battleRoom = new()
        {
            Participants = participants,
            roomId = roomId
        };
        
        m_battleRoomsController.OnRoomCreated(battleRoom);
    }

    private void CancelBattle(List<BattleParticipant> participants)
    {
        var values = new Dictionary<string, string>()
        {
            { Protocols.BattleCanceled, "" }
        };

        var json = JsonConvert.SerializeObject(values);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        foreach (var participant in participants)
        {
            m_client.PostAsync(participant.PlayerAddress, content);
        }
    }
}