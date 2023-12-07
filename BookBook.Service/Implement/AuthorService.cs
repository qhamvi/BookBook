using AutoMapper;
using BookBook.DTOs.DataTransferObject;
using BookBook.Models.Exceptions;
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

    public AuthorDto CreateAuthor(CreateAuthorDto authorDto)
    {
        var author = _mapper.Map<Author>(authorDto);
        _repositoryManager.AuthorRepositoryV2.CreateAuthor(author);
        _repositoryManager.Save();

        var response = _mapper.Map<AuthorDto>(author);
        return response;
    }

    public IEnumerable<AuthorDto> GetAllAuthors(bool trackChanges)
    {
            var authors = _repositoryManager.AuthorRepositoryV2.GetAllAuthors(trackChanges);
            var result = _mapper.Map<IEnumerable<AuthorDto>>(authors);
            return result;
    }

    public AuthorDto GetAuthor(Guid authorId, bool trackChanges)
    {
        var author = _repositoryManager.AuthorRepositoryV2.GetAuthor(authorId, trackChanges);
        //Check if author is null
        if (author is null)
            throw new AuthorNotFoundException(authorId);

        var result = _mapper.Map<AuthorDto>(author);
        return result;
    }
}
