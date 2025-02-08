using FizzBuzzFlex.Api.Dtos;
using FizzBuzzFlex.EF.Entities;

namespace FizzBuzzFlex.Api.Services;

public interface IMatchService
{
    Task<RoundResponse> GetMatchPrompt(Match match, bool previousRoundResult);
}
