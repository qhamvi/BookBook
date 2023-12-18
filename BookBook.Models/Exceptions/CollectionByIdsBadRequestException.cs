using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Models.Exceptions
{
    public class CollectionByIdsBadRequestException : BadRequestException
    {
        public CollectionByIdsBadRequestException() : base("Collection count mismatch comparing to ids.")
        {
        }
    }
}