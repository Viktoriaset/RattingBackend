using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ratting.Application.Players.Commands.CreatePlayer;
using Ratting.Application.Players.Queries;
using Ratting.Application.Players.Queries.GetPlayer;
using Ratting.WepAPI.Models;

namespace Ratting.WepAPI.Controllers
{

    [Route("api/[controller]")]
    public class PlayerController: BaseController
    {
        private readonly IMapper m_mapper;

        public PlayerController(IMapper mapper)
        {
            m_mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDetailVm>> Get(Guid id)
        {
            var query = new GetPlayerDetailsQuery
            {
                PlayerId = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreatePlayerDto createPlayerDto)
        {
            var command = m_mapper.Map<CreatePlayerCommand>(createPlayerDto);
            command.Id = Guid.NewGuid();
            await Mediator.Send(command);
            return Ok(command.Id);
        }
    }
}
