using System.ComponentModel.DataAnnotations;

namespace FizzBuzzFlex.EF.Entities;

public class Prompt
{
    [Key]
    public Guid Id { get; set; }

    public int Number { get; set; }

    public bool? IsCorrect { get; set; }

    public Guid MatchId { get; set; }
}
