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

    public void DeleteBookForAuthor(Guid authorId, Guid bookId, bool trackChanges)
    {
        var author = _repositoryManager.AuthorRepositoryV2.GetAuthor(authorId, trackChanges);
        if(author is null) 
            throw new AuthorNotFoundException(authorId);
        var bookOfAuthor = _repositoryManager.BookRepositoryV2.GetBookForAuthor(authorId, bookId, trackChanges);
        if(bookOfAuthor is null) 
            throw new BookNotFoundException(bookId);
        _repositoryManager.BookRepositoryV2.DeleteBookForAuthor(bookOfAuthor);
        _repositoryManager.Save();
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

    public IEnumerable<BookDto> GetAllBookForAuthor(Guid authorId, bool trackChanges)
    {
        var author = _repositoryManager.AuthorRepositoryV2.GetAuthor(authorId, trackChanges);
        if(author is null)
            throw new AuthorNotFoundException(authorId);
        var books = _repositoryManager.BookRepositoryV2.GetAllBookForAuthor(authorId, trackChanges);
        var response = books.Select(v => _mapper.Map<BookDto>(v));
        return response;
    }

    public IEnumerable<BookDto> GetBooks(bool trackChanges)
    {
        var books = _repositoryManager.BookRepositoryV2.GetBooks(trackChanges);
        var response = books.Select(v => _mapper.Map<BookDto>(v));
        return response;
    }

    public void UpdateBookForAuthor(Guid authorId, Guid bookId, UpdateBookDto bookDto, bool auTrackChanges, bool bookTrackChanges)
    {
        var author = _repositoryManager.AuthorRepositoryV2.GetAuthor(authorId, auTrackChanges);
        if(author is null)
            throw new AuthorNotFoundException(authorId);
        
        var book = _repositoryManager.BookRepositoryV2.GetBookForAuthor(authorId, bookId, bookTrackChanges);
        if(book is null)
            throw new BookNotFoundException(bookId);
        
        _mapper.Map(bookDto, book);
        _repositoryManager.Save();
    }
}
