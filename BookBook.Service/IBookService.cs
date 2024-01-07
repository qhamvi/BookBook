using BookBook.DTOs;
using BookBook.DTOs.DataTransferObject;
using Microsoft.AspNetCore.JsonPatch;

namespace BookBook.Service;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllBookForAuthorAsync(Guid authorId, bool trackChanges);
    Task<BookDto> GetBookForAuthor(Guid authorId, Guid id, bool trackChanges);
    /// <summary>
    /// Create a book for Author
    /// </summary>
    /// <param name="authorId"></param>
    /// <param name="bookDto"></param>
    /// <param name="trackChanges"></param>
    /// <returns></returns>
    Task<BookDto> CreateBookForAuthorAsync(Guid authorId, CreateBookDto  bookDto, bool trackChanges);
    Task DeleteBookForAuthor(Guid authorId, Guid bookId, bool trackChanges);
    Task<IEnumerable<BookDto>> GetBooksAsync(bool trackChanges);
    Task UpdateBookForAuthor(Guid authorId, Guid bookId, UpdateBookDto bookDto, bool auTrackChanges, bool bookTrackChanges);
    Task PartiallyUpdateBookForAuthor(Guid authorId, Guid bookId, JsonPatchDocument<UpdateBookDto> patchBookDto, bool auTrackChanges, bool bookTrackChanges);

}
