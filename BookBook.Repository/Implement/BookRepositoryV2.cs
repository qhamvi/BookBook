using BookBook.Models.Models;
using Contracts;

namespace BookBook.Repository;

public class BookRepositoryV2 : RepositoryBase<Book>, IBookRepositoryV2
{
    public BookRepositoryV2(RepositoryContext _repositoryContext) : base(_repositoryContext)
    {
    }

    public void CreateBookForAuthor(Guid authorId, Book book)
    {
        book.AuthorId = authorId;
        Create(book);
    }

    public Book GetBookForAuthor(Guid authorId, Guid bookId, bool trackChanges)
    {
        return FindByCondition(v => v.AuthorId == authorId && v.Id == bookId, trackChanges).FirstOrDefault();
    }

    public IEnumerable<Book> GetBooks(Guid authorId, bool trackChanges)
    {
        return FindByCondition(v => v.AuthorId == authorId, trackChanges).OrderBy(v => v.BookName).ToList(); 
    }
}
