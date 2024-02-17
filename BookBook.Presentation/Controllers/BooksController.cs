using BookBook.DTOs;
using BookBook.DTOs.DataTransferObject;
using BookBook.Service;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookBook.Presentation.Controllers
{
    [ApiController]
    [Route("books")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class BooksController : ControllerBase
    {
        private IServiceManager _serviceManager;
        public BooksController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [HttpGet]
        [SwaggerOperation(Summary = "Get all book", Description = "Get all book in MySQL database", OperationId = nameof(GetBooks))]
        [ProducesResponseType(typeof(IEnumerable<BookDto>), 200)]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _serviceManager.BookService.GetBooksAsync(trackChanges: false);
            return Ok(books);
        }


        [HttpGet("of-author/{authorId}")]
        [SwaggerOperation(Summary = "Get all book of author", Description = "Get all book for Author in MySQL database", OperationId = nameof(GetAllBookForAuthor))]
        [ProducesResponseType(typeof(IEnumerable<BookDto>), 200)]
        public async Task<IActionResult> GetAllBookForAuthor(Guid authorId)
        {
            var books = await _serviceManager.BookService.GetAllBookForAuthorAsync(authorId, trackChanges: false);
            return Ok(books);
        }

        [HttpGet("{id:guid}", Name = "GetBookForAuthor")]
        [SwaggerOperation(Summary = "Get Book For Author", Description = "Get Book for Author in MySQL database", OperationId = nameof(GetBookForAuthor))]
        [ProducesResponseType(typeof(BookDto), 200)]
        public async Task<IActionResult> GetBookForAuthor(Guid authorId, Guid id)
        {
            var book = await _serviceManager.BookService.GetBookForAuthor(authorId, id, trackChanges: false);
            return Ok(book);
        }


        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [SwaggerOperation(Summary = "Create Book For Author", Description = "Create Book for Author in MySQL database", OperationId = nameof(CreatebookForAuhtor))]
        public async Task<IActionResult> CreatebookForAuhtor(Guid authorId, [FromBody] CreateBookDto book)
        {
           
            var response = await _serviceManager.BookService.CreateBookForAuthorAsync(authorId, book, trackChanges: false);
            return CreatedAtRoute("GetBookForAuthor", new
            {
                authorId,
                id = response.Id
            }, response);
        }

        [HttpDelete("{bookId:guid}")]
        [SwaggerOperation(Summary = "Delete Book for Author", Description = "Delete book for Author in MySQL Database", OperationId = nameof(DeleteBookForAuthor))]
        public async Task<IActionResult> DeleteBookForAuthor(Guid authorId, Guid bookId)
        {
            await _serviceManager.BookService.DeleteBookForAuthor(authorId, bookId, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{bookId:Guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [SwaggerOperation(Summary = "Update Book for Author", Description = "Update book for Author in MySQL Database", OperationId = nameof(UpdateBookForAuthor))]
        public async Task<IActionResult> UpdateBookForAuthor(Guid authorId, Guid bookId, [FromBody] UpdateBookDto bookDto)
        {           
            await _serviceManager.BookService.UpdateBookForAuthor(authorId, bookId, bookDto, auTrackChanges: false, bookTrackChanges: true);
            return NoContent();
        }

        [HttpPatch("{bookId:Guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [SwaggerOperation(Summary = "Update partially Book for Author", Description = "Update partially book for Author in MySQL Database", OperationId = nameof(PartiallyUpdateBookForAuthor))]

        public async Task<IActionResult> PartiallyUpdateBookForAuthor(Guid authorId, Guid bookId, [FromBody] JsonPatchDocument<UpdateBookDto> patchBookDto)
        {
            await _serviceManager.BookService.PartiallyUpdateBookForAuthor(authorId, bookId, patchBookDto, auTrackChanges: false, bookTrackChanges: true);
            return NoContent();
        }

    }
}