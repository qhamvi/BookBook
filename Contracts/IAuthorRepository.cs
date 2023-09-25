using BookBook.Models.Models;

namespace Contracts
{
    public interface IAuthorRepository : IRepositoryBase<Author>
    {
        IEnumerable<Author> GetAllAuthors(); 
        Author GetAuthorById(Guid id);
        Author GetAuthorDetailsWithBook(Guid id);
    }
}