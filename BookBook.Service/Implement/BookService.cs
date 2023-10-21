using Contracts;

namespace BookBook.Service;

public class BookService : IBookService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;
    public BookService(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
    {
        _loggerManager = loggerManager;
        _repositoryManager = repositoryManager;
    }
}
