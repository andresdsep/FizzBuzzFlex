namespace FizzBuzzFlex.Api.Dtos;

public class GameWriteDto
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Author { get; set; }

    public List<DivisorLabelWriteDto> DivisorLabels { get; set; } = [];
}
