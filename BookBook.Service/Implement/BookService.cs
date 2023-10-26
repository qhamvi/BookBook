using AutoMapper;
using Contracts;

namespace BookBook.Service;

public class BookService : IBookService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;
    private readonly IMapper _mapper;
    public BookService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
    {
        _loggerManager = loggerManager;
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }
}
