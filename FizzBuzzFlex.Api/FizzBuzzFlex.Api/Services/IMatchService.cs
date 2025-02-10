using FizzBuzzFlex.Api.Dtos;
using FizzBuzzFlex.EF.Entities;

namespace FizzBuzzFlex.Api.Services;

public interface IMatchService
{
    Task<RoundResponseDto> StartMatch(MatchWriteDto matchWriteDto);

    Task<RoundResponseDto> CheckMatchPrompt(RoundAnswerDto roundAnswer);

    Task<RoundResponseDto> GetMatchPrompt(Match match, bool? previousRoundResult);
}
