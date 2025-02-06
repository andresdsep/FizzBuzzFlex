using System.ComponentModel.DataAnnotations;

namespace FizzBuzzFlex.EF.Entities;

public class Match
{
    [Key]
    public Guid Id { get; set; }

    public Guid GameId { get; set; }

    public required Game Game { get; set; }

    public int DurationInSeconds { get; set; }

    public DateTime CreatedDate { get; set; }

    public List<Prompt> Prompts { get; set; } = [];
}
