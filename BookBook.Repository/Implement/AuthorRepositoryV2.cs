﻿using BookBook.DTOs;
using BookBook.Models.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

namespace BookBook.Repository;

public class AuthorRepositoryV2 : RepositoryBase<Author>, IAuthorRepositoryV2
{
    public AuthorRepositoryV2(RepositoryContext _repositoryContext) : base(_repositoryContext)
    {
    }

    public void CreateAuthor(Author author) => Create(author);

    public void DeleteAuthor(Author author)
    {
        Delete(author);
    }

    public async Task<PagedList<Author>> GetAllAuthorsAsync(AuthorListRequest param, bool trackChanges) 
    {
        var authors = await FindAll(trackChanges).OrderBy(v => v.FirstName + v.LastName)
                                            .Skip((param.PageNumber - 1) * param.PageSize)
                                            .Take(param.PageSize)
                                            .ToListAsync();
        var count = await FindAll(trackChanges).CountAsync();
        return new PagedList<Author>(authors, count, param.PageNumber, param.PageSize);
        //return PagedList<Author>.ToPagedList(authors, count, param.PageNumber, param.PageSize);
    }

    public async Task<Author> GetAuthorAsync(Guid authorId, bool trackChanges)
            => await FindByCondition(v => v.Id.Equals(authorId), trackChanges).FirstOrDefaultAsync();

    public async Task<IEnumerable<Author>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
    {
        return await FindByCondition(v => ids.Contains(v.Id), trackChanges).ToListAsync();
    }
}
