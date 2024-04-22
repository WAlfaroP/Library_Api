using Library_WebApi.Commands;
using Library_WebApi.Dtos;
using Library_WebApi.Models;

namespace Library_WebApi.Interfaces
{
    public interface IMappingService
    {
        BookDto MapBookToDto(Book book);
        NewBookDto MapNewBookToDto(AddBookCommand command, bool isBookExists);
        DeleteBookResultDto MapBookToDeleteToDto(Book book, bool isDeleted);
        BorrowBookResultDto MapBookToBorrowToDto(Book book, bool IsBorrow);
        ReturnBookResultDto MapBookToReturnToDto(Book book, bool isReturnSuccess);
    }
}