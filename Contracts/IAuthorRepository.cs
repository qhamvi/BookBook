using BookBook.Models.Models;

namespace Contracts
{
    public interface IAuthorRepository : IRepositoryBase<Author>
    {
        IEnumerable<Author> GetAllAuthors(); 
        PagedList<Author> GetAuthorsPaging(AuthorParameters authorParameters);
        Author GetAuthorById(Guid id);
        Author GetAuthorDetailsWithBook(Guid id);
        void CreateAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(Author author);
    }
}