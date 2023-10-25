using BookBook.Models.Models;
using Contracts;

namespace BookBook.Service;

public class AuthorService : IAuthorService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;
    public AuthorService(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
    }

    public IEnumerable<Author> GetAllAuthors(bool trackChanges)
    {
        try
        {
            var authors = _repositoryManager.AuthorRepositoryV2.GetAllAuthors(trackChanges);
            return authors;
        }
        catch(Exception ex)
        {
            _loggerManager.LogError($"Something went wrong in the {nameof(GetAllAuthors)} service method {ex}");
            throw;
        }
    }
}
