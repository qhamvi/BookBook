namespace BookBook.DTOs;

public record LoginResponse
{
    public string? AccessToken {get; init;}
    public string? RefreshToKen {get; init;}
}
