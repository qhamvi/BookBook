using AutoMapper;
using BookBook.DTOs;
using BookBook.DTOs.DataTransferObject;
using BookBook.Models.Exceptions;
using BookBook.Models.Models;
using BookBook.Repository;
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
        await CheckAuthorIfItExisted(authorId, trackChanges);

        var book = _mapper.Map<Book>(bookDto);
        _repositoryManager.BookRepositoryV2.CreateBookForAuthor(authorId, book);
        await _repositoryManager.SaveAsync();

        var response = _mapper.Map<BookDto>(book);
        return response;
    }

    public async Task DeleteBookForAuthor(Guid authorId, Guid bookId, bool trackChanges)
    {
        await CheckAuthorIfItExisted(authorId, trackChanges);

        var book = await GetBookIfItExisted(authorId, bookId, trackChanges);

        _repositoryManager.BookRepositoryV2.DeleteBookForAuthor(book);
        await _repositoryManager.SaveAsync();
    }

    public async Task<BookDto> GetBookForAuthor(Guid authorId, Guid id, bool trackChanges)
    {
        await CheckAuthorIfItExisted(authorId, trackChanges);
        var book = await GetBookIfItExisted(authorId, id, trackChanges);
        var response = _mapper.Map<BookDto>(book);
        return response;
    }

    public async Task<IEnumerable<BookDto>> GetAllBookForAuthorAsync(Guid authorId, bool trackChanges)
    {
        await CheckAuthorIfItExisted(authorId, trackChanges);
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

    public async Task UpdateBookForAuthor(Guid authorId, Guid bookId, UpdateBookDto bookDto, bool auTrackChanges, bool bookTrackChanges)
    {
        await CheckAuthorIfItExisted(authorId, auTrackChanges);
        var book = await GetBookIfItExisted(authorId, bookId, bookTrackChanges);


        _mapper.Map(bookDto, book);
        await _repositoryManager.SaveAsync();
    }

    public async Task PartiallyUpdateBookForAuthor(Guid authorId, Guid bookId, JsonPatchDocument<UpdateBookDto> patchBookDto, bool auTrackChanges, bool bookTrackChanges)
    {
        await CheckAuthorIfItExisted(authorId, auTrackChanges);

        var book = await GetBookIfItExisted(authorId, bookId, bookTrackChanges);

        var bookToPatch = _mapper.Map<UpdateBookDto>(book);
        patchBookDto.ApplyTo(bookToPatch);

        _mapper.Map(bookToPatch, book);
        await _repositoryManager.SaveAsync();

    }
    private async Task CheckAuthorIfItExisted(Guid authorId, bool trackChanges)
    {
        var author = await _repositoryManager.AuthorRepositoryV2.GetAuthorAsync(authorId, trackChanges);
        if (author is null)
            throw new AuthorNotFoundException(authorId);
    }
    private async Task<Book> GetBookIfItExisted(Guid authorId, Guid bookId, bool trackChanges)
    {
        var book = await _repositoryManager.BookRepositoryV2.GetBookForAuthorAsync(authorId, bookId, trackChanges);
        if (book is null)
            throw new BookNotFoundException(bookId);
        return book;
    }
}
