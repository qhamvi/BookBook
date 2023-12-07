using BookBook.DTOs.DataTransferObject;
using BookBook.Models.Models;

namespace BookBook.Service;

public interface IAuthorService
{
    IEnumerable<AuthorDto> GetAllAuthors(bool trackChanges);
    AuthorDto GetAuthor(Guid authorId, bool trackChanges);
    AuthorDto CreateAuthor(CreateAuthorDto authorDto);

}
