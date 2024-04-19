using Library_WebApi.Commands;
using Library_WebApi.Dtos;
using Library_WebApi.Interfaces;
using MediatR;

namespace Library_WebApi.Handlers.CommandHandlers
{
    public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, BorrowBookResultDto>
    {
        private readonly IBookRepository _bookRepository;

        public BorrowBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<BorrowBookResultDto> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request), "The BorrowBookCommand cannot be null.");
                }

                if (request.BookId <= 0)
                {
                    throw new ArgumentException("Invalid BookId provided in the BorrowBookCommand.");
                }

                return await _bookRepository.BorrowBookAsync(request.BookId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to borrow the book.", ex);
            }
        }
    }
}