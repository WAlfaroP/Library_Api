using Library_WebApi.Context;
using Library_WebApi.Dtos;
using Library_WebApi.Interfaces;
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
    }
}