using BookBook.Models.Models;

namespace Contracts
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        IEnumerable<Book> BooksByAuthor(Guid authorId);
    }
}