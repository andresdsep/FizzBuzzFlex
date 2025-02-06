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

    public async Task<RoundResponse> GetMatchPrompt(Match match)
    {
        await Task.CompletedTask;
        var roundResponse = new RoundResponse
        {
            RoundNumber = 1,
            PreviousRoundResult = false,
            Prompt = 1,
        };
        return roundResponse;
    }
}