using AutoMapper;
using BookBook.DTOs.DataTransferObject;
using BookBook.Models.Models;

namespace BookBook.DTOs.MappingProfile;

public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateBookDto, Book>();
    }
}
