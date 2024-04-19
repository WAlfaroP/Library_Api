using Library_WebApi.Dtos;
using MediatR;

namespace Library_WebApi.Commands
{
    public class AddBookCommand : IRequest<NewBookDto>
    {
    }
}