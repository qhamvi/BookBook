using BookBook.DTOs;
using BookBook.DTOs.DataTransferObject;
using BookBook.Service;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookBook.Presentation.Controllers
{

    [Route("api/authors")]
    [ApiController]
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
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _serviceManager.AuthorService.GetAllAuthorsAsync(trackChanges: false);
            return Ok(authors);
        }

        /// <summary>
        /// Get author By authorId
        /// </summary>
        [HttpGet("{authorId:Guid}")]
        [SwaggerOperation(Summary = "Get Author By AuthorId", Description = "Get author by authorId in MySQL database", OperationId = nameof(GetAuthor))]
        [ProducesResponseType(typeof(AuthorDto), 200)]
        public async Task<IActionResult> GetAuthor(Guid authorId)
        {
            var author = await _serviceManager.AuthorService.GetAuthorAsync(authorId, trackChanges: false);
            return Ok(author);
        }

        /// <summary>
        /// Create a author
        /// </summary>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [SwaggerOperation(Summary = "Create a author", Description = "Create a author in MySQL database", OperationId = nameof(CreateAuthor))]
        [ProducesResponseType(typeof(AuthorDto), 200)]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDto authorDto)
        {
            var author = await _serviceManager.AuthorService.CreateAuthorAsync(authorDto);
            return CreatedAtAction("GetAuthorAsync", new { authorId = author.Id }, author);
        }
        /// <summary>
        /// Get Author Collection
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet("collection/({ids})", Name = "GetAuthorCollection")]
        [SwaggerOperation(Summary = "Get author collection", Description = "Get author collection in MySQL database", OperationId = nameof(CreateAuthor))]
        public async Task<IActionResult> GetAuthorCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            var authors = await _serviceManager.AuthorService.GetByIdsAsync(ids, trackChanges: false);
            return Ok(authors);
        }

        ///<summary>
        ///Create a author collection
        ///</summary>
        ///<param name="{authorDtos}"></param>
        ///<returns></returns>
        [HttpPost("collection")]
        [SwaggerOperation(Summary = "Create collection of author", Description = "Create author collection", OperationId = "")]
        public async Task<IActionResult> CreateAuthorCollection([FromBody] IEnumerable<CreateAuthorDto> authorDtos)
        {
            var result = await _serviceManager.AuthorService.CreateAuthorCollectionAsync(authorDtos);
            return CreatedAtAction("GetAuthorCollection", new {result.ids} , result.authorDtos);
        }

        ///<summary>
        ///Delete Author
        ///</summary>
        ///<param name="{authorId}"></param>
        ///<returns></returns>
        [HttpDelete("{authorId:Guid}")]
        public async Task<IActionResult> DeleteAuthor(Guid authorId)
        {
            await _serviceManager.AuthorService.DeleteAuthor(authorId, trackChanges: false);
            return NoContent();
        }

        ///<summary>
        ///Update author with book collection
        ///</summary>
        ///<param name=""></param>
        ///<returns></returns>
        [HttpPut("{authorId:Guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateAuthor(Guid authorId, [FromBody] UpdateAuthorWithBooksRequest authorDto)
        {            
            await _serviceManager.AuthorService.UpdateAuthor(authorId, authorDto, trackChanges: true);
            return NoContent();
        }

    }
}