using FizzBuzzFlex.Api.Dtos;
using FizzBuzzFlex.Api.Projections;
using FizzBuzzFlex.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace FizzBuzzFlex.Api.Services;

public class GameService : IGameService
{
    private readonly DatabaseContext _context;

    public GameService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<GameReadDto> Create(GameWriteDto dto)
    {
        var game = dto.ToEntity();

        _context.Games.Add(game);
        await _context.SaveChangesAsync();

        return game.ToReadDto();
    }

    public async Task<IEnumerable<GameMinimalDto>> GetAll()
    {
        var games = await _context.Games.ToListAsync();
        return games.Select(GameProjections.ToMinimalDto);
    }

    public async Task<GameReadDto?> GetById(Guid id)
    {
        var game = await _context.Games.Include(g => g.DivisorLabels)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (game == null)
        {
            return null;
        }

        return game.ToReadDto();
    }
}