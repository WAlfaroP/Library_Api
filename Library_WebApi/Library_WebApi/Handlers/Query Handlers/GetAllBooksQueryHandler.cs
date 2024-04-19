using Library_WebApi.Dtos;
using Library_WebApi.Interfaces;
using Library_WebApi.Queries;
using MediatR;

namespace Library_WebApi.Handlers.Query_Handlers
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<BookDto>>
    {
        private readonly IBookRepository _bookRepository;

        public GetAllBooksQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return new List<BookDto> { };
        }
    }
}