using BookBook.DTOs;
using BookBook.DTOs.DataTransferObject;
using BookBook.Service;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookBook.Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
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
        public IActionResult GetBooks()
        {
            var books = _serviceManager.BookService.GetBooks(trackChanges: false);
            return Ok(books);
        }


        [HttpGet("of-author/{authorId}")]
        [SwaggerOperation(Summary = "Get all book of author", Description = "Get all book for Author in MySQL database", OperationId = nameof(GetAllBookForAuthor))]
        [ProducesResponseType(typeof(IEnumerable<BookDto>), 200)]
        public IActionResult GetAllBookForAuthor(Guid authorId)
        {
            var books = _serviceManager.BookService.GetAllBookForAuthor(authorId, trackChanges: false);
            return Ok(books);
        }

        [HttpGet("{id:guid}", Name = "GetBookForAuthor")]
        [SwaggerOperation(Summary = "Get Book For Author", Description = "Get Book for Author in MySQL database", OperationId = nameof(GetBookForAuthor))]
        [ProducesResponseType(typeof(BookDto), 200)]
        public IActionResult GetBookForAuthor(Guid authorId, Guid id)
        {
            var book = _serviceManager.BookService.GetBookForAuthor(authorId, id, trackChanges: false);
            return Ok(book);
        }


        [HttpPost]
        [SwaggerOperation(Summary = "Create Book For Author", Description = "Create Book for Author in MySQL database", OperationId = nameof(CreatebookForAuhtor))]
        public IActionResult CreatebookForAuhtor(Guid authorId, [FromBody] CreateBookDto book)
        {
            if (book is null)
                return BadRequest("Create Book object is null");
            var response = _serviceManager.BookService.CreateBookForAuthor(authorId, book, trackChanges: false);
            return CreatedAtRoute("GetBookForAuthor", new
            {
                authorId,
                id = response.Id
            }, response);
        }

        [HttpDelete("{bookId:guid}")]
        [SwaggerOperation(Summary = "Delete Book for Author", Description = "Delete book for Author in MySQL Database", OperationId = nameof(DeleteBookForAuthor))]
        public IActionResult DeleteBookForAuthor(Guid authorId, Guid bookId)
        {
            _serviceManager.BookService.DeleteBookForAuthor(authorId, bookId, trackChanges: false);
            return NoContent();
        }
        
    }
}