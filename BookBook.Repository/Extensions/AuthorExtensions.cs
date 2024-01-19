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

}
