using Library_WebApi.Commands;
using Library_WebApi.Dtos;
using Library_WebApi.Interfaces;
using MediatR;

namespace Library_WebApi.Handlers.CommandHandlers
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, DeleteBookResultDto>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<DeleteBookResultDto> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request), "The DeleteBookCommand cannot be null.");
                }

                if (request.BookId <= 0)
                {
                    throw new Exception("Invalid BookId provided in the DeleteBookCommand.");
                }

                return await _bookRepository.DeleteBookAsync(request.BookId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to delete the book.", ex);
            }
        }
    }
}