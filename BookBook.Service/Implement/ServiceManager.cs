using AutoMapper;
using BookBook.DTOs.DataTransferObject;
using BookBook.Models;
using BookBook.Repository;
using BookBook.Service.Implement;
using Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Shared;

namespace BookBook.Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAuthorService> _authorService;
    private readonly Lazy<IBookService> _bookService;
    private readonly Lazy<IUserManagementService> _userManagement;
    
    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper, 
        IDataShapper<AuthorDto> dataShapper, UserManager<User> userManager, IConfiguration configuration)
    {
        _authorService = new Lazy<IAuthorService>(() => new AuthorService(repositoryManager, loggerManager, mapper, dataShapper));
        _bookService = new Lazy<IBookService>(() => new BookService(repositoryManager, loggerManager, mapper));
        _userManagement = new Lazy<IUserManagementService>(() => new UserManagementService(loggerManager, mapper, userManager, configuration));

    }

    public IAuthorService AuthorService => _authorService.Value;

    public IBookService BookService => _bookService.Value;
    public IUserManagementService UserManagement => _userManagement.Value;

}
