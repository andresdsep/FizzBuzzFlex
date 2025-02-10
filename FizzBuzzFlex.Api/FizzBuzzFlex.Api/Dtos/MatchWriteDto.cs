using System.ComponentModel.DataAnnotations;

namespace FizzBuzzFlex.Api.Dtos;

public class MatchWriteDto
{
    public Guid Id { get; set; }

    public Guid GameId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "DurationInSeconds must be a positive number.")]
    public int DurationInSeconds { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "MinimumNumber must be a positive number.")]
    public int MinimumNumber { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "MaximumNumber must be a positive number.")]
    public int MaximumNumber { get; set; }
}
