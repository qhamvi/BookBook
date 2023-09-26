using AutoMapper;
using BookBook.DTOs.DataTransferObject;
using BookBook.Models.Models;

namespace BookBook.DTOs;

public class AuthorMappingProfile : Profile
{
    public AuthorMappingProfile()
    {
        CreateMap<Author, AuthorDto>();
        CreateMap<CreateAuthorDto, Author>();
        CreateMap<UpdateAuthorDto, Author>();
    }
}
