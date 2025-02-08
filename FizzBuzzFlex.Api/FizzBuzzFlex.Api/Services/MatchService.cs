using FizzBuzzFlex.Api.Dtos;
using FizzBuzzFlex.EF.Context;
using FizzBuzzFlex.EF.Entities;

namespace FizzBuzzFlex.Api.Services;

public class MatchService : IMatchService
{
    private readonly DatabaseContext _context;

    public MatchService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<RoundResponse> GetMatchPrompt(Match match, bool previousRoundResult)
    {
        var usedNumbers = match.Prompts.Select(p => p.Number).ToList();

        var random = new Random();
        int attemptedNumber;
        do
        {
            attemptedNumber = random.Next(match.MinimumNumber, match.MaximumNumber + 1);
        } while (usedNumbers.Contains(attemptedNumber));

        var newPrompt = new Prompt
        {
            Id = Guid.NewGuid(),
            Number = attemptedNumber,
        };
        match.Prompts.Add(newPrompt);
        await _context.SaveChangesAsync();

        return new RoundResponse
        {
            RoundNumber = match.Prompts.Count,
            PreviousRoundResult = previousRoundResult,
            Prompt = attemptedNumber,
        };
    }
}