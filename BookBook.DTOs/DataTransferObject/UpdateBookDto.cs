using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.DTOs.DataTransferObject
{
    public class UpdateBookDto
    {
        public string BookName { get; init; }
        public int Price { get; init; }
    }
}