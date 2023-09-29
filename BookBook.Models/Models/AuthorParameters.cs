
namespace BookBook.Models.Models
{
    public class AuthorParameters : QueryStringParameters
    {
        //uint = int > 0
        public uint MinYearOfBirth {get; set;}
        public uint MaxYearOfBirth {get; set;} = (uint)DateTime.Now.Year;
        public bool ValidYearRange => MaxYearOfBirth > MinYearOfBirth;
    }
}