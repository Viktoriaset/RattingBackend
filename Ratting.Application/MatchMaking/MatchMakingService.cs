using Microsoft.Extensions.DependencyInjection;
using Ratting.Application.Battle;
using Ratting.Application.Common.Exceptions;
using Ratting.Application.Interfaces;
using Ratting.Application.MatchMaking;

namespace Ratting.Aplication.MatchMaking;

public class MatchMakingService
{
    private readonly Queue<BattleParticipant> m_qOnBattle = new();
    private readonly BattleCreateService m_battleCreateService;
    private readonly MatchMakingConfiguration m_matchMakingConfiguration;
    private readonly IServiceScopeFactory m_serviceScopeFactory;

    public MatchMakingService(IServiceScopeFactory  serviceScopeFactory, BattleCreateService battleCreateService,
        MatchMakingConfiguration matchMakingConfiguration)
    {
        m_battleCreateService = battleCreateService;
        m_matchMakingConfiguration = matchMakingConfiguration;
        m_serviceScopeFactory = serviceScopeFactory;
    }

    public void AddPlayerInQ(FindBattleCommand findBattleCommand)
    {
        using (var scope = m_serviceScopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<IRattingDBContext>();
            var player = dbContext.players.FirstOrDefault(player => player.Id == findBattleCommand.PlayerId);
        
            if (player == null)
            {
                throw new NotFoundException("Player", findBattleCommand.PlayerId);
            }

            var p = m_qOnBattle.FirstOrDefault(bp => bp.Player.Id == player.Id);
            if (p != null)
            {
                throw new PlayerAlreadyInQ(player.Id);
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

   
}