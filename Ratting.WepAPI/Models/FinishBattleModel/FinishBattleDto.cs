using AutoMapper;
using Ratting.Aplication.Battle.Commands;
using Ratting.Application.Common.Mappings;

namespace Ratting.WepAPI.Models;

public class FinishBattleDto: IMapWith<FinishBattleCommand>
{
    public Guid RoomName;
    public Guid PlayerId;
    public int PlayerResult;
    public int PlayerPosition;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<FinishBattleDto, FinishBattleCommand>()
            .ForMember(command => command.PlayerId,
                opt => opt.MapFrom(dto => dto.PlayerId))
            .ForMember(command => command.roomId,
                opt => opt.MapFrom(dto => dto.RoomName))
            .ForMember(command => command.PlayerResult,
                opt => opt.MapFrom(dto => dto.PlayerResult))
            .ForMember(command => command.PlayerPosition,
                opt => opt.MapFrom(dto => dto.PlayerPosition));
    }
}