namespace BookBook.DTOs.DataTransferObject;

public record AuthorDto
{

        public Guid Id {get; init;}
        public string? FirstName {get; init;}
        public string? LastName {get; init;}
        public string? FullName {get; init;}
        public string? PhoneNumber {get; init;}
        public DateTime DayOfBirth {get; init;}
        public string? Country {get; init;}
        //Additional
        //public IEnumerable<BookDto> Books {get; set;}

}
