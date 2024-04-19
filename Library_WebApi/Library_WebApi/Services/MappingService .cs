using Library_WebApi.Commands;
using Library_WebApi.Dtos;
using Library_WebApi.Interfaces;
using Library_WebApi.Models;

namespace Library_WebApi.Services
{
    public class MappingService : IMappingService 
    {
        public BookDto MapBookToDto(Book book)
        {
            return new BookDto
            {
                BookId = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                PublicationDate = book.PublicationDate,
                Copies = book.Copies,
                BorrowedCopies = book.BorrowedCopies
            };
        }
        public NewBookDto MapNewBookToDto(AddBookCommand book)
        {
            return new NewBookDto
            {
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                PublicationDate = book.PublicationDate,
                Copies = book.Copies
            };
        }
        public DeleteBookResultDto MapBookToDeleteToDto(Book book, bool isDeleted)
        {
            return new DeleteBookResultDto
            {
                IsDeleted = isDeleted,
                Title = book.Title,
                Author = book.Author,
                PublicationDate = book.PublicationDate
            };
        }
        public BorrowBookResultDto MapBookToBorrowToDto(Book book, bool IsBorrowSuccess)
        {
            return new BorrowBookResultDto
            {
                IsBorrowSuccess = IsBorrowSuccess,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                PublicationDate = book.PublicationDate,
                UpdatedCopies = book.Copies
            };
        }
        public ReturnBookResultDto MapBookToReturnToDto(Book book, bool isReturnSuccess)
        {
            return new ReturnBookResultDto
            {
                IsReturnSuccess = isReturnSuccess,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                PublicationDate = book.PublicationDate,
                UpdatedCopies = book.Copies
            };
        }
    }
}