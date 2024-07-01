using AutoMapper;
using Ratting.Aplication.Common.Mappings;
using Ratting.Aplication.Players.Commands.CreatePlayer;

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
