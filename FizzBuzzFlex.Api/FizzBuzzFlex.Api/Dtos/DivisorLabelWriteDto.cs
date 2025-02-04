namespace FizzBuzzFlex.Api.Dtos;

public class DivisorLabelWriteDto
{
    public Guid Id { get; set; }

    public int Divisor { get; set; }

    public required string Label { get; set; }
}
