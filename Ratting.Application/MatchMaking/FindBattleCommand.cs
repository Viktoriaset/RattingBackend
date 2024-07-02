using AutoMapper;
using Ratting.Application.Battle;
using Ratting.Application.Common.Mappings;

namespace Ratting.Application.MatchMaking;

public class FindBattleCommand
{
    public Guid PlayerId;
    public string PlayerIpAddres;
    
}