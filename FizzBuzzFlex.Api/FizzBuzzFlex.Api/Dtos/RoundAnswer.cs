namespace FizzBuzzFlex.Api.Dtos;

public class RoundAnswer
{
    public Guid MatchId { get; set; }

    public Guid PromptId { get; set; }

    public required string Answer { get; set; }
}
