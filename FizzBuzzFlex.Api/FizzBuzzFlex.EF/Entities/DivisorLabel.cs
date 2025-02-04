using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FizzBuzzFlex.EF.Entities
{
    public class DivisorLabel
    {
        public Guid Id { get; set; }

        public int Divisor { get; set; }

        [MaxLength(50)]
        public required string Label { get; set; }

        public int Order { get; set; }

        [ForeignKey("GameId")]
        public Guid GameId { get; set; }
    }
}