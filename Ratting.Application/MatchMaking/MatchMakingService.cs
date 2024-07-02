using Ratting.Application.Battle;
using Ratting.Application.Common.Exceptions;
using Ratting.Application.Interfaces;

namespace Ratting.Application.MatchMaking;

public class MatchMakingService
{
    private readonly Queue<BattleParticipant> m_qOnBattle = new();
    private readonly IRattingDBContext m_dbContext;
    private readonly BattleCreateService m_battleCreateService;
    private readonly MatchMakingConfiguration m_matchMakingConfiguration;

    public MatchMakingService(IRattingDBContext rattingDbContext, BattleCreateService battleCreateService,
        MatchMakingConfiguration matchMakingConfiguration)
    {
        m_dbContext = rattingDbContext;
        m_battleCreateService = battleCreateService;
        m_matchMakingConfiguration = matchMakingConfiguration;
    }

    public void AddPlayerInQ(FindBattleCommand findBattleCommand)
    {
        var player = m_dbContext.players.FirstOrDefault(player => player.Id == findBattleCommand.PlayerId);
        
        if (player == null)
        {
            throw new NotFoundException("Player", findBattleCommand.PlayerId);
        }

        BattleParticipant battleParticipant = new BattleParticipant()
        {
            Player = player,
            PlayerAddress =  findBattleCommand.PlayerIpAddres
        };
        m_qOnBattle.Enqueue(battleParticipant);

        if (m_qOnBattle.Count >= m_matchMakingConfiguration.MaxPlayer)
        {
            List<BattleParticipant> participants = new();
            for (int i = 0; i < m_matchMakingConfiguration.MaxPlayer; i++)
            {
                participants.Add(m_qOnBattle.Dequeue());
            }
            
            m_battleCreateService.CreateBattle(participants);
        }
    }

   
}