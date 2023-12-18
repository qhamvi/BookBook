using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Models.Exceptions
{
    public class IdParametersBadRequestException : BadRequestException
    {
        public IdParametersBadRequestException() : base("Parameters ids are null.")
        {
        }
    }
}