using BookBook.DTOs;
using BookBook.DTOs.DataTransferObject;
using Microsoft.AspNetCore.Identity;

namespace BookBook.Service
{
    public interface IUserManagementService
    {
        Task<IdentityResult> CreateUserAsync(CreateUserRequest request);
        Task<string?> LoginAsync(LoginRequest request);
    }
}