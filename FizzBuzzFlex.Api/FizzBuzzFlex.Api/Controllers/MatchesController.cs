using FizzBuzzFlex.Api.Dtos;
using FizzBuzzFlex.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FizzBuzzFlex.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class MatchesController : ControllerBase
{
    private readonly IMatchService _matchService;

    public MatchesController(IMatchService matchService)
    {
        _matchService = matchService;
    }

    [HttpPost("start")]
    public async Task<ActionResult<RoundResponseDto>> Start(MatchWriteDto dto) =>
        await _matchService.StartMatch(dto);

    [HttpPost("play-round")]
    public async Task<ActionResult<RoundResponseDto>> PlayRound(RoundAnswerDto roundAnswer) =>
        await _matchService.CheckMatchPrompt(roundAnswer);

    [HttpGet("{matchId:guid}/results")]
    public async Task<ActionResult<MatchResultsDto>> GetResults(Guid matchId) =>
        await _matchService.GetMatchResults(matchId);
}
