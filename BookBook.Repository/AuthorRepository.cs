using BookBook.Models.Models;
using Contracts;

namespace BookBook.Repository
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(RepositoryContext _repositoryContext) : base(_repositoryContext)
        {
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return FindAll()
                    .OrderBy(v => v.FirstName)
                    .ToList();
        }
    }
}