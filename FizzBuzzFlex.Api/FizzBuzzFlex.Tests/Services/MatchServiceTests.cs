using System.Threading.Tasks;
using FizzBuzzFlex.Api.Dtos;
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
    public async Task ShouldStartMatchWithFirstPrompt()
    {
        await SetUpGame();

        var dto = new MatchWriteDto { Id = Guid.NewGuid() };
        var prompt = await _service.StartMatch(dto);
        Assert.NotNull(prompt);

        var matchInContext = await _context.Matches.Include(m => m.Prompts)
            .FirstOrDefaultAsync(m => m.Id == dto.Id);
        Assert.NotNull(matchInContext);
        Assert.Single(matchInContext.Prompts);
    }

    [Fact]
    public async Task ShouldCreateFollowUpPrompt()
    {
        var match = await SetUpGameAndMatch([1]);

        var prompt = await _service.GetMatchPrompt(match, true);
        Assert.NotNull(prompt);

        var matchInContext = await _context.Matches.Include(m => m.Prompts)
            .FirstOrDefaultAsync(m => m.Id == match.Id);
        Assert.NotNull(matchInContext);
        Assert.Equal(2, matchInContext.Prompts.Count);
    }

    [Fact]
    public async Task ShouldNotReturnDuplicateNumbers()
    {
        var match = await SetUpGameAndMatch([1, 2, 3], 1, 4);

        var prompt = await _service.GetMatchPrompt(match, true);
        Assert.NotNull(prompt);

        var matchInContext = await _context.Matches.Include(m => m.Prompts)
            .FirstOrDefaultAsync(m => m.Id == match.Id);
        Assert.NotNull(matchInContext);
        Assert.Equal(4, matchInContext.Prompts.Count);

        var promptNumbers = matchInContext.Prompts.Select(p => p.Number).ToList();
        Assert.Equal(4, promptNumbers.Last());
    }

    [Fact]
    public async Task ShouldCheckFizzBuzzAnswer()
    {
        var match = await SetUpGameAndMatch([15]);
        var roundAnswer = new RoundAnswerDto
        {
            MatchId = match.Id,
            PromptId = match.Prompts.First().Id,
            Answer = "FizzBuzz",
        };

        var roundResponse = await _service.CheckMatchPrompt(roundAnswer);
        Assert.True(roundResponse.PreviousRoundResult);

        var matchInContext = await _context.Matches.Include(m => m.Prompts)
            .FirstOrDefaultAsync(m => m.Id == match.Id);
        Assert.NotNull(matchInContext);
        var promptInMatch = matchInContext.Prompts.First();
        Assert.True(promptInMatch.IsCorrect);
    }

    [Fact]
    public async Task ShouldCheckBuzzAnswer()
    {
        var match = await SetUpGameAndMatch([20]);
        var roundAnswer = new RoundAnswerDto
        {
            MatchId = match.Id,
            PromptId = match.Prompts.First().Id,
            Answer = "Buzz",
        };

        var roundResponse = await _service.CheckMatchPrompt(roundAnswer);
        Assert.True(roundResponse.PreviousRoundResult);

        var matchInContext = await _context.Matches.Include(m => m.Prompts)
            .FirstOrDefaultAsync(m => m.Id == match.Id);
        Assert.NotNull(matchInContext);
        var promptInMatch = matchInContext.Prompts.First();
        Assert.True(promptInMatch.IsCorrect);
    }

    [Fact]
    public async Task ShouldCheckNumberAnswer()
    {
        var match = await SetUpGameAndMatch([22]);
        var roundAnswer = new RoundAnswerDto
        {
            MatchId = match.Id,
            PromptId = match.Prompts.First().Id,
            Answer = "22",
        };

        var roundResponse = await _service.CheckMatchPrompt(roundAnswer);
        Assert.True(roundResponse.PreviousRoundResult);

        var matchInContext = await _context.Matches.Include(m => m.Prompts)
            .FirstOrDefaultAsync(m => m.Id == match.Id);
        Assert.NotNull(matchInContext);
        var promptInMatch = matchInContext.Prompts.First();
        Assert.True(promptInMatch.IsCorrect);
    }

    [Fact]
    public async Task ShouldFailWrongAnswer()
    {
        var match = await SetUpGameAndMatch([25]);
        var roundAnswer = new RoundAnswerDto
        {
            MatchId = match.Id,
            PromptId = match.Prompts.First().Id,
            Answer = "Fizz",
        };

        var roundResponse = await _service.CheckMatchPrompt(roundAnswer);
        Assert.False(roundResponse.PreviousRoundResult);

        var matchInContext = await _context.Matches.Include(m => m.Prompts)
            .FirstOrDefaultAsync(m => m.Id == match.Id);
        Assert.NotNull(matchInContext);
        var promptInMatch = matchInContext.Prompts.First();
        Assert.False(promptInMatch.IsCorrect);
    }

    private async Task SetUpGame()
    {
        var game = new Game
        {
            Id = Guid.NewGuid(),
            Name = "FizzBuzz",
            Author = "Andres",
        };
        _context.Games.Add(game);
        await _context.SaveChangesAsync();
    }

    private async Task<Match> SetUpGameAndMatch(List<int>? promptNumbers = null, int minimumNumber = 1, int maximumNumber = 100)
    {
        var game = new Game
        {
            Id = Guid.NewGuid(),
            Name = "FizzBuzz",
            Author = "Andres",
            DivisorLabels =
            [
                new() { Id = Guid.NewGuid(), Divisor = 3, Label = "Fizz" },
                new() { Id = Guid.NewGuid(), Divisor = 5, Label = "Buzz" },
            ],
        };
        var match = new Match
        {
            Id = Guid.NewGuid(),
            GameId = game.Id,
            DurationInSeconds = 60,
            MinimumNumber = minimumNumber,
            MaximumNumber = maximumNumber,
        };

        foreach (var number in promptNumbers ?? [])
        {
            var prompt = new Prompt
            {
                Id = Guid.NewGuid(),
                MatchId = match.Id,
                Number = number,
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