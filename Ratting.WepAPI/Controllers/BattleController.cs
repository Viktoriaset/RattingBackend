using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ratting.Aplication.Battle.Commands;
using Ratting.Application.MatchMaking;
using Ratting.WepAPI.Models;

namespace Ratting.WepAPI.Controllers;

public class BattleController: BaseController
{
    private readonly IMapper m_mapper;
    private readonly IMediator m_mediator;

    public BattleController(IMapper mapper, IMediator mediator)
    {
        m_mapper = mapper;
        m_mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> FindBattle([FromBody] FinishBattleDto dto)
    {
        var command = m_mapper.Map<FinishBattleCommand>(dto);
        await m_mediator.Send(command);
        return Ok();
    }
}