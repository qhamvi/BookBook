using BookBook.Models.Models;

namespace Contracts;

public interface IAuthorRepositoryV2
{
    IEnumerable<Author> GetAllAuthors(bool trackChanges);
    Author GetAuthor(Guid authorId, bool trackChanges);
    void CreateAuthor(Author author);

}
