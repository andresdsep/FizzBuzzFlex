using FizzBuzzFlex.Api.Dtos;
using FizzBuzzFlex.Api.Projections;
using FizzBuzzFlex.EF.Context;
using FizzBuzzFlex.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace FizzBuzzFlex.Api.Services;

public class MatchService : IMatchService
{
    private readonly DatabaseContext _context;

    public MatchService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<RoundResponseDto> StartMatch(MatchWriteDto matchWriteDto)
    {
        var newMatch = matchWriteDto.ToEntity();
        await _context.Matches.AddAsync(newMatch);
        await _context.SaveChangesAsync();

        return await GetMatchPrompt(newMatch, null);
    }

    public async Task<RoundResponseDto> CheckMatchPrompt(RoundAnswerDto roundAnswer)
    {
        var match = await _context.Matches
            .AsSplitQuery()
            .Include(m => m.Prompts)
            .Include(m => m.Game)
            .ThenInclude(g => g.DivisorLabels)
            .SingleOrDefaultAsync(m => m.Id == roundAnswer.MatchId);
        if (match is null)
            throw new ArgumentException("matchId didn't match a match");

        var prompt = match.Prompts.SingleOrDefault(p => p.Id == roundAnswer.PromptId);
        if (prompt is null)
            throw new ArgumentException("promptId didn't match a prompt");

        var correctAnswer = string.Empty;
        foreach (var divisorLabel in match.Game.DivisorLabels)
        {
            var isDivisible = (prompt.Number % divisorLabel.Divisor) == 0;
            if (isDivisible)
                correctAnswer += divisorLabel.Label;
        }

        if (correctAnswer == string.Empty)
            correctAnswer = prompt.Number.ToString();

        var isCorrect = string.Equals(correctAnswer, roundAnswer.Answer, StringComparison.OrdinalIgnoreCase);
        return await GetMatchPrompt(match, isCorrect);
    }

    public async Task<RoundResponseDto> GetMatchPrompt(Match match, bool? previousRoundResult)
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

        return new RoundResponseDto
        {
            RoundNumber = match.Prompts.Count,
            PreviousRoundResult = previousRoundResult,
            MatchId = match.Id,
            PromptId = newPrompt.Id,
            PromptNumber = attemptedNumber,
        };
    }
}