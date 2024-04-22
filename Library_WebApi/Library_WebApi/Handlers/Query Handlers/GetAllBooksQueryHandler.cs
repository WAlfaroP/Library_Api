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
            try
            {
                return await _bookRepository.GetAllBooksAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to fetch books.", ex);
            }
        }
    }
}