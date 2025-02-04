using System.ComponentModel.DataAnnotations;

namespace FizzBuzzFlex.EF.Entities;

public class Game
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(500)]
    public required string Author { get; set; }

    public DateTime CreatedDate { get; set; }

    public List<DivisorLabel> DivisorLabels { get; set; } = [];
}
