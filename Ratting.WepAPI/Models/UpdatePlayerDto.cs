using AutoMapper;
using Ratting.Aplication.Common.Mappings;
using Ratting.Aplication.Players.Commands.UpdatePlayer;

namespace Ratting.WepAPI.Models
{
    public class UpdatePlayerDto: IMapWith<UpdatePlayerCommand>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int BestResult { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdatePlayerDto, UpdatePlayerCommand>()
                .ForMember(playerCommand => playerCommand.UserId,
                    opt => opt.MapFrom(playerDto => playerDto.Id))
                .ForMember(playerCommand => playerCommand.Name,
                    opt => opt.MapFrom(playerDto => playerDto.Name))
                .ForMember(playerCommand => playerCommand.BestResult,
                    opt => opt.MapFrom(playerDto => playerDto.BestResult));
        }

    }
}
