namespace FizzBuzzFlex.Api.Dtos;

public class GameReadDto
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Author { get; set; }

    public DateTime CreatedDate { get; set; }

    public List<DivisorLabelReadDto> DivisorLabels { get; set; } = [];
}
