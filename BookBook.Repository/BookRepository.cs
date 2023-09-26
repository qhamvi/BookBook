using BookBook.Models.Models;
using Contracts;

namespace BookBook.Repository
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext _repositoryContext) : base(_repositoryContext)
        {
        }

        public IEnumerable<Book> BooksByAuthor(Guid authorId)
        {
            return FindByCondition(v => v.AuthorId.Equals(authorId)).ToList();
        }
    }
}