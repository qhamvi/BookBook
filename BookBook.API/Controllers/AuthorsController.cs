using AutoMapper;
using BookBook.DTOs;
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
        private IMapper _mapper;
        public AuthorsController(ILoggerManager logger, IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            try
            {
                var authors = _repositoryWrapper.Author.GetAllAuthors();
                _logger.LogInfor($"Returned all authors from database.");
                var authorsResult = _mapper.Map<List<AuthorDto>>(authors);
                var author = _mapper.Map<AuthorDto>(authors.First());
                return Ok(authorsResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GET ALL AUTHOR");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetAuthorById(Guid id)
        {
            try
            {
                var author = _repositoryWrapper.Author.GetAuthorById(id);

                if (author is null)
                {
                    _logger.LogError($"author with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfor($"Returned author with id: {id}");

                    var authorResult = _mapper.Map<AuthorDto>(author);
                    return Ok(authorResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetauthorById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{id}/details")]
        public IActionResult GetAuthorWithDetails(Guid id)
        {
            try
            {
                var Author = _repositoryWrapper.Author.GetAuthorDetailsWithBook(id);

                if (Author == null)
                {
                    _logger.LogError($"Author with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfor($"Returned Author with details for id: {id}");

                    var AuthorResult = _mapper.Map<AuthorDto>(Author);
                    return Ok(AuthorResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAuthorWithDetails action: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
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

    }
}