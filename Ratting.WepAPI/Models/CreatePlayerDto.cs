using AutoMapper;
using Ratting.Application.Common.Mappings;
using Ratting.Application.Players.Commands.CreatePlayer;

namespace Ratting.WepAPI.Models
{
    public class CreatePlayerDto: IMapWith<CreatePlayerCommand>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePlayerDto, CreatePlayerCommand>()
                .ForMember(playerCommand => playerCommand.Name,
                    opt => opt.MapFrom(playerDto => playerDto.Name));
        }
    }
}
