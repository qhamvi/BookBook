
namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IBookRepository Book {get;}
        IAuthorRepository Author {get;}
        void Save();
    }
}