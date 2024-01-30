using BookBook.DTOs;
using BookBook.DTOs.DataTransferObject;
using BookBook.Service;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookBook.Presentation.Controllers
{
    [ApiController]
    [Route("authenticate")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public AuthenticationController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        /// <summary>
        /// Login user 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [SwaggerOperation(Summary = "Login", Description = "Login", OperationId = nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _serviceManager.UserManagement.LoginAsync(request);
            if (result is null)
                return Unauthorized();

            return Ok(new {Token = result});
        }
    }
}