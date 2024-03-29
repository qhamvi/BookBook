using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using BookBook.DTOs;
using BookBook.DTOs.DataTransferObject;
using BookBook.Service;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookBook.Presentation.Controllers
{

    [Route("authors")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    //[ResponseCache(CacheProfileName = "120SecondsDuration")]
    public class AuthorsController : ControllerBase
    {
        private IServiceManager _serviceManager;
        public AuthorsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        /// <summary>
        /// Get author list
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(List<AuthorDto>), 200)]
        public async Task<IActionResult> GetAuthorList([FromQuery] AuthorListRequest param)
        {
            var pagedResult = await _serviceManager.AuthorService.GetAuthorListAsync(param, trackChanges: false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.authors);
        }

        /// <summary>
        /// Get all author
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet("all")]
        [Authorize(Roles = "Adminstrator")]
        [ProducesResponseType(typeof(List<AuthorDto>), 200)]
        public async Task<IActionResult> GetAllAuthors([FromQuery] AuthorListRequest param)
        {
            var pagedResult = await _serviceManager.AuthorService.GetAllAuthorsAsync(param, trackChanges: false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.MetaData));
            return Ok(pagedResult.Result);
        }

        /// <summary>
        /// Get a author by authorId
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns></returns>
        [HttpGet("{authorId:Guid}")]
        [ProducesResponseType(typeof(AuthorDto), 200)]
        //[ResponseCache(Duration = 60)]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> GetAuthor(Guid authorId)
        {
            var author = await _serviceManager.AuthorService.GetAuthorAsync(authorId, trackChanges: false);
            return Ok(author);
        }

        /// <summary>
        /// Create a author
        /// </summary>
        /// <param name="authorDto"></param>
        /// <returns></returns>
        /// <returns>A newly created company</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        /// <response code="422">If the model is invalid</response>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(typeof(AuthorDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDto authorDto)
        {
            var author = await _serviceManager.AuthorService.CreateAuthorAsync(authorDto);
            return CreatedAtAction("GetAuthorAsync", new { authorId = author.Id }, author);
        }
        /// <summary>
        /// Get Author Collection by Id List
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet("collection/({ids})", Name = "GetAuthorCollection")]
        public async Task<IActionResult> GetAuthorCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            var authors = await _serviceManager.AuthorService.GetByIdsAsync(ids, trackChanges: false);
            return Ok(authors);
        }

        ///<summary>
        ///Create author collection
        ///</summary>
        ///<param name="authorDtos"></param>
        ///<returns></returns>
        [HttpPost("collection")]
        public async Task<IActionResult> CreateAuthorCollection([FromBody] IEnumerable<CreateAuthorDto> authorDtos)
        {
            var result = await _serviceManager.AuthorService.CreateAuthorCollectionAsync(authorDtos);
            return CreatedAtAction("GetAuthorCollection", new { result.ids }, result.authorDtos);
        }

        ///<summary>
        ///Delete Author
        ///</summary>
        ///<param name="authorId"></param>
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
        /// <param name="authorId"></param>
        /// <param name="authorDto"></param>
        ///<returns></returns>
        [HttpPut("book/{authorId:Guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateAuthorWithBookRequest(Guid authorId, [FromBody] UpdateAuthorWithBooksRequest authorDto)
        {
            await _serviceManager.AuthorService.UpdateAuthorWithBook(authorId, authorDto, trackChanges: true);
            return NoContent();
        }

        ///<summary>
        ///Update author with book collection
        ///</summary>
        /// <param name="authorId"></param>
        /// <param name="authorDto"></param>
        ///<returns></returns>
        [HttpPut("{authorId:Guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateAuthor(Guid authorId, [FromBody] UpdateAuthorDto authorDto)
        {
            await _serviceManager.AuthorService.UpdateAuthor(authorId, authorDto, trackChanges: true);
            return NoContent();
        }

    }
}