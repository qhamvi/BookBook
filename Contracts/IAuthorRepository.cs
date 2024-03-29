using BookBook.Models.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface IAuthorRepository : IRepositoryBase<Author>
    {
        IEnumerable<Author> GetAllAuthors(); 
        Task<PaginatedList<Author>> GetAuthorsPagingFiltering(AuthorParameters authorParameters);
        Author GetAuthorById(Guid id);
        Author GetAuthorDetailsWithBook(Guid id);
        void CreateAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(Author author);
    }
}