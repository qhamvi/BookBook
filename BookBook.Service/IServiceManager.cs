namespace BookBook.Service;

public interface IServiceManager
{
    IAuthorService AuthorService {get;}
    IBookService BookService {get;}
    IUserManagementService UserManagement {get;}

}
