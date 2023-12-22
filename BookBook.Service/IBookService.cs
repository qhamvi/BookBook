using BookBook.DTOs;
using BookBook.DTOs.DataTransferObject;
using Microsoft.AspNetCore.JsonPatch;

namespace BookBook.Service;

public interface IBookService
{
    IEnumerable<BookDto> GetAllBookForAuthor(Guid authorId, bool trackChanges);
    BookDto GetBookForAuthor(Guid authorId, Guid id, bool trackChanges);
    /// <summary>
    /// Create a book for Author
    /// </summary>
    /// <param name="authorId"></param>
    /// <param name="bookDto"></param>
    /// <param name="trackChanges"></param>
    /// <returns></returns>
    BookDto CreateBookForAuthor(Guid authorId, CreateBookDto  bookDto, bool trackChanges);
    void DeleteBookForAuthor(Guid authorId, Guid bookId, bool trackChanges);
    IEnumerable<BookDto> GetBooks(bool trackChanges);
    void UpdateBookForAuthor(Guid authorId, Guid bookId, UpdateBookDto bookDto, bool auTrackChanges, bool bookTrackChanges);
    void PartiallyUpdateBookForAuthor(Guid authorId, Guid bookId, JsonPatchDocument<UpdateBookDto> patchBookDto, bool auTrackChanges, bool bookTrackChanges);

}
