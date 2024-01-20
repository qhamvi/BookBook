using BookBook.Models.Models;
using Contracts;
using Shared.RequestFeatures;

namespace BookBook.Repository
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext _repositoryContext) : base(_repositoryContext)
        {
        }

        public async Task<PaginatedList<Book>> GetBooksByAuthor(Guid authorId, BookParameters bookParameters)
        {
            // return FindByCondition(v => v.AuthorId.Equals(authorId)).ToList();
            return await PaginatedList<Book>.ToPagedList(FindByCondition(v => v.AuthorId.Equals(authorId), false),
                bookParameters.PageNumber,
                bookParameters.PageSize
            );

        }
    }
}