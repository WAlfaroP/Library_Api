using Library_WebApi.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Library_WebApi.Commands
{
    public class ReturnBookCommand : IRequest<ReturnBookResultDto>
    {
        [Required]
        public int BookId { get; set; }
    }
}