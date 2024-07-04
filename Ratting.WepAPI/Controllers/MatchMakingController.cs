using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ratting.Aplication.MatchMaking;
using Ratting.Application.MatchMaking;
using Ratting.WepAPI.Models;

namespace Ratting.WepAPI.Controllers;

[Route("api/matchmacking")]
public class MatchMakingController: BaseController
{
    private readonly IMapper m_mapper;
    private readonly MatchMakingService m_matchMakingService;

    public MatchMakingController(IMapper mapper, MatchMakingService matchMakingService)
    {
        m_mapper = mapper;
        m_matchMakingService = matchMakingService;
    }
    
    [HttpPost]
    public async Task<IActionResult> FindBattle([FromBody] FindBattleDto findBattleDto)
    {
        var command = m_mapper.Map<FindBattleCommand>(findBattleDto);
        m_matchMakingService.AddPlayerInQ(command);
        return Ok();
    }
}