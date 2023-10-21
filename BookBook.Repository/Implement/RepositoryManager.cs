using Contracts;

namespace BookBook.Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IAuthorRepositoryV2> _authorRepositoryV2;
    private readonly Lazy<IBookRepositoryV2> _bookRepositoryV2;
    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _authorRepositoryV2 = new Lazy<IAuthorRepositoryV2>(() => new AuthorRepositoryV2(repositoryContext));
        _bookRepositoryV2 = new Lazy<IBookRepositoryV2>(() => new BookRepositoryV2(repositoryContext));
    }

    public IAuthorRepositoryV2 AuthorRepositoryV2 => _authorRepositoryV2.Value;

    public IBookRepositoryV2 BookRepositoryV2 => _bookRepositoryV2.Value;

    public void Save() => _repositoryContext.SaveChanges();
}
