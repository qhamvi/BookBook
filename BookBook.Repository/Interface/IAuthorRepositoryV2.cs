using BookBook.DTOs;
using BookBook.Models.Models;
using Shared.RequestFeatures;

namespace BookBook.Repository;

public interface IAuthorRepositoryV2
{
    Task<PagedList<Author>> GetAllAuthorsAsync(AuthorListRequest param, bool trackChanges);
    Task<Author> GetAuthorAsync(Guid authorId, bool trackChanges);
    void CreateAuthor(Author author);
    Task<IEnumerable<Author>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    void DeleteAuthor(Author author);

}
