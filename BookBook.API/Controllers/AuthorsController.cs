using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BookBook.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private IRepositoryWrapper _repositoryWrapper;
        public AuthorsController(ILoggerManager logger, IRepositoryWrapper repositoryWrapper)
        {
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }
        [HttpGet("test")]
        public IActionResult Get()
        {
            _logger.LogInfor("Here is info message from the controller.");
            _logger.LogDebug("Here is debug message from the controller.");
            _logger.LogWarn("Here is warn message from the controller.");
            _logger.LogError("Here is error message from the controller.");
            return Ok("okey");
        }
        
        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            try
            {
                var authors = _repositoryWrapper.Author.GetAllAuthors();
                _logger.LogInfor($"Returned all authors from database.");

                return Ok(authors);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GET ALL AUTHOR");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}