using BookBook.Models.Exceptions;

namespace BookBook.Models;

public class MaxAgeRangeBadRequestException : BadRequestException
{
    public MaxAgeRangeBadRequestException() : base("Max year can't be less than min age.")
    {
    }
}
