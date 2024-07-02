﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ratting.Aplication.Battle.Commands;
using Ratting.WepAPI.Models;

namespace Ratting.WepAPI.Controllers;

public class BattleController: BaseController
{
    private readonly IMapper m_mapper;

    public BattleController(IMapper mapper)
    {
        m_mapper = mapper;
    }
    
    [HttpPost]
    public async Task<IActionResult> FindBattle([FromBody] FinishBattleDto dto)
    {
        var command = m_mapper.Map<FinishBattleCommand>(dto);
        await Mediator.Send(command);
        return Ok();
    }
}