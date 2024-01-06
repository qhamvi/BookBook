using BookBook.Models.Models;

namespace Contracts;

public interface IBookRepositoryV2
{
    Task<IEnumerable<Book>> GetAllBookForAuthorAsync(Guid authorId, bool trackChanges);
    Task<Book> GetBookForAuthorAsync(Guid authorId, Guid bookId, bool trackChanges);
    void CreateBookForAuthor(Guid authorId, Book book);
    void DeleteBookForAuthor(Book book);
    Task<IEnumerable<Book>> GetBooksAsync(bool trackChanges);

}
