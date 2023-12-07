
namespace BookBook.Models.Exceptions
{
    public class AuthorNotFoundException : NotFoundException
    {
        public AuthorNotFoundException(Guid authorId) : base($"Athor with Id = {authorId} is not found")
        {
            
        }
    }
}
