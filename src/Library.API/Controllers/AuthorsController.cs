﻿using AutoMapper;
using Library.API.Entities;
using Library.API.Helpers;
using Library.API.Interfaces;
using Library.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.API.Controllers
{
    [Route("api/authors")]
    public class AuthorsController : Controller
    {
        private ILibraryRepository _libraryRepository;
        private IUrlHelper _urlHelper;
        private IPropertyMappingService _propertyMappingService;
        private ITypeHelperService _typeHelperService;

        public AuthorsController(ILibraryRepository libraryRepository,
                                 IUrlHelper urlHelper,
                                 IPropertyMappingService propertyMappingService,
                                 ITypeHelperService typeHelperService)
        {
            _urlHelper = urlHelper;
            _libraryRepository = libraryRepository;
            _propertyMappingService = propertyMappingService;
            _typeHelperService = typeHelperService;
        }

        [HttpGet(Name = "GetAuthors")]
        public IActionResult GetAuthors(AuthorsResourceParameters authorsResourceParameters)
        {
            if (!_propertyMappingService.ValidMappingExistsFor<AuthorDto, Author>(authorsResourceParameters.OrderBy))
            {
                return BadRequest();
            }

            if (!_typeHelperService.TypeHasProperties<AuthorDto>(authorsResourceParameters.Fields))
            {
                return BadRequest();
            }

            PageList<Author> authorsFromRepo = _libraryRepository.GetAuthors(authorsResourceParameters);
            if (authorsFromRepo == null || !authorsFromRepo.Any())
            {
                return NotFound();
            }

            var paginationMetadata = new
            {
                totalCount = authorsFromRepo.TotalCount,
                pageSize = authorsFromRepo.PageSize,
                currentPage = authorsFromRepo.CurrentPage,
                totalPages = authorsFromRepo.TotalPages
            };

            Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            var authors = Mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo);

            var links = CreateLinksForAuthors(authorsResourceParameters,
                 authorsFromRepo.HasNext, authorsFromRepo.HasPrevious);

            var shapedAuthors = authors.ShapeData(authorsResourceParameters.Fields);

            var shapedAuthorsWithLinks = shapedAuthors.Select(author =>
            {
                var authorAsDictionary = author as IDictionary<string, object>;
                var authorLinks = CreateLinksForAuthor(
                    (Guid)authorAsDictionary["Id"], authorsResourceParameters.Fields);

                authorAsDictionary.Add("links", authorLinks);

                return authorAsDictionary;
            });

            var linkedCollectionResource = new
            {
                value = shapedAuthorsWithLinks,
                links = links
            };

            return Ok(linkedCollectionResource);
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public IActionResult GetAuthor(Guid id, [FromQuery] string fields)
        {
            if (!_typeHelperService.TypeHasProperties<AuthorDto>(fields))
            {
                return BadRequest();
            }

            var authorFromRepo = _libraryRepository.GetAuthor(id);

            if (authorFromRepo == null)
            {
                return NotFound();
            }

            var author = Mapper.Map<AuthorDto>(authorFromRepo);

            var links = CreateLinksForAuthor(id, fields);
            var linkedResourceToReturn = author.ShapeData(fields) as IDictionary<string, object>;
            linkedResourceToReturn.Add("links", links);

            return Ok(linkedResourceToReturn);
        }

        [HttpPost]
        public IActionResult CreateAuthor([FromBody] AuthorForCreationDto author)
        {
            if (author == null)
            {
                return BadRequest();
            }
            Author authorEntity = Mapper.Map<Author>(author);
            _libraryRepository.AddAuthor(authorEntity);
            if (!_libraryRepository.Save())
            {
                throw new Exception("Creating an author failed on save.");
            }
            var authorToReturn = Mapper.Map<AuthorDto>(authorEntity);

            var links = CreateLinksForAuthor(authorToReturn.Id, null);
            var linkedResourceToReturn = author.ShapeData(null) as IDictionary<string, object>;
            linkedResourceToReturn.Add("links", links);

            return CreatedAtRoute("GetAuthor", new { id = linkedResourceToReturn["Id"] }, linkedResourceToReturn);  
        }

        [HttpPost("{id}")]
        public IActionResult BlockAuthorCreation(Guid id)
        {
            if (_libraryRepository.AuthorExists(id))
            {

                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            return NotFound();
        }

        [HttpDelete("{id}", Name = "DeleteAuthor")]
        public IActionResult DeleteAuthor(Guid id)
        {
            var authorFromRepository = _libraryRepository.GetAuthor(id);
            if (authorFromRepository == null)
            {
                return NotFound();
            }

            _libraryRepository.DeleteAuthor(authorFromRepository);
            if (!_libraryRepository.Save())
            {
                throw new Exception($"Deleting author {id} failed on save.");
            }

            return NoContent();
        }

        private string CreateAuthorsResourceUri(AuthorsResourceParameters authorsResourceParameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return _urlHelper.Link("GetAuthors", new
                    {
                        fields = authorsResourceParameters.Fields,
                        searchQuery = authorsResourceParameters.SearchQuery,
                        genre = authorsResourceParameters.Genre,
                        pageNumber = authorsResourceParameters.PageNumber - 1,
                        pageSize = authorsResourceParameters.PageSize
                    });
                case ResourceUriType.NextPage:
                    return _urlHelper.Link("GetAuthors", new
                    {
                        fields = authorsResourceParameters.Fields,
                        searchQuery = authorsResourceParameters.SearchQuery,
                        genre = authorsResourceParameters.Genre,
                        pageNumber = authorsResourceParameters.PageNumber + 1,
                        pageSize = authorsResourceParameters.PageSize
                    });
                case ResourceUriType.Current:
                default:
                    return _urlHelper.Link("GetAuthors", new
                    {
                        fields = authorsResourceParameters.Fields,
                        searchQuery = authorsResourceParameters.SearchQuery,
                        genre = authorsResourceParameters.Genre,
                        pageNumber = authorsResourceParameters.PageNumber,
                        pageSize = authorsResourceParameters.PageSize
                    });
            }
        }

        private IEnumerable<LinkDto> CreateLinksForAuthor(Guid id, string fields)
        {
            List<LinkDto> links = new List<LinkDto>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add(new LinkDto(_urlHelper.Link("GetAuthors", new { id = id }), "self", "GET"));
            }
            else
            {
                links.Add(new LinkDto(_urlHelper.Link("GetAuthors", new { id = id, fields = fields }), "self", "GET"));
            }

            links.Add(new LinkDto(_urlHelper.Link("DeleteAuthor", new { id = id }), "delete_author", "DELETE"));
            links.Add(new LinkDto(_urlHelper.Link("CreateBookForAuthor", new { authorId = id }), "create_book_for_author", "POST"));
            links.Add(new LinkDto(_urlHelper.Link("GetBooksForAuthor", new { authorId = id }), "books", "GET")); 
            return links;
        }

        private IEnumerable<LinkDto> CreateLinksForAuthors(
             AuthorsResourceParameters authorsResourceParameters,
             bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>();

            // self 
            links.Add(
               new LinkDto(CreateAuthorsResourceUri(authorsResourceParameters,
               ResourceUriType.Current)
               , "self", "GET"));

            if (hasNext)
            {
                links.Add(
                  new LinkDto(CreateAuthorsResourceUri(authorsResourceParameters,
                  ResourceUriType.NextPage),
                  "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(
                    new LinkDto(CreateAuthorsResourceUri(authorsResourceParameters,
                    ResourceUriType.PreviousPage),
                    "previousPage", "GET"));
            }

            return links;
        }
    }
}