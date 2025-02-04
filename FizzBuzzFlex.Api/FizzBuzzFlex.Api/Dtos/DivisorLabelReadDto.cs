namespace FizzBuzzFlex.Api.Dtos;

public class DivisorLabelReadDto
{
    public Guid Id { get; set; }

    public int Divisor { get; set; }

    public required string Label { get; set; }

    public int Order { get; set; }
}
