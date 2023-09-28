using BookBook.Models.Models;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace BookBook.Repository
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(RepositoryContext _repositoryContext) : base(_repositoryContext)
        {
        }

        public void CreateAuthor(Author author)
        {
            Create(author);
        }

        public void DeleteAuthor(Author author)
        {
            Delete(author);
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return FindAll()
                    .OrderBy(v => v.FirstName)
                    .ToList();
        }
        public PagedList<Author> GetAuthorsPaging(AuthorParameters authorParameters)
        {
            return PagedList<Author>.ToPagedList(FindAll().OrderBy(v => v.LastName),
                authorParameters.PageNumber,
                authorParameters.PageSize
            );
        }

        public Author GetAuthorById(Guid id)
        {
            return FindByCondition(v => v.Id == id).FirstOrDefault();
        }

        public Author GetAuthorDetailsWithBook(Guid id)
        {
            return FindByCondition(v => v.Id == id)
                    .Include(v => v.Books)
                    .FirstOrDefault();
        }

        public void UpdateAuthor(Author author)
        {
            Update(author);
        }
    }
}