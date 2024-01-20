using System.Linq.Expressions;
using BookBook.Models.Models;

namespace BookBook.Repository;

public static class AuthorExtensions
{
    public static IQueryable<Author> FilterAuthors(this IQueryable<Author> authors, uint minYear, uint maxYear)
    {
        return authors.Where(v => (v.DayOfBirth.Year >= minYear && v.DayOfBirth.Year <= maxYear));
    }
    public static IQueryable<Author> Search(this IQueryable<Author> authors, string? search)
    {
        if(string.IsNullOrEmpty(search))
            return authors;
        
        return authors.Where(v => (v.FirstName + ' ' + v.LastName).ToLower()
                                  .Contains(search.Trim().ToLower()));
    }
    public static IQueryable<Author> OrderCustomBy(this IQueryable<Author> authors, string orderBy, bool isDesc)
    {
        orderBy = string.IsNullOrEmpty(orderBy) ? string.Empty : orderBy;
        Dictionary<string, Expression<Func<Author, object>>> keyValuePairs = new()
        {
            {"", v => (v.FirstName + ' ' + v.LastName)},
            {nameof(Author.FirstName), v => v.FirstName},
            {nameof(Author.LastName), v => v.LastName},
            {nameof(Author.Country), v => v.Country},
            {nameof(Author.DayOfBirth), v => v.DayOfBirth},
            {nameof(Author.PhoneNumber), v => v.PhoneNumber}
        };
        if(isDesc)
            return authors.OrderByDescending(keyValuePairs[orderBy]);
            
        return authors.OrderBy(keyValuePairs[orderBy]);
    }

}
