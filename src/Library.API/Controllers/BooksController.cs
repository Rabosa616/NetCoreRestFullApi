using System;
using System.Collections.Generic;
using AutoMapper;
using Library.API.Entities;
using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/authors/{authorId}/books")]
    public class BooksController:Controller
    {
        private ILibraryRepository _libraryRepository;
        
        public BooksController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpGet]
        public IActionResult GetBooksForAuthor(Guid authorId)
        {
            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            IEnumerable<Book> booksForAuthorFromRepository = _libraryRepository.GetBooksForAuthor(authorId);
            IEnumerable<BookDto> booksForAuthor = Mapper.Map<IEnumerable<BookDto>>(booksForAuthorFromRepository);

            return Ok(booksForAuthor);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookForAuthor(Guid authorId, Guid id)
        {
            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            Book bookForAuthorFromRepository = _libraryRepository.GetBookForAuthor(authorId, id);
            if (bookForAuthorFromRepository == null)
            {
                return NotFound();
            }
            BookDto bookForAuthor = Mapper.Map<BookDto>(bookForAuthorFromRepository);
            return Ok(bookForAuthor);
        }
    }
}
