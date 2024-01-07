namespace BookBook.Repository;

public interface IRepositoryManager
{
    IAuthorRepositoryV2 AuthorRepositoryV2 {get;}
    IBookRepositoryV2 BookRepositoryV2 {get;}
    Task SaveAsync();
}
