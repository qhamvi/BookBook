using BookBook.Models.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        PagedList<Book> GetBooksByAuthor(Guid authorId, BookParameters bookParameters);
    }
}