using System.ComponentModel.DataAnnotations;

namespace FizzBuzzFlex.EF.Entities;

public class Match
{
    [Key]
    public Guid Id { get; set; }

    public Guid GameId { get; set; }

    public Game Game { get; set; } = null!;

    public int DurationInSeconds { get; set; }

    public int MinimumNumber { get; set; }

    public int MaximumNumber { get; set; }

    public DateTime CreatedDate { get; set; }

    public List<Prompt> Prompts { get; set; } = [];
}
