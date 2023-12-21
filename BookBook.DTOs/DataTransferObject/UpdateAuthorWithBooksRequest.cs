namespace BookBook.DTOs;

public class UpdateAuthorWithBooksRequest
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public DateTime DayOfBirth { get; init; }
    public string Country { get; init; }
    public string PhoneNumber { get; init; }
    public IEnumerable<CreateBookDto> Books { get; init; }
}
