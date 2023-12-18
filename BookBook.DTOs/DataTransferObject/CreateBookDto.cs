namespace BookBook.DTOs;

public record CreateBookDto
{
    public string BookName { get; init; }
    public DateTime CreatedDate { get; init; }
    public int Price { get; init; }

}
