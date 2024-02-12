using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using BookBook.DTOs;
using BookBook.DTOs.DataTransferObject;
using BookBook.Models;
using BookBook.Models.ConfigurationModels;
using Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BookBook.Service.Implement
{
    public class UserManagementService : IUserManagementService
    {
        //private readonly JwtConfiguration _jwtConfiguration;
        private readonly JwtConfiguration _jwtOptionPattern;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        //private readonly IConfiguration _configuration;
        // Compare : Binding configuration vs Option Pattern
        //private readonly IOptions<JwtConfiguration> _options;
        //private readonly IOptionsSnapshot<JwtConfiguration> _options;
        private readonly IOptionsMonitor<JwtConfiguration> _options;
        public UserManagementService(ILoggerManager loggerManager, IMapper mapper, UserManager<User> userManager,
            //IConfiguration configuration,
            // Compare : Binding configuration vs Option Pattern
            //IOptions<JwtConfiguration> options,
            //IOptionsSnapshot<JwtConfiguration> options,
            IOptionsMonitor<JwtConfiguration> options
            )
        {
            _logger = loggerManager;
            _mapper = mapper;
            _userManager = userManager;
            //_configuration = configuration;

            //_jwtConfiguration = new JwtConfiguration();
            //_configuration.Bind(_jwtConfiguration.Section, _jwtConfiguration);
            // Compare : Binding configuration vs Option Pattern
            _options = options;
            // _jwtOptionPattern = _options.CurrentValue; // = Value for IOptions and IOptionsSnapshot
            _jwtOptionPattern = _options.Get("JwtSettingsV2");
        }

        public async Task<IdentityResult> CreateUserAsync(CreateUserRequest request)
        {
            var user = _mapper.Map<User>(request);
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, request.Roles);
            return result;
        }
        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            //Check valid
            var user = await _userManager.FindByNameAsync(request.UserName);
            bool result = (user != null && await _userManager.CheckPasswordAsync(user, request.Password));
            if (result is false)
            {
                _logger.LogWarn($"{nameof(LoginRequest)}: Authentication failed. Wrong username or password.");
                return null;
            }
            var response = await CreateToken(user, populatedExp: true);
            return response;
        }

        public async Task<LoginResponse?> RefreshTokenAsync(TokenDto request)
        {
            var principal = GetPrincipalFromExpiredToken(request.AccessToken);
            var user = await _userManager.FindByNameAsync(principal.Identity.Name);
            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpireTime <= DateTime.Now)
                throw new RefreshTokenBadRequest();

            return await CreateToken(user, populatedExp: false);
        }
        #region Private
        private async Task<LoginResponse?> CreateToken(User user, bool populatedExp)
        {
            //Generate AccessTtoken
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            //Generate RefreshToKen
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            if (populatedExp is true)
            {
                user.RefreshTokenExpireTime = DateTime.Now.AddDays(7);
            }
            await _userManager.UpdateAsync(user);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
            //var key = Encoding.UTF8.GetBytes("DuongPhamTuongVi6b0ff2b9-bf16-11ee-a8c1-0242ac110002SecretKey");
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;

        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            //var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
                // issuer: jwtSettings["ValidIssuer"],
                // audience: jwtSettings["ValidAudience"],

                //Binding configuration
                // issuer: _jwtConfiguration.ValidIssuer,
                // audience: _jwtConfiguration.ValidAudience,

                //Option pattern
                issuer: _jwtOptionPattern.ValidIssuer,
                audience: _jwtOptionPattern.ValidAudience,


                claims: claims,
                // expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["Expires"])),
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtOptionPattern.Expires)),
                signingCredentials: signingCredentials
            );
            return tokenOptions;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            //var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"))),
                ValidateLifetime = true,
                // ValidIssuer = jwtSettings["validIssuer"],
                // ValidAudience = jwtSettings["validAudience"]

                // Binding Configuration
                // ValidIssuer = _jwtConfiguration.ValidIssuer,
                // ValidAudience = _jwtConfiguration.ValidAudience

                //Option Pattern
                ValidIssuer = _jwtOptionPattern.ValidIssuer,
                ValidAudience = _jwtOptionPattern.ValidAudience

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null ||
           !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
            StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;

        }
        #endregion
    }
}