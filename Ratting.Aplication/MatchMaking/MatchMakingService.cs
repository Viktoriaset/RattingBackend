using Ratting.Aplication.Common.MessageParamsName;

namespace Ratting.Aplication.MatchMaking;

public class MatchMakingService
{
    private readonly Queue<FindBattleCommand> m_qOnBattle = new();
    private readonly MatchMakingConfiguration m_matchMakingConfiguration;
    private readonly HttpClient m_client;

    public MatchMakingService(MatchMakingConfiguration matchMakingConfiguration, HttpClient httpClient)
    {
        m_matchMakingConfiguration = matchMakingConfiguration;
        m_client = httpClient;
    }

    public void AddPlayerInQ(FindBattleCommand findBattleCommand)
    {
        m_qOnBattle.Enqueue(findBattleCommand);

        if (m_qOnBattle.Count >= m_matchMakingConfiguration.MaxPlayer)
        {
            CreateBattle();
        }
    }

    private async void CreateBattle()
    {
        var host = m_qOnBattle.Dequeue();
        string roomName = host.PlayerId.ToString();
        var values = new Dictionary<string, string>()
        {
            { MessageParamNameS.RoomName, roomName }
        };
        
        await SendCreateRoom(values, host);
        for (int i = 0; i < m_matchMakingConfiguration.MaxPlayer - 1; i++)
        {
            SendConnectToRoom(values, m_qOnBattle.Dequeue(), roomName);
        }
    }

    private async Task SendCreateRoom(Dictionary<string, string> values, FindBattleCommand findBattleCommand)
    {
        values.Add(MessageParamNameI.MaxPlayers, m_matchMakingConfiguration.MaxPlayer.ToString());
        values.Add(MessageParamNameB.IsHost, "1");

        var content = new FormUrlEncodedContent(values);

        var response = await m_client.PostAsync(findBattleCommand.PlayerIpAddres, content);
    }

    private async Task SendConnectToRoom(Dictionary<string, string> values, FindBattleCommand findBattleCommand, string roomName)
    {
        values.Add(MessageParamNameB.IsHost, "0");
        
        var content = new FormUrlEncodedContent(values);

        var responce = m_client.PostAsync(findBattleCommand.PlayerIpAddres, content);
    }
}