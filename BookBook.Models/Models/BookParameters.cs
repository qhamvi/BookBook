
namespace BookBook.Models.Models
{
    public class BookParameters : QueryStringParameters
    {
        public BookParameters()
        {
            OrderBy = "CreatedDate";
        }
    }
}