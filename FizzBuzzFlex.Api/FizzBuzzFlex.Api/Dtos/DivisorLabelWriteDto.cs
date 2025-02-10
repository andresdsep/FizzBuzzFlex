using System.ComponentModel.DataAnnotations;

namespace FizzBuzzFlex.Api.Dtos;

public class DivisorLabelWriteDto
{
    public Guid Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Divisor must be a positive number.")]
    public int Divisor { get; set; }

    public required string Label { get; set; }
}
