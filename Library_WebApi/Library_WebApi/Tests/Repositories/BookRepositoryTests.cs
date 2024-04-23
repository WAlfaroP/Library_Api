using Library_WebApi.Commands;
using Library_WebApi.Context;
using Library_WebApi.Models;
using Library_WebApi.Repositories;
using Library_WebApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class BookRepositoryTests
{
    private DbContextOptions<LibraryDbContext> _options;
    private MappingService _mappingService;

    [TestInitialize]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<LibraryDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _mappingService = new MappingService();
    }

    [TestMethod]
    public async Task GetAllBooksAsync_ReturnsAllBooks()
    {
        // Arrange
        using (var context = new LibraryDbContext(_options))
        {
            await InitializeDatabaseWithBooks(context);
            var repository = new BookRepository(context, _mappingService);

            // Act
            var result = await repository.GetAllBooksAsync();

            // Assert
            Assert.AreEqual(2, result.Count());
        }
    }

    [TestMethod]
    public async Task GetAllBooksAsync_ReturnsEmptyList_WhenNoBooksExist()
    {
        // Arrange
        using (var context = new LibraryDbContext(_options))
        {
            var repository = new BookRepository(context, _mappingService);

            // Act
            var result = await repository.GetAllBooksAsync();

            // Assert
            Assert.AreEqual(0, result.Count());
        }
    }

    [TestMethod]
    public async Task AddBookAsync_AddsNewBook_WhenBookDoesNotExist()
    {
        using (var context = new LibraryDbContext(_options))
        {
            var repository = new BookRepository(context, _mappingService);
            var command = new AddBookCommand
            {
                Title = "New Book",
                Author = "New Author",
                Genre = "New Genre",
                PublicationDate = new DateTime(2022, 4, 18),
                Copies = 1,
                IsBookExists = false
            };

            var result = await repository.AddBookAsync(command);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsBookExists);
            Assert.AreEqual(command.Title, result.Title);
            Assert.AreEqual(command.Author, result.Author);
            Assert.AreEqual(command.Genre, result.Genre);
            Assert.AreEqual(command.PublicationDate, result.PublicationDate);
            Assert.AreEqual(command.Copies, result.Copies);
        }
    }

    [TestMethod]
    public async Task AddBookAsync_ReturnsExistingBookDto_WhenBookExists()
    {
        using (var context = new LibraryDbContext(_options))
        {
            await InitializeDatabaseWithBook(context, "Existing Book", "Existing Author", new DateTime(2022, 4, 18));
            var repository = new BookRepository(context, _mappingService);
            var command = new AddBookCommand
            {
                Title = "Existing Book",
                Author = "Existing Author",
                Genre = "Existing Genre",
                PublicationDate = new DateTime(2022, 4, 18),
                Copies = 1,
                IsBookExists = true
            };

            var result = await repository.AddBookAsync(command);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsBookExists);
            Assert.AreEqual(command.Title, result.Title);
            Assert.AreEqual(command.Author, result.Author);
            Assert.AreEqual(command.Genre, result.Genre);
            Assert.AreEqual(command.PublicationDate, result.PublicationDate);
            Assert.AreEqual(command.Copies, result.Copies);
        }
    }

    [TestMethod]
    public async Task DeleteBookAsync_DeletesExistingBook_ReturnsDeletedBook()
    {
        using (var context = new LibraryDbContext(_options))
        {
            // Arrange
            var bookIdToDelete = 1;
            var bookToDelete = new Book { Id = bookIdToDelete, Title = "Book 1", Author = "Author 1", Genre = "Genre 1", PublicationDate = new DateTime(2022, 1, 1), Copies = 2 };

            context.Books.Add(bookToDelete);
            await context.SaveChangesAsync();

            var repository = new BookRepository(context, _mappingService);

            // Act
            var result = await repository.DeleteBookAsync(bookIdToDelete);

            // Assert
            Assert.IsTrue(result.IsDeleted);
            Assert.AreEqual(bookToDelete.Title, result.Title);
            Assert.AreEqual(bookToDelete.Author, result.Author);
            Assert.AreEqual(bookToDelete.PublicationDate, result.PublicationDate);
        }
    }

    [TestMethod]
    public async Task DeleteBookAsync_ThrowsException_WhenBookNotFound()
    {
        using (var context = new LibraryDbContext(_options))
        {
            // Arrange
            var bookIdToDelete = 1;

            var repository = new BookRepository(context, _mappingService);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => repository.DeleteBookAsync(bookIdToDelete));
        }
    }

    [TestMethod]
    public async Task BorrowBookAsync_BorrowsBookSuccessfully_WhenBookExistsAndCopiesAvailable()
    {
        using (var context = new LibraryDbContext(_options))
        {
            // Arrange
            var bookId = 1;
            var book = new Book { Id = bookId, Author = "Wilmar", Genre = "Mystery", Title = "Book 1", Copies = 4, BorrowedCopies = 0 };
            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();

            var repository = new BookRepository(context, _mappingService);

            // Act
            var result = await repository.BorrowBookAsync(bookId);

            // Assert
            Assert.IsTrue(result.IsBorrowSuccess);
            Assert.AreEqual(book.Copies, result.UpdatedCopies);
            Assert.AreEqual(book.BorrowedCopies, 1);
        }
    }

    [TestMethod]
    public async Task BorrowBookAsync_ThrowsException_WhenBookNotFound()
    {
        using (var context = new LibraryDbContext(_options))
        {
            // Arrange
            var nonExistentBookId = 999;
            var repository = new BookRepository(context, _mappingService);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => repository.BorrowBookAsync(nonExistentBookId));
        }
    }

    [TestMethod]
    public async Task BorrowBookAsync_ThrowsException_WhenNoCopiesAvailable()
    {
        using (var context = new LibraryDbContext(_options))
        {
            // Arrange
            var bookId = 1;
            var book = new Book { Id = bookId, Author = "Wilmar", Genre = "Mystery", Title = "Book 1", Copies = 0, BorrowedCopies = 0 };
            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();

            var repository = new BookRepository(context, _mappingService);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => repository.BorrowBookAsync(bookId));
        }
    }

    [TestMethod]
    public async Task ReturnBookAsync_ReturnsBookSuccessfully_WhenBookExists()
    {
        using (var context = new LibraryDbContext(_options))
        {
            // Arrange
            var bookId = 1;
            var book = new Book { Id = bookId, Author = "Wilmar", Genre = "Mystery", Title = "Book 1", Copies = 4, BorrowedCopies = 2 };
            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();

            var repository = new BookRepository(context, _mappingService);

            // Act
            var result = await repository.ReturnBookAsync(bookId);

            // Assert
            Assert.IsTrue(result.IsReturnSuccess);
            Assert.AreEqual(book.Copies , result.UpdatedCopies);
            Assert.AreEqual(book.BorrowedCopies, 1);
        }
    }

    [TestMethod]
    public async Task ReturnBookAsync_ThrowsException_WhenBookNotFound()
    {
        using (var context = new LibraryDbContext(_options))
        {
            // Arrange
            var nonExistentBookId = 999; // ID de un libro que no existe en la base de datos
            var repository = new BookRepository(context, _mappingService);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => repository.ReturnBookAsync(nonExistentBookId));
        }
    }

    private async Task InitializeDatabaseWithBook(LibraryDbContext context, string title, string author, DateTime publicationDate)
    {
        var book = new Book
        {
            Title = title,
            Author = author,
            Genre = "Test Genre",
            PublicationDate = publicationDate,
            Copies = 1
        };

        context.Books.Add(book);
        await context.SaveChangesAsync();
    }


    private async Task InitializeDatabaseWithBooks(LibraryDbContext context)
    {
        var books = new List<Book>
        {
            new Book { Title = "Book 1", Author = "Author 1", Genre = "Genre 1", PublicationDate = new DateTime(2022, 1, 1), Copies = 2 },
            new Book { Title = "Book 2", Author = "Author 2", Genre = "Genre 2", PublicationDate = new DateTime(2022, 1, 2), Copies = 3 }
        };

        context.Books.AddRange(books);
        await context.SaveChangesAsync();
    }
}
