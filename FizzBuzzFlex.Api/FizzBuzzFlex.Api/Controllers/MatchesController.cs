using FizzBuzzFlex.Api.Dtos;
using FizzBuzzFlex.Api.Projections;
using FizzBuzzFlex.Api.Services;
using FizzBuzzFlex.EF.Context;
using Microsoft.AspNetCore.Mvc;

namespace FizzBuzzFlex.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class MatchesController : ControllerBase
{
    private readonly DatabaseContext _context;
    private readonly IMatchService _matchService;

    public MatchesController(DatabaseContext context, IMatchService matchService)
    {
        _context = context;
        _matchService = matchService;
    }

    [HttpPost("start")]
    public async Task<ActionResult<RoundResponse>> StartForGame(MatchWriteDto dto)
    {
        var newMatch = dto.ToEntity();
        await _context.Matches.AddAsync(newMatch);
        await _context.SaveChangesAsync();

        var prompt = await _matchService.GetMatchPrompt(newMatch);
        return prompt;
    }
}
