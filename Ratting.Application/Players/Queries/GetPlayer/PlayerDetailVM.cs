using AutoMapper;
using Ratting.Application.Common.Mappings;
using Ratting.Domain;

namespace Ratting.Application.Players.Queries.GetPlayer
{
    public class PlayerDetailVm : IMapWith<Player>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BestResult { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Player, PlayerDetailVm>()
                .ForMember(player => player.Name,
                    opt => opt
                        .MapFrom(player => player.Name))
                .ForMember(player => player.Id,
                    opt => opt
                        .MapFrom(player => player.Id))
                .ForMember(player => player.BestResult,
                    opt => opt
                        .MapFrom(player => player.BestResult));
        }
    }
}
