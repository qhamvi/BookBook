using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.DTOs.DataTransferObject
{
    public record CreateAuthorWithBooksRequest
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public DateTime DayOfBirth { get; init; }
        public string Country { get; init; }
        public string PhoneNumber { get; init; }
        IEnumerable<CreateBookDto> BookDtos {get; init;}
    }
}