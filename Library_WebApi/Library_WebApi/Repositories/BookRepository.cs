using Library_WebApi.Context;
using Library_WebApi.Interfaces;

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
    }
}