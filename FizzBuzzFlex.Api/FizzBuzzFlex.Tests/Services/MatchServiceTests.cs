using FizzBuzzFlex.Api.Services;
using FizzBuzzFlex.EF.Context;
using FizzBuzzFlex.EF.Entities;
using FizzBuzzFlex.Tests.Utils;
using Microsoft.EntityFrameworkCore;

namespace FizzBuzzFlex.Tests.Services;

public class MatchServiceTests
{
    private readonly DatabaseContext _context = DatabaseContextHelper.CreateInMemoryDatabase();
    private readonly MatchService _service;

    public MatchServiceTests()
    {
        _service = new MatchService(_context);
    }

    [Fact]
    public async Task ShouldCreateFirstPromptForMatch()
    {
        var match = await SetUpGameAndMatch();

        var prompt = await _service.GetMatchPrompt(match);

        Assert.NotNull(prompt);

        var matchInContext = await _context.Matches.Include(m => m.Prompts).FirstOrDefaultAsync(m => m.Id == match.Id);
        Assert.NotNull(matchInContext);
        Assert.Single(matchInContext.Prompts);
    }

    private async Task<Match> SetUpGameAndMatch()
    {
        var game = new Game
        {
            Id = Guid.NewGuid(),
            Name = "FizzBuzz",
            Author = "Andres",
        };
        var match = new Match
        {
            Id = Guid.NewGuid(),
            GameId = game.Id,
            DurationInSeconds = 60,
        };
        _context.Games.Add(game);
        _context.Matches.Add(match);
        await _context.SaveChangesAsync();
        return match;
    }
}