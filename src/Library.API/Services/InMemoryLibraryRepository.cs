using Library.API.Entities;
using Library.API.Helpers;
using Library.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.API.Services
{
    public class InMemoryLibraryRepository : ILibraryRepository
    {
        private static List<Author> _authors;
        private static List<Book> _books;

        public InMemoryLibraryRepository()
        {
            GenerateNewAuthors();
            GenerateNewBooks();
        }

        private static void GenerateNewAuthors()
        {
            _authors = new List<Author>
            {
                new Author()
                {
                     Id = new Guid("25320c5e-f58a-4b1f-b63a-8ee07a840bdf"),
                     FirstName = "Stephen",
                     LastName = "King",
                     Genre = "Horror",
                     DateOfBirth = new DateTimeOffset(new DateTime(1947, 9, 21)),
                     Books = new List<Book>()
                     {
                         new Book()
                         {
                             Id = new Guid("c7ba6add-09c4-45f8-8dd0-eaca221e5d93"),
                             Title = "The Shining",
                             Description = "The Shining is a horror novel by American author Stephen King. Published in 1977, it is King's third published novel and first hardback bestseller: the success of the book firmly established King as a preeminent author in the horror genre. "
                         },
                         new Book()
                         {
                             Id = new Guid("a3749477-f823-4124-aa4a-fc9ad5e79cd6"),
                             Title = "Misery",
                             Description = "Misery is a 1987 psychological horror novel by Stephen King. This novel was nominated for the World Fantasy Award for Best Novel in 1988, and was later made into a Hollywood film and an off-Broadway play of the same name."
                         },
                         new Book()
                         {
                             Id = new Guid("70a1f9b9-0a37-4c1a-99b1-c7709fc64167"),
                             Title = "It",
                             Description = "It is a 1986 horror novel by American author Stephen King. The story follows the exploits of seven children as they are terrorized by the eponymous being, which exploits the fears and phobias of its victims in order to disguise itself while hunting its prey. 'It' primarily appears in the form of a clown in order to attract its preferred prey of young children."
                         },
                         new Book()
                         {
                             Id = new Guid("60188a2b-2784-4fc4-8df8-8919ff838b0b"),
                             Title = "The Stand",
                             Description = "The Stand is a post-apocalyptic horror/fantasy novel by American author Stephen King. It expands upon the scenario of his earlier short story 'Night Surf' and outlines the total breakdown of society after the accidental release of a strain of influenza that had been modified for biological warfare causes an apocalyptic pandemic which kills off the majority of the world's human population."
                         }
                     }
                },
                new Author()
                {
                     Id = new Guid("76053df4-6687-4353-8937-b45556748abe"),
                     FirstName = "George",
                     LastName = "RR Martin",
                     Genre = "Fantasy",
                     DateOfBirth = new DateTimeOffset(new DateTime(1948, 9, 20)),
                     Books = new List<Book>()
                     {
                         new Book()
                         {
                             Id = new Guid("447eb762-95e9-4c31-95e1-b20053fbe215"),
                             Title = "A Game of Thrones",
                             Description = "A Game of Thrones is the first novel in A Song of Ice and Fire, a series of fantasy novels by American author George R. R. Martin. It was first published on August 1, 1996."
                         },
                         new Book()
                         {
                             Id = new Guid("bc4c35c3-3857-4250-9449-155fcf5109ec"),
                             Title = "The Winds of Winter",
                             Description = "Forthcoming 6th novel in A Song of Ice and Fire."
                         },
                         new Book()
                         {
                             Id = new Guid("09af5a52-9421-44e8-a2bb-a6b9ccbc8239"),
                             Title = "A Dance with Dragons",
                             Description = "A Dance with Dragons is the fifth of seven planned novels in the epic fantasy series A Song of Ice and Fire by American author George R. R. Martin."
                         }
                     }
                },
                new Author()
                {
                     Id = new Guid("412c3012-d891-4f5e-9613-ff7aa63e6bb3"),
                     FirstName = "Neil",
                     LastName = "Gaiman",
                     Genre = "Fantasy",
                     DateOfBirth = new DateTimeOffset(new DateTime(1960, 11, 10)),
                     Books = new List<Book>()
                     {
                         new Book()
                         {
                             Id = new Guid("9edf91ee-ab77-4521-a402-5f188bc0c577"),
                             Title = "American Gods",
                             Description = "American Gods is a Hugo and Nebula Award-winning novel by English author Neil Gaiman. The novel is a blend of Americana, fantasy, and various strands of ancient and modern mythology, all centering on the mysterious and taciturn Shadow."
                         }
                     }
                },
                new Author()
                {
                     Id = new Guid("578359b7-1967-41d6-8b87-64ab7605587e"),
                     FirstName = "Tom",
                     LastName = "Lanoye",
                     Genre = "Various",
                     DateOfBirth = new DateTimeOffset(new DateTime(1958, 8, 27)),
                     Books = new List<Book>()
                     {
                         new Book()
                         {
                             Id = new Guid("01457142-358f-495f-aafa-fb23de3d67e9"),
                             Title = "Speechless",
                             Description = "Good-natured and often humorous, Speechless is at times a 'song of curses', as Lanoye describes the conflicts with his beloved diva of a mother and her brave struggle with decline and death."
                         }
                     }
                },
                new Author()
                {
                     Id = new Guid("f74d6899-9ed2-4137-9876-66b070553f8f"),
                     FirstName = "Douglas",
                     LastName = "Adams",
                     Genre = "Science fiction",
                     DateOfBirth = new DateTimeOffset(new DateTime(1952, 3, 11)),
                     Books = new List<Book>()
                     {
                         new Book()
                         {
                             Id = new Guid("e57b605f-8b3c-4089-b672-6ce9e6d6c23f"),
                             Title = "The Hitchhiker's Guide to the Galaxy",
                             Description = "The Hitchhiker's Guide to the Galaxy is the first of five books in the Hitchhiker's Guide to the Galaxy comedy science fiction 'trilogy' by Douglas Adams."
                         }
                     }
                },
                new Author()
                {
                     Id = new Guid("a1da1d8e-1988-4634-b538-a01709477b77"),
                     FirstName = "Jens",
                     LastName = "Lapidus",
                     Genre = "Thriller",
                     DateOfBirth = new DateTimeOffset(new DateTime(1974, 5, 24)),
                     Books = new List<Book>()
                     {
                         new Book()
                         {
                             Id = new Guid("1325360c-8253-473a-a20f-55c269c20407"),
                             Title = "Easy Money",
                             Description = "Easy Money or Snabba cash is a novel from 2006 by Jens Lapidus. It has been a success in term of sales, and the paperback was the fourth best seller of Swedish novels in 2007."
                         }
                     }
                }
            };
        }

        private void GenerateNewBooks()
        {
            _books = _authors.SelectMany(author => author.Books.Select(book => new Book { Id = book.Id, AuthorId = author.Id, Description = book.Description, Title = book.Title })).ToList();
        }

        public void AddAuthor(Author author)
        {
            author.Id = Guid.NewGuid();
            _authors.Add(author);

            // the repository fills the id (instead of using identity columns)
            if (author.Books.Any())
            {
                foreach (var book in author.Books)
                {
                    book.Id = Guid.NewGuid();
                    book.AuthorId = author.Id;
                    _books.Add(book);
                }
            }
        }

        public void AddBookForAuthor(Guid authorId, Book book)
        {
            var author = GetAuthor(authorId);
            if (author != null)
            {
                // if there isn't an id filled out (ie: we're not upserting),
                // we should generate one
                if (book.Id == null || book.Id == new Guid())
                {
                    book.Id = Guid.NewGuid();
                }
                book.AuthorId = authorId;
                author.Books.Add(book);
                _books.Add(book);
            }
        }

        public bool AuthorExists(Guid authorId)
        {
            return _authors.Any(a => a.Id == authorId);
        }

        public void DeleteAuthor(Author author)
        {
            _books.RemoveAll(item => item.AuthorId == author.Id);
            _authors.Remove(author);

        }

        public void DeleteBook(Book book)
        {
            _books.Remove(book);
        }

        public Author GetAuthor(Guid authorId)
        {
            return _authors.FirstOrDefault(a => a.Id == authorId);
        }

        public PageList<Author> GetAuthors(AuthorsResourceParameters authorsResourceParameters)
        {
            if (!string.IsNullOrEmpty(authorsResourceParameters.Genre))
            {
                string genre = authorsResourceParameters.Genre.Trim().ToLowerInvariant();
                var collection = _authors.OrderBy(a => a.FirstName)
                                         .OrderBy(a => a.LastName)
                                         .Where(item => item.Genre.ToLowerInvariant() == genre).AsQueryable();
                return PageList<Author>.Create(collection, authorsResourceParameters.PageNumber, authorsResourceParameters.PageSize);
            }
            if (!string.IsNullOrEmpty(authorsResourceParameters.SearchQuery))
            {
                string searchQuery = authorsResourceParameters.SearchQuery.Trim().ToLowerInvariant();
                var collection = _authors.OrderBy(a => a.FirstName)
                                         .OrderBy(a => a.LastName)
                                         .Where(item => item.Genre.ToLowerInvariant().Contains(searchQuery) || 
                                                        item.FirstName.ToLowerInvariant().Contains(searchQuery) || 
                                                        item.LastName.ToLowerInvariant().Contains(searchQuery)).AsQueryable();
                return PageList<Author>.Create(collection, authorsResourceParameters.PageNumber, authorsResourceParameters.PageSize);
            }
            var collectionBeforePaging = _authors
                .OrderBy(a => a.FirstName)
                .OrderBy(a => a.LastName);
            return PageList<Author>.Create(collectionBeforePaging.AsQueryable(), authorsResourceParameters.PageNumber, authorsResourceParameters.PageSize);
        }

        public IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds)
        {
            return _authors.Where(a => authorIds.Contains(a.Id))
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .ToList();
        }

        public void UpdateAuthor(Author author)
        {
            // no code in this implementation
        }

        public Book GetBookForAuthor(Guid authorId, Guid bookId)
        {
            return _books.Where(b => b.AuthorId == authorId && b.Id == bookId).FirstOrDefault();
        }

        public IEnumerable<Book> GetBooksForAuthor(Guid authorId)
        {
            return _books.Where(b => b.AuthorId == authorId).OrderBy(b => b.Title).ToList();
        }

        public void UpdateBookForAuthor(Book book)
        {
            var bookToUpdate = _books.FirstOrDefault(item => item.Id == book.Id);
            bookToUpdate.Title = book.Title;
            bookToUpdate.Description = book.Description;
        }

        public bool Save()
        {
            return true;
        }
    }
}
