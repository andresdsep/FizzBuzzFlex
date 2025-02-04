using FizzBuzzFlex.Api.Dtos;
using FizzBuzzFlex.Api.Projections;
using FizzBuzzFlex.EF.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FizzBuzzFlex.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GamesController : ControllerBase
{
    private readonly DatabaseContext _context;

    public GamesController(DatabaseContext context)
    {
        _context = context;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<GameMinimalDto>>> GetAll()
    {
        var games = await _context.Games.ToListAsync();
        return Ok(games.Select(GameProjections.ToMinimalDto));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GameReadDto>> GetById(Guid id)
    {
        var game = await _context.Games.Include(g => g.DivisorLabels).FirstOrDefaultAsync(g => g.Id == id);

        if (game == null)
        {
            return NotFound();
        }

        return Ok(game.ToReadDto());
    }
}
