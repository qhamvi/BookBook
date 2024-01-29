using AutoMapper;
using BookBook.DTOs.DataTransferObject;
using BookBook.Models;
using Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace BookBook.Service.Implement
{
    public class UserManagementService : IUserManagementService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        public UserManagementService(ILoggerManager loggerManager, IMapper mapper, UserManager<User> userManager,
            IConfiguration configuration)
        {
            _logger = loggerManager;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> CreateUserAsync(CreateUserRequest request)
        {
            var user = _mapper.Map<User>(request);
            var result = await _userManager.CreateAsync(user, request.Password);
            if(result.Succeeded)
                await _userManager.AddToRolesAsync(user, request.Roles);
            return result;
        }
    }
}