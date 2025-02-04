using FizzBuzzFlex.Api.Dtos;
using FizzBuzzFlex.EF.Entities;

namespace FizzBuzzFlex.Api.Projections;

public static class DivisorLabelProjections
{
    public static DivisorLabelReadDto ToReadDto(this DivisorLabel label) => new()
    {
        Id = label.Id,
        Divisor = label.Divisor,
        Label = label.Label,
        Order = label.Order,
    };
}
