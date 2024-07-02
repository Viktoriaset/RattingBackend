using AutoMapper;
using Ratting.Aplication.Battle;
using Ratting.Aplication.Common.Mappings;

namespace Ratting.Aplication.MatchMaking;

public class FindBattleCommand
{
    public Guid PlayerId;
    public string PlayerIpAddres;
    
}