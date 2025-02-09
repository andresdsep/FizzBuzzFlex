namespace FizzBuzzFlex.Api.Dtos;

public class RoundResponse
{
    public int RoundNumber { get; set; }

    public bool PreviousRoundResult { get; set; }

    public Guid PromptId { get; set; }

    public int PromptNumber { get; set; }
}