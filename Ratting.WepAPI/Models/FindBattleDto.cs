using AutoMapper;
using Ratting.Aplication.Common.Mappings;
using Ratting.Aplication.MatchMaking;

namespace Ratting.WepAPI.Models;

public class FindBattleDto: IMapWith<FindBattleCommand>
{
    public Guid PlayerId;
    public string PlayerIpAddres;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<FindBattleDto, FindBattleCommand>()
            .ForMember(command => command.PlayerId,
                opt => opt.MapFrom(dto => dto.PlayerId))
            .ForMember(command => command.PlayerIpAddres,
                opt => opt.MapFrom(dto => dto.PlayerIpAddres));
    }
}