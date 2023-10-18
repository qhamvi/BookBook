using System.Reflection;
using System.Text;
using BookBook.Models.Models;
using Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;//for OrderBy query

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

            SearchByName(ref authors, authorParameters.Search);
            ApplySort(ref authors, authorParameters.OrderBy);

            return PagedList<Author>.ToPagedList(authors,
                authorParameters.PageNumber,
                authorParameters.PageSize
            );
        }
        private void SearchByName(ref IQueryable<Author> authors, string name)
        {
            if (!authors.Any() || string.IsNullOrEmpty(name))
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
        private void ApplySort(ref IQueryable<Author> authors, string orderByQueryString)
        {
            if (!authors.Any())
                return;

            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                authors = authors.OrderBy(x => x.FirstName + x.LastName);
                return;
            }

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Author).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                authors = authors.OrderBy(x => x.FirstName + x.LastName);
                return;
            }

            authors = authors.OrderBy(orderQuery);
        }
    }
}