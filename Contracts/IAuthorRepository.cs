using BookBook.Models.Models;

namespace Contracts
{
    public interface IAuthorRepository : IRepositoryBase<Author>
    {
        IEnumerable<Author> GetAllAuthors(); 
    }
}