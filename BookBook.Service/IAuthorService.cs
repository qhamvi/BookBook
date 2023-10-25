using BookBook.Models.Models;

namespace BookBook.Service;

public interface IAuthorService
{
    IEnumerable<Author> GetAllAuthors(bool trackChanges);

}
