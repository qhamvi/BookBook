using BookBook.Models.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        Task<PaginatedList<Book>> GetBooksByAuthor(Guid authorId, BookParameters bookParameters);
    }
}