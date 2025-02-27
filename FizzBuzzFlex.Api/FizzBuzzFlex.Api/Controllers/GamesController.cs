using FizzBuzzFlex.Api.Dtos;
using FizzBuzzFlex.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FizzBuzzFlex.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;

    public GamesController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<GameMinimalDto>>> GetAll()
    {
        return Ok(await _gameService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GameReadDto>> GetById(Guid id)
    {
        var game = await _gameService.GetById(id);

        if (game == null)
        {
            return NotFound();
        }

        return game;
    }

    [HttpPost]
    public async Task<ActionResult<GameReadDto>> Create(GameWriteDto dto)
    {
        var game = await _gameService.Create(dto);

        return CreatedAtAction(nameof(GetById), new { id = game.Id }, game);
    }
}
