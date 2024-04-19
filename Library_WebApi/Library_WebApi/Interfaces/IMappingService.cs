using Library_WebApi.Commands;
using Library_WebApi.Dtos;
using Library_WebApi.Models;

namespace Library_WebApi.Interfaces
{
    public interface IMappingService
    {
        BookDto MapBookToDto(Book book);
    }
}