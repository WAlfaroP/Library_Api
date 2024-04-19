using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_WebApi.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "The title of the book is required.")]
        [MaxLength(100, ErrorMessage = "The title of the book cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The author's name is required.")]
        [MaxLength(50, ErrorMessage = "The author's name cannot exceed 50 characters.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "The genre of the book is required.")]
        [MaxLength(50, ErrorMessage = "The genre of the book cannot exceed 50 characters.")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "The publication date of the book is required.")]
        [Column(TypeName = "date")]
        public DateTime PublicationDate { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The number of copies cannot be negative.")]
        public int Copies { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The number of borrowed copies cannot be negative.")]
        public int BorrowedCopies { get; set; } = 0;
    }
}