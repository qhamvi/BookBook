using AutoMapper;
using BookBook.DTOs.DataTransferObject;
using BookBook.Models;

namespace BookBook.DTOs.MappingProfile
{
    public class UserManagementMappingProfile : Profile
    {
        public UserManagementMappingProfile()
        {
            CreateMap<CreateUserRequest, User>();
        }
    }
}