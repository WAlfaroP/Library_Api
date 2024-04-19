using Library_WebApi.Dtos;
using MediatR;

namespace Library_WebApi.Queries
{
    public class GetAllBooksQuery : IRequest<IEnumerable<BookDto>> { }
}