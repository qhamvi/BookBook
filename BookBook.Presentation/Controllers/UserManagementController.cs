using BookBook.DTOs.DataTransferObject;
using BookBook.Service;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookBook.Presentation.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserManagementController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public UserManagementController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        /// <summary>
        /// Create user 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost]
        [SwaggerOperation(Summary = "Create user", Description = "Create User in MySQL database", OperationId = nameof(CreateUser))]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var result = await _serviceManager.UserManagement.CreateUserAsync(request);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return StatusCode(201);
        }
    }
}