using BookBook.Models.Models;

namespace Contracts;

public interface IBookRepositoryV2
{
    IEnumerable<Book> GetAllBookForAuthor(Guid authorId, bool trackChanges);
    Book GetBookForAuthor(Guid authorId, Guid bookId, bool trackChanges);
    void CreateBookForAuthor(Guid authorId, Book book);
    void DeleteBookForAuthor(Book book);
    IEnumerable<Book> GetBooks(bool trackChanges);

}
