using AutoMapper;
using BookBook.DTOs.DataTransferObject;
using BookBook.Models;
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

    public (IEnumerable<AuthorDto> authorDtos, string ids) CreateAuthorCollection(IEnumerable<CreateAuthorDto> authorCollection)
    {
        if(authorCollection is null)
            throw new AuthorCollectionBadRequest();
        var authorEntities = _mapper.Map<IEnumerable<Author>>(authorCollection);
        foreach(var author in authorEntities)
        {
            _repositoryManager.AuthorRepositoryV2.CreateAuthor(author);
        }
        _repositoryManager.Save();

        var authorCollectionToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authorEntities);
        var ids = string.Join(",", authorCollectionToReturn.Select(v => v.Id));

        return (authorDtos: authorCollectionToReturn, ids: ids);
    }

    public void DeleteAuthor(Guid authorId, bool trackChanges)
    {
        var author = _repositoryManager.AuthorRepositoryV2.GetAuthor(authorId, trackChanges);
        if(author is null) 
            throw new AuthorNotFoundException(authorId);
        
        _repositoryManager.AuthorRepositoryV2.DeleteAuthor(author);
        _repositoryManager.Save();
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

    public IEnumerable<AuthorDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
    {
        if(ids is null)
            throw new IdParametersBadRequestException();
        var authors = _repositoryManager.AuthorRepositoryV2.GetByIds(ids, trackChanges);
        if(ids.Count() != authors.Count())
            throw new CollectionByIdsBadRequestException();
        var response = _mapper.Map<IEnumerable<AuthorDto>>(authors);
        return response;
    }
}
