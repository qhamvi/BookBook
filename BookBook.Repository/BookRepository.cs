using BookBook.Models.Models;
using Contracts;

namespace BookBook.Repository
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext _repositoryContext) : base(_repositoryContext)
        {
        }
    }
}