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

        bool isCorrect = CheckAnswer(roundAnswer, prompt.Number, match.Game.DivisorLabels);
        prompt.IsCorrect = isCorrect;

        return await GetMatchPrompt(match, isCorrect);
    }

    private static bool CheckAnswer(RoundAnswerDto roundAnswer, int promptNumber, List<DivisorLabel> divisorLabels)
    {
        var hasDivisor = false;
        foreach (var divisorLabel in divisorLabels)
        {
            var isDivisible = (promptNumber % divisorLabel.Divisor) == 0;
            if (isDivisible)
            {
                if (!roundAnswer.Answer.Contains(divisorLabel.Label, StringComparison.InvariantCultureIgnoreCase))
                    return false;
                hasDivisor = true;
            }
            else
                if (roundAnswer.Answer.Contains(divisorLabel.Label, StringComparison.InvariantCultureIgnoreCase))
                    return false;
        }

        return hasDivisor || roundAnswer.Answer.Contains(promptNumber.ToString());
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
            PromptId = newPrompt.Id,
            PromptNumber = attemptedNumber,
        };
    }

    public async Task<MatchResultsDto> GetMatchResults(Guid matchId)
    {
        var match = await _context.Matches
            .Include(m => m.Prompts)
            .SingleOrDefaultAsync(m => m.Id == matchId);
        if (match is null)
            throw new ArgumentException("matchId didn't match a match");

        return new MatchResultsDto
        {
            CorrectAnswers = match.Prompts.Count(p => p.IsCorrect == true),
            IncorrectAnswers = match.Prompts.Count(p => p.IsCorrect == false),
        };
    }
}