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
        public PagedList<Author> GetAuthorsPagingFiltering(AuthorParameters authorParameters)
        {
            var authors = FindByCondition(v => v.DayOfBirth.Year >= authorParameters.MinYearOfBirth &&
                                                v.DayOfBirth.Year <= authorParameters.MaxYearOfBirth);

            SearchByName(ref authors, authorParameters.Name);

            return PagedList<Author>.ToPagedList(authors,
                authorParameters.PageNumber,
                authorParameters.PageSize
            );
        }
        private void SearchByName(ref IQueryable<Author> authors, string name)
        {
            if(!authors.Any() || string.IsNullOrEmpty(name))
            return;
            authors = authors.Where(v => v.FirstName.ToLower().Contains(name.Trim().ToLower()) ||
                v.LastName.ToLower().Contains(name.Trim().ToLower()));
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