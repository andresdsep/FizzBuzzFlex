using FizzBuzzFlex.Api.Dtos;
using FizzBuzzFlex.EF.Entities;

namespace FizzBuzzFlex.Api.Services;

public interface IMatchService
{
    Task<RoundResponse> StartMatch(MatchWriteDto matchWriteDto);

    Task<RoundResponse> CheckMatchPrompt(RoundAnswer roundAnswer);

    Task<RoundResponse> GetMatchPrompt(Match match, bool previousRoundResult);
}
