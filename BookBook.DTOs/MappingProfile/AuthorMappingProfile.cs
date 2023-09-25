using AutoMapper;
using BookBook.Models.Models;

namespace BookBook.DTOs;

public class AuthorMappingProfile : Profile
{
    public AuthorMappingProfile()
    {
        CreateMap<Author, AuthorDto>();
    }
}
