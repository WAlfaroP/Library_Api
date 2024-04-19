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
            return new BorrowBookResultDto { };
        }
    }
}