using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.API.Entities;
using Library.API.Helpers;
using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Mvc;

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
