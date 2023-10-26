namespace BookBook.DTOs.DataTransferObject;

public class BookDto
{
        public Guid Id {get; set;}
        public string BookName {get; set;}
        public DateTime CreatedDate {get; set;}
        public int Price {get; set;}
        
}
