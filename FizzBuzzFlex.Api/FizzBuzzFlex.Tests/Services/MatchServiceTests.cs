using System.Threading.Tasks;
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

        var prompt = await _service.GetMatchPrompt(match, false);
        Assert.NotNull(prompt);

        var matchInContext = await _context.Matches.Include(m => m.Prompts)
            .FirstOrDefaultAsync(m => m.Id == match.Id);
        Assert.NotNull(matchInContext);
        Assert.Single(matchInContext.Prompts);
    }

    [Fact]
    public async Task ShouldCreateFollowUpPromptForMatch()
    {
        var match = await SetUpGameAndMatch(1);

        var prompt = await _service.GetMatchPrompt(match, true);
        Assert.NotNull(prompt);

        var matchInContext = await _context.Matches.Include(m => m.Prompts)
            .FirstOrDefaultAsync(m => m.Id == match.Id);
        Assert.NotNull(matchInContext);
        Assert.Equal(2, matchInContext.Prompts.Count);
    }

    [Fact]
    public async Task ShouldNotReturnDuplicateNumbersForMatch()
    {
        var match = await SetUpGameAndMatch(3, 1, 4);

        var prompt = await _service.GetMatchPrompt(match, true);
        Assert.NotNull(prompt);

        var matchInContext = await _context.Matches.Include(m => m.Prompts)
            .FirstOrDefaultAsync(m => m.Id == match.Id);
        Assert.NotNull(matchInContext);
        Assert.Equal(4, matchInContext.Prompts.Count);

        var promptNumbers = matchInContext.Prompts.Select(p => p.Number).ToList();
        Assert.Equal(4, promptNumbers.Last());
    }

    private async Task<Match> SetUpGameAndMatch(int numberOfPrompts = 0, int minimumNumber = 1, int maximumNumber = 100)
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
            MinimumNumber = minimumNumber,
            MaximumNumber = maximumNumber,
        };

        for (int i = 1; i <= numberOfPrompts; i++)
        {
            var prompt = new Prompt
            {
                Id = Guid.NewGuid(),
                MatchId = match.Id,
                Number = i,
                IsCorrect = true,
            };
            match.Prompts.Add(prompt);
        }

        _context.Games.Add(game);
        _context.Matches.Add(match);
        await _context.SaveChangesAsync();

        return match;
    }
}