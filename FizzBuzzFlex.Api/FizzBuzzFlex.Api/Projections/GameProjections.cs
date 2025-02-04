using FizzBuzzFlex.Api.Dtos;
using FizzBuzzFlex.EF.Entities;

namespace FizzBuzzFlex.Api.Projections;

public static class GameProjections
{
    public static GameMinimalDto ToMinimalDto(this Game game) => new()
    {
        Id = game.Id,
        Name = game.Name,
        Author = game.Author,
        CreatedDate = game.CreatedDate,
    };

    public static GameReadDto ToReadDto(this Game game) => new()
    {
        Id = game.Id,
        Name = game.Name,
        Author = game.Author,
        CreatedDate = game.CreatedDate,
        DivisorLabels = [.. game.DivisorLabels.OrderBy(l => l.Order).Select(DivisorLabelProjections.ToReadDto)],
    };

    public static Game ToEntity(this GameWriteDto dto) => new()
    {
        Id = dto.Id,
        Name = dto.Name,
        Author = dto.Author,
        CreatedDate = DateTime.Now,
        DivisorLabels = [.. dto.DivisorLabels.Select((l, i) => l.ToEntity(i))],
    };
}
