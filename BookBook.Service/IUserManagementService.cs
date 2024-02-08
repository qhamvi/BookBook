using BookBook.DTOs;
using BookBook.DTOs.DataTransferObject;
using Microsoft.AspNetCore.Identity;

namespace BookBook.Service
{
    public interface IUserManagementService
    {
        Task<IdentityResult> CreateUserAsync(CreateUserRequest request);
        Task<LoginResponse?> LoginAsync(LoginRequest request);
        Task<LoginResponse?> RefreshTokenAsync(TokenDto request);

    }
}