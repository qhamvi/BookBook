namespace BookBook.DTOs;

public class AuthorDto
{
        public Guid Id {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string PhoneNumber {get; set;}
        public DateTime DayOfBirth {get; set;}
        public string Country {get; set;}
        //Additional
        public IEnumerable<BookDto> Books {get; set;}

}
