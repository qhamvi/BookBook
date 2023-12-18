using AutoMapper;
using BookBook.DTOs;
using BookBook.DTOs.DataTransferObject;
using BookBook.Models.Exceptions;
using BookBook.Models.Models;
using Contracts;

namespace BookBook.Service;

public class BookService : IBookService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;
    private readonly IMapper _mapper;
    public BookService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
    {
        _loggerManager = loggerManager;
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public BookDto CreateBookForAuthor(Guid authorId, CreateBookDto bookDto, bool trackChanges)
    {
        var author = _repositoryManager.AuthorRepositoryV2.GetAuthor(authorId, trackChanges);
        if(author is null)
            throw new AuthorNotFoundException(authorId);

        var book = _mapper.Map<Book>(bookDto);
        _repositoryManager.BookRepositoryV2.CreateBookForAuthor(authorId, book);
        _repositoryManager.Save();

        var response = _mapper.Map<BookDto>(book);
        return response;
    }

    public BookDto GetBookForAuthor(Guid authorId, Guid id, bool trackChanges)
    {
        var author = _repositoryManager.AuthorRepositoryV2.GetAuthor(authorId, trackChanges);
        if(author is null)
            throw new AuthorNotFoundException(authorId);
        var book = _repositoryManager.BookRepositoryV2.GetBookForAuthor(authorId, id, trackChanges);
        var response = _mapper.Map<BookDto>(book);
        return response;
    }

    public IEnumerable<BookDto> GetBooks(Guid authorId, bool trackChanges)
    {
        throw new NotImplementedException();
    }
}
