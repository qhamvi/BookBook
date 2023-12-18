using BookBook.DTOs.DataTransferObject;
using BookBook.Models.Models;

namespace BookBook.Service;

public interface IAuthorService
{
    IEnumerable<AuthorDto> GetAllAuthors(bool trackChanges);
    AuthorDto GetAuthor(Guid authorId, bool trackChanges);
    AuthorDto CreateAuthor(CreateAuthorDto authorDto);
    IEnumerable<AuthorDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    (IEnumerable<AuthorDto> authorDtos, string ids) CreateAuthorCollection(IEnumerable<CreateAuthorDto> authorCollection);
    void DeleteAuthor(Guid authorId, bool trackChanges);
}
