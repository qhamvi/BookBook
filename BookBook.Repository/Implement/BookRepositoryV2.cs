using BookBook.Models.Models;
using Contracts;
using Microsoft.EntityFrameworkCore;

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

    public void DeleteBookForAuthor(Book book)
    {
        Delete(book);
    }

    public async Task<Book> GetBookForAuthorAsync(Guid authorId, Guid bookId, bool trackChanges)
    {
        return await FindByCondition(v => v.AuthorId == authorId && v.Id == bookId, trackChanges).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Book>> GetAllBookForAuthorAsync(Guid authorId, bool trackChanges)
    {
        return await FindByCondition(v => v.AuthorId == authorId, trackChanges).OrderBy(v => v.BookName).ToListAsync(); 
    }

    public async Task<IEnumerable<Book>> GetBooksAsync(bool trackChanges)
    {
        return await FindAll(trackChanges).OrderBy(v => v.BookName).ToListAsync();
    }
}
