using Library_WebApi.Dtos;
using MediatR;

namespace Library_WebApi.Commands
{
    public class ReturnBookCommand : IRequest<ReturnBookResultDto>
    {

    }
}