using BookBook.Models.Exceptions;

namespace BookBook.Models;

public sealed class RefreshTokenBadRequest : BadRequestException
{
    public RefreshTokenBadRequest() : base("Invalid client request. The token has some invalid value.")
    {
    }
}
