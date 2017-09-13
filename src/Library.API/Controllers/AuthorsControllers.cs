using System;
using System.Collections.Generic;
using AutoMapper;
using Library.API.Entities;
using Library.API.Models;
using Microsoft.AspNetCore.Mvc;
using Library.API.Interfaces;

namespace Library.API.Controllers
{
    [Route("api/authors")]
    public class AuthorsControllers : Controller
    {
        private ILibraryRepository _libraryRepository;

        public AuthorsControllers(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            IEnumerable<Author> authorsFromRepository = _libraryRepository.GetAuthors();

            IEnumerable<AuthorDto> authors = Mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepository);
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthor(Guid id)
        {
            Author authorFromRepository = _libraryRepository.GetAuthor(id);
            if (authorFromRepository == null)
            {
                return NotFound();
            }
            AuthorDto author = Mapper.Map<AuthorDto>(authorFromRepository);
            return new JsonResult(author);
        }

    }
}
