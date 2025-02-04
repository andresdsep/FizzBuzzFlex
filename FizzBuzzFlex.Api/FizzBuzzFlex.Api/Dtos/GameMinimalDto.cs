namespace FizzBuzzFlex.Api.Dtos;

public class GameMinimalDto
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Author { get; set; }

    public DateTime CreatedDate { get; set; }
}
