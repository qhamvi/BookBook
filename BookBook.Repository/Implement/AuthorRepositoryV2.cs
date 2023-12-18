using BookBook.Models.Models;
using Contracts;

namespace BookBook.Repository;

public class AuthorRepositoryV2 : RepositoryBase<Author>, IAuthorRepositoryV2
{
    public AuthorRepositoryV2(RepositoryContext _repositoryContext) : base(_repositoryContext)
    {
    }

    public void CreateAuthor(Author author) => Create(author);

    public IEnumerable<Author> GetAllAuthors(bool trackChanges) 
            => FindAll(trackChanges).OrderBy(v => v.FirstName + v.LastName).ToList();

    public Author GetAuthor(Guid authorId, bool trackChanges)
            => FindByCondition(v => v.Id.Equals(authorId), trackChanges).FirstOrDefault();

    public IEnumerable<Author> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
    {
        return FindByCondition(v => ids.Contains(v.Id), trackChanges).ToList();
    }
}
