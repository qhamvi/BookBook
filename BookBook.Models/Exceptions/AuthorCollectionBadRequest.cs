using BookBook.Models.Exceptions;

namespace BookBook.Models;

public class AuthorCollectionBadRequest : BadRequestException
{
    public AuthorCollectionBadRequest() : base("Author collection sent from a client is null.")
    {
        
    }
}
