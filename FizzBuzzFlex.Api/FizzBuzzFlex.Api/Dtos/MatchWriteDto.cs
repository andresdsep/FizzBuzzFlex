namespace FizzBuzzFlex.Api.Dtos;

public class MatchWriteDto
{
    public Guid Id { get; set; }

    public Guid GameId { get; set; }

    public int DurationInSeconds { get; set; }

    public int MinimumNumber { get; set; }

    public int MaximumNumber { get; set; }
}
