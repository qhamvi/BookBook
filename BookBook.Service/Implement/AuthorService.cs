﻿using System.Dynamic;
using AutoMapper;
using BookBook.DTOs;
using BookBook.DTOs.DataTransferObject;
using BookBook.Models;
using BookBook.Models.Exceptions;
using BookBook.Models.Models;
using BookBook.Repository;
using Contracts;
using Shared;
using Shared.RequestFeatures;

namespace BookBook.Service;

public class AuthorService : IAuthorService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;
    private readonly IMapper _mapper;
    private readonly IDataShapper<AuthorDto> _dataShapper;
    public AuthorService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper, IDataShapper<AuthorDto> dataShapper)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
        _mapper = mapper;
        _dataShapper = dataShapper;
    }

    public async Task<AuthorDto> CreateAuthorAsync(CreateAuthorDto authorDto)
    {
        var author = _mapper.Map<Author>(authorDto);
        _repositoryManager.AuthorRepositoryV2.CreateAuthor(author);
        await _repositoryManager.SaveAsync();

        var response = _mapper.Map<AuthorDto>(author);
        return response;
    }

    public async Task<(IEnumerable<AuthorDto> authorDtos, string ids)> CreateAuthorCollectionAsync(IEnumerable<CreateAuthorDto> authorCollection)
    {
        if (authorCollection is null)
            throw new AuthorCollectionBadRequest();
        var authorEntities = _mapper.Map<IEnumerable<Author>>(authorCollection);
        foreach (var author in authorEntities)
        {
            _repositoryManager.AuthorRepositoryV2.CreateAuthor(author);
        }
        await _repositoryManager.SaveAsync();

        var authorCollectionToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authorEntities);
        var ids = string.Join(",", authorCollectionToReturn.Select(v => v.Id));

        return (authorDtos: authorCollectionToReturn, ids: ids);
    }

    public async Task DeleteAuthor(Guid authorId, bool trackChanges)
    {
        var author = await GetAuthorIfItExisted(authorId, trackChanges);
        _repositoryManager.AuthorRepositoryV2.DeleteAuthor(author);
        await _repositoryManager.SaveAsync();
    }

    public async Task<AuthorListResponse> GetAllAuthorsAsync(AuthorListRequest param, bool trackChanges)
    {
        if(!param.ValidYearRange)
            throw new MaxAgeRangeBadRequestException();
        var pagedAuthor = await _repositoryManager.AuthorRepositoryV2.GetAllAuthorsAsync(param, trackChanges);
        var result = _mapper.Map<IEnumerable<AuthorDto>>(pagedAuthor);

        return new AuthorListResponse() {
            Result = result,
            MetaData = pagedAuthor.MetaData
        };
    }

    public async Task<AuthorDto> GetAuthorAsync(Guid authorId, bool trackChanges)
    {
        var author = await GetAuthorIfItExisted(authorId, trackChanges);
        var result = _mapper.Map<AuthorDto>(author);
        return result;
    }

    public async Task<(IEnumerable<Entity> authors, MetaData metaData)> GetAuthorListAsync(AuthorListRequest param, bool trackChanges)
    {
        if(!param.ValidYearRange)
            throw new MaxAgeRangeBadRequestException();
        var pagedAuthor = await _repositoryManager.AuthorRepositoryV2.GetAllAuthorsAsync(param, trackChanges);
        var result = _mapper.Map<IEnumerable<AuthorDto>>(pagedAuthor);

        var shapedData = _dataShapper.ShapeData(result, param.Fields);
        return (authors: shapedData, metaData: pagedAuthor.MetaData);
    }

    public async Task<IEnumerable<AuthorDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids is null)
            throw new IdParametersBadRequestException();
        var authors = await _repositoryManager.AuthorRepositoryV2.GetByIdsAsync(ids, trackChanges);
        if (ids.Count() != authors.Count())
            throw new CollectionByIdsBadRequestException();
        var response = _mapper.Map<IEnumerable<AuthorDto>>(authors);
        return response;
    }

    public async Task UpdateAuthorWithBook(Guid authorId, UpdateAuthorWithBooksRequest updateAuthorDto, bool trackChanges)
    {
        var author = await GetAuthorIfItExisted(authorId, trackChanges);

        _mapper.Map(updateAuthorDto, author);
        
        await _repositoryManager.SaveAsync();
    }
        public async Task UpdateAuthor(Guid authorId, UpdateAuthorDto updateAuthorDto, bool trackChanges)
    {
        var author = await GetAuthorIfItExisted(authorId, trackChanges);

        _mapper.Map(updateAuthorDto, author);
        
        await _repositoryManager.SaveAsync();
    }
    private async Task<Author> GetAuthorIfItExisted(Guid authorId, bool trackChanges)
    {
        var author = await _repositoryManager.AuthorRepositoryV2.GetAuthorAsync(authorId, trackChanges);
        if (author is null)
            throw new AuthorNotFoundException(authorId);
        return author;
    }
}
