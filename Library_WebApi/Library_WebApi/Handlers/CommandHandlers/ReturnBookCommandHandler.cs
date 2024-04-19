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
            return new ReturnBookResultDto { };
        }
    }
}