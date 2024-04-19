using Library_WebApi.Commands;
using Library_WebApi.Dtos;
using Library_WebApi.Interfaces;
using MediatR;

namespace Library_WebApi.Handlers.CommandHandlers
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, NewBookDto>
    {
        private readonly IBookRepository _bookRepository;
        public AddBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<NewBookDto> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            return new NewBookDto { };
        }
    }
}