using AutoMapper;
using BookBook.DTOs;
using BookBook.DTOs.DataTransferObject;
using BookBook.Models.Exceptions;
using BookBook.Models.Models;
using Contracts;
using Microsoft.AspNetCore.JsonPatch;

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

    public async Task<BookDto> CreateBookForAuthorAsync(Guid authorId, CreateBookDto bookDto, bool trackChanges)
    {
        var author = _repositoryManager.AuthorRepositoryV2.GetAuthorAsync(authorId, trackChanges);
        if(author is null)
            throw new AuthorNotFoundException(authorId);

        var book = _mapper.Map<Book>(bookDto);
        _repositoryManager.BookRepositoryV2.CreateBookForAuthor(authorId, book);
        _repositoryManager.SaveAsync();

        var response = _mapper.Map<BookDto>(book);
        return response;
    }

    public async void DeleteBookForAuthor(Guid authorId, Guid bookId, bool trackChanges)
    {
        var author = _repositoryManager.AuthorRepositoryV2.GetAuthorAsync(authorId, trackChanges);
        if(author is null) 
            throw new AuthorNotFoundException(authorId);
        var bookOfAuthor = await _repositoryManager.BookRepositoryV2.GetBookForAuthorAsync(authorId, bookId, trackChanges);
        if(bookOfAuthor is null) 
            throw new BookNotFoundException(bookId);
        _repositoryManager.BookRepositoryV2.DeleteBookForAuthor(bookOfAuthor);
        _repositoryManager.SaveAsync();
    }

    public async Task<BookDto> GetBookForAuthor(Guid authorId, Guid id, bool trackChanges)
    {
        var author = _repositoryManager.AuthorRepositoryV2.GetAuthorAsync(authorId, trackChanges);
        if(author is null)
            throw new AuthorNotFoundException(authorId);
        var book = _repositoryManager.BookRepositoryV2.GetBookForAuthorAsync(authorId, id, trackChanges);
        var response = _mapper.Map<BookDto>(book);
        return response;
    }

    public async Task<IEnumerable<BookDto>> GetAllBookForAuthorAsync(Guid authorId, bool trackChanges)
    {
        var author = _repositoryManager.AuthorRepositoryV2.GetAuthorAsync(authorId, trackChanges);
        if(author is null)
            throw new AuthorNotFoundException(authorId);
        var books = await _repositoryManager.BookRepositoryV2.GetAllBookForAuthorAsync(authorId, trackChanges);
        var response = books.Select(v => _mapper.Map<BookDto>(v));
        return response;
    }

    public async Task<IEnumerable<BookDto>> GetBooksAsync(bool trackChanges)
    {
        var books = await _repositoryManager.BookRepositoryV2.GetBooksAsync(trackChanges);
        var response = books.Select(v => _mapper.Map<BookDto>(v));
        return response;
    }

    public void UpdateBookForAuthor(Guid authorId, Guid bookId, UpdateBookDto bookDto, bool auTrackChanges, bool bookTrackChanges)
    {
        var author = _repositoryManager.AuthorRepositoryV2.GetAuthorAsync(authorId, auTrackChanges);
        if(author is null)
            throw new AuthorNotFoundException(authorId);
        
        var book = _repositoryManager.BookRepositoryV2.GetBookForAuthorAsync(authorId, bookId, bookTrackChanges);
        if(book is null)
            throw new BookNotFoundException(bookId);
        
        _mapper.Map(bookDto, book);
        _repositoryManager.SaveAsync();
    }

    public void PartiallyUpdateBookForAuthor(Guid authorId, Guid bookId, JsonPatchDocument<UpdateBookDto> patchBookDto, bool auTrackChanges, bool bookTrackChanges)
    {
        var author = _repositoryManager.AuthorRepositoryV2.GetAuthorAsync(authorId, auTrackChanges);
        if(author is null)
            throw new AuthorNotFoundException(authorId);
        
        var book = _repositoryManager.BookRepositoryV2.GetBookForAuthorAsync(authorId, bookId, bookTrackChanges);
        if(book is null)
            throw new BookNotFoundException(bookId);

        var bookToPatch = _mapper.Map<UpdateBookDto>(book);
        patchBookDto.ApplyTo(bookToPatch);

        _mapper.Map(bookToPatch, book);
        _repositoryManager.SaveAsync();

    }
}
