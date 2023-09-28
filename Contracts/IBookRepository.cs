using BookBook.Models.Models;

namespace Contracts
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        PagedList<Book> GetBooksByAuthor(Guid authorId, BookParameters bookParameters);
    }
}