using BookBook.Models.Models;

namespace Contracts;

public interface IAuthorRepositoryV2
{
    Task<IEnumerable<Author>> GetAllAuthorsAsync(bool trackChanges);
    Task<Author> GetAuthorAsync(Guid authorId, bool trackChanges);
    void CreateAuthor(Author author);
    Task<IEnumerable<Author>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    void DeleteAuthor(Author author);

}
