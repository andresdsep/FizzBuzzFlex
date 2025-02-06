namespace FizzBuzzFlex.Api.Dtos;

public class RoundResponse
{
    public int RoundNumber { get; set; }

    public bool PreviousRoundResult { get; set; }

    public int Prompt { get; set; }
}