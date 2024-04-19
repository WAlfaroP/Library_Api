using Library_WebApi.Dtos;

namespace Library_WebApi.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
    }
}