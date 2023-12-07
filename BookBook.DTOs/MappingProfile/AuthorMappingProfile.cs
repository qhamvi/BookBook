using AutoMapper;
using BookBook.DTOs.DataTransferObject;
using BookBook.Models.Models;

namespace BookBook.DTOs.MappingProfile;

public class AuthorMappingProfile : Profile
{
    public AuthorMappingProfile()
    {
        CreateMap<Author, AuthorDto>()
            .ForMember(v => v.FullName, opt => opt.MapFrom(v => string.Join(' ', v.FirstName, v.LastName)));
            // .ForCtorParam(nameof(AuthorDto.FullName), opt => opt.MapFrom(v => string.Join(' ', v.FirstName, v.LastName)));
        CreateMap<CreateAuthorDto, Author>();
        CreateMap<UpdateAuthorDto, Author>();
    }
}
