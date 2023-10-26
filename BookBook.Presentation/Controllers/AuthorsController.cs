using BookBook.Service;
using Microsoft.AspNetCore.Mvc;

namespace BookBook.Presentation.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private IServiceManager _serviceManager;
        public AuthorsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("all")]
        public IActionResult GetAllAuthors()
        {
            throw new Exception("Quang loi");
            var authors = _serviceManager.AuthorService.GetAllAuthors(trackChanges: false);
            return Ok(authors);
        }
    }
}