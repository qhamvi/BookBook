using AutoMapper;
using BookBook.Models.Models;

namespace BookBook.DTOs;

public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        CreateMap<Book, BookDto>();
    }
}
