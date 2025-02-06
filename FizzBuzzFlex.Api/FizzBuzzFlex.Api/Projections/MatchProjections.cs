using FizzBuzzFlex.Api.Dtos;
using FizzBuzzFlex.EF.Entities;

namespace FizzBuzzFlex.Api.Projections;

public static class MatchProjections
{
    public static Match ToEntity(this MatchWriteDto m) => new()
    {
        Id = m.Id,
        GameId = m.GameId,
        DurationInSeconds = m.DurationInSeconds,
    };
}