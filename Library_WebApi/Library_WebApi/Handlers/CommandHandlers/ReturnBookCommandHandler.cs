using Library_WebApi.Commands;
using Library_WebApi.Dtos;
using Library_WebApi.Interfaces;
using MediatR;

namespace Library_WebApi.Handlers.CommandHandlers
{
    public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, ReturnBookResultDto>
    {
        private readonly IBookRepository _bookRepository;

        public ReturnBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ReturnBookResultDto> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request), "The ReturnBookCommand cannot be null.");
                }

                if (request.BookId <= 0)
                {
                    throw new ArgumentException("Invalid BookId provided in the ReturnBookCommand.");
                }

                return await _bookRepository.ReturnBookAsync(request.BookId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to return the book.", ex);
            }
        }
    }
}