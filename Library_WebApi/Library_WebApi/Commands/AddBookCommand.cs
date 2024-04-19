using Library_WebApi.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Library_WebApi.Commands
{
    public class AddBookCommand : IRequest<NewBookDto>
    {
        [Required]
        [MaxLength(100)]
        public required string Title { get; set; }

        [MaxLength(50)]
        public string Author { get; set; }

        [MaxLength(50)]
        public string Genre { get; set; }

        [Required]
        public DateTime PublicationDate { get; set; }

        public int Copies { get; set; } = 1;
    }
}