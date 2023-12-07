using BookBook.DTOs.DataTransferObject;
using BookBook.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace BookBook.Presentation.Controllers
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string? StatusDesc {  get; set; }
        public string? Message {  get; set; }
    }
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private IServiceManager _serviceManager;
        public AuthorsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [SwaggerOperation(Summary = "Get All Authors", Description = "Get all author in MySQL database", OperationId = nameof(GetAllAuthors))]
        [HttpGet("all")]
        [ProducesResponseType(typeof(List<AuthorDto>), 200)]
        public IActionResult GetAllAuthors()
        {
            var authors = _serviceManager.AuthorService.GetAllAuthors(trackChanges: false);
            return Ok(authors);
        }

        /// <summary>
        /// Get author By authorId
        /// </summary>
        [HttpGet("{authorId:Guid}")]
        [SwaggerOperation(Summary = "Get Author By AuthorId", Description = "Get author by authorId in MySQL database", OperationId = nameof(GetAuthor))]
        [ProducesResponseType(typeof(AuthorDto), 200)]
        public IActionResult GetAuthor(Guid authorId)
        {
            var author = _serviceManager.AuthorService.GetAuthor(authorId, trackChanges: false);
            return Ok(author);
        }
        
    }
}