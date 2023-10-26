using AutoMapper;
using BookBook.DTOs.DataTransferObject;
using BookBook.Models.Models;
using Contracts;

namespace BookBook.Service;

public class AuthorService : IAuthorService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;
    private readonly IMapper _mapper;
    public AuthorService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
        _mapper = mapper;
    }

    public IEnumerable<AuthorDto> GetAllAuthors(bool trackChanges)
    {
            var authors = _repositoryManager.AuthorRepositoryV2.GetAllAuthors(trackChanges);
            var result = _mapper.Map<IEnumerable<AuthorDto>>(authors);
            return result;
    }
}
