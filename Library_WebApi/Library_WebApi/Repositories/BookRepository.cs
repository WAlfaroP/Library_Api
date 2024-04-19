using Library_WebApi.Commands;
using Library_WebApi.Context;
using Library_WebApi.Dtos;
using Library_WebApi.Interfaces;
using Library_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_WebApi.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;
        private readonly IMappingService _mappingService;

        public BookRepository(LibraryDbContext context, IMappingService mappingService)
        {
            _context = context;
            _mappingService = mappingService;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            try
            {
                var books = await _context.Books.ToListAsync();

                return books.Select(book => _mappingService.MapBookToDto(book))
                            .ToList();

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to retrieve the books from the database.", ex);
            }
        }
        public async Task<NewBookDto> AddBookAsync(AddBookCommand command)
        {
            try
            {
                var bookExists = await BookExistsAsync(command.Title, command.Author, command.PublicationDate);

                if (bookExists)
                {
                    throw new ArgumentException("The book already exists.");
                }

                _context.Books.Add(new Book
                {
                    Title = command.Title,
                    Author = command.Author,
                    Genre = command.Genre,
                    PublicationDate = command.PublicationDate,
                    Copies = command.Copies
                });

                await _context.SaveChangesAsync();

                return _mappingService.MapNewBookToDto(command);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to add a new book to the database.", ex);
            }
        }

        private async Task<bool> BookExistsAsync(string title, string author, DateTime publicationDate)
        {
            return await _context.Books
                                .AnyAsync(b =>
                                  string.Equals(b.Title, title, StringComparison.OrdinalIgnoreCase) &&
                                  string.Equals(b.Author, author, StringComparison.OrdinalIgnoreCase) &&
                                  b.PublicationDate.Date == publicationDate.Date);
        }
    }
}