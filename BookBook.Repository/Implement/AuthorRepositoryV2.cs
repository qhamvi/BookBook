using BookBook.DTOs;
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

    public async Task<PaginatedList<Author>> GetAllAuthorsAsync(AuthorListRequest param, bool trackChanges) 
    {
        var authors = FindAll(trackChanges).FilterAuthors(param.MinYearOfBirth, param.MaxYearOfBirth)
                                            .Search(param.Search)
                                            .OrderCustomBy(param.OrderBy, param.IsDesc);
                                            //.Skip((param.PageNumber - 1) * param.PageSize)
                                            //.Take(param.PageSize)
                                            //.ToListAsync();
        // var authors = await FindAll(trackChanges).OrderBy(v => v.FirstName + v.LastName)
        //                                     .Skip((param.PageNumber - 1) * param.PageSize)
        //                                     .Take(param.PageSize)
        //                                     .ToListAsync();
        //var count = await FindAll(trackChanges).CountAsync();
        //return new PaginatedList<Author>(authors, count, param.PageNumber, param.PageSize);
        var y = await PaginatedList<Author>.ToPagedList(authors, param.PageNumber, param.PageSize);
        return y;
    }

    public async Task<Author> GetAuthorAsync(Guid authorId, bool trackChanges)
            => await FindByCondition(v => v.Id.Equals(authorId), trackChanges).FirstOrDefaultAsync();

    public async Task<IEnumerable<Author>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
    {
        return await FindByCondition(v => ids.Contains(v.Id), trackChanges).ToListAsync();
    }
}
