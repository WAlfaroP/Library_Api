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
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request), "The AddBookCommand cannot be null.");
                }

                return await _bookRepository.AddBookAsync(request);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to add a new book.");
            }
        }
    }
}