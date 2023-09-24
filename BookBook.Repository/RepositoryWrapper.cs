using Contracts;

namespace BookBook.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repositoryContext;
        private IBookRepository _bookRepository;
        private IAuthorRepository _authorRepository;

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public void Save()
        {
            _repositoryContext.SaveChanges();
        }

        public IBookRepository Book
        {
            get {
                if(_bookRepository == null)
                {
                    _bookRepository = new BookRepository(_repositoryContext);
                }
                return _bookRepository;
            }
        }

        public IAuthorRepository Author 
        {
            get {
                if(_authorRepository == null)
                {
                    _authorRepository = new AuthorRepository(_repositoryContext);
                }
                return _authorRepository;
            }
        }
        
    }
}