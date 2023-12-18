using BookBook.Models.Models;

namespace Contracts;

public interface IBookRepositoryV2
{
    IEnumerable<Book> GetBooks(Guid authorId, bool trackChanges);
    Book GetBookForAuthor(Guid authorId, Guid bookId, bool trackChanges);
    void CreateBookForAuthor(Guid authorId, Book book);

}
