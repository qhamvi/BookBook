namespace BookBook.DTOs.DataTransferObject;

public class AuthorDto
{
        public AuthorDto(string fullName)
        {
            _fullName = fullName;
        }
        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
        }
        public Guid Id {get; set;}
        public string? FirstName {get; set;}
        public string? LastName {get; set;}
        public string? PhoneNumber {get; set;}
        public DateTime DayOfBirth {get; set;}
        public string? Country {get; set;}
        //Additional
        public IEnumerable<BookDto> Books {get; set;}

}
