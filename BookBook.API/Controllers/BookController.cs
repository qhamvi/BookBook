using System.Text.Json;
using AutoMapper;
using BookBook.Models.Models;
using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BookBook.API.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private IRepositoryWrapper _repositoryWrapper;
        private IMapper _mapper;
        public BookController(ILoggerManager logger, IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }


        [HttpGet("authors/{authorId}")]
        public IActionResult GetBooksForAuthor(Guid authorId, [FromQuery] BookParameters parameters)
        {
            var books = _repositoryWrapper.Book.GetBooksByAuthor(authorId, parameters);

            var response = new
            {
                books.TotalCount,
                books.PageSize,
                books.CurrentPage,
                books.TotalPages,
                books.HasNext,
                books.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(response));

            _logger.LogInfor($"Returned {books.TotalCount} authors from database.");

            return Ok(books);
        }
    }
}