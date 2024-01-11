using BookBook.DTOs;
using BookBook.DTOs.DataTransferObject;

namespace BookBook.Service;

public interface IAuthorService
{
    Task<AuthorListResponse> GetAllAuthorsAsync(AuthorListRequest param, bool trackChanges);
    Task<AuthorDto> GetAuthorAsync(Guid authorId, bool trackChanges);
    Task<AuthorDto> CreateAuthorAsync(CreateAuthorDto authorDto);
    Task<IEnumerable<AuthorDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    Task<(IEnumerable<AuthorDto> authorDtos, string ids)> CreateAuthorCollectionAsync(IEnumerable<CreateAuthorDto> authorCollection);
    Task DeleteAuthor(Guid authorId, bool trackChanges);
    Task UpdateAuthor(Guid authorId, UpdateAuthorWithBooksRequest updateAuthorDto, bool trackChanges);
}
