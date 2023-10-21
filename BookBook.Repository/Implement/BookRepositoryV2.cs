using BookBook.Models.Models;
using Contracts;

namespace BookBook.Repository;

public class BookRepositoryV2 : RepositoryBase<Book>, IBookRepositoryV2
{
    public BookRepositoryV2(RepositoryContext _repositoryContext) : base(_repositoryContext)
    {
    }
}
