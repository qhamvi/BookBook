using AutoMapper;
using BookBook.Repository;
using Contracts;

namespace BookBook.Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAuthorService> _authorService;
    private readonly Lazy<IBookService> _bookService;
    
    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
    {
        _authorService = new Lazy<IAuthorService>(() => new AuthorService(repositoryManager, loggerManager, mapper));
        _bookService = new Lazy<IBookService>(() => new BookService(repositoryManager, loggerManager, mapper));
    }

    public IAuthorService AuthorService => _authorService.Value;

    public IBookService BookService => _bookService.Value;
}
