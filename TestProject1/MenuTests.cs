using System.Diagnostics.CodeAnalysis;
using BookDepository1.Interfaces;
using BookDepository1.Models;
using BookDepository1.Services;
using Moq;

namespace TestProject1
{
    [ExcludeFromCodeCoverage]
    public class MenuTests
    {
        private Mock<IUserInput> _mockUserInput;
        private Mock<IUserOutput> _mockUserOutput;
        private Mock<IBookCatalog> _mockBookCatalog;
        private Menu _menu;

        [SetUp]
        public void Setup()
        {
            _mockUserInput = new Mock<IUserInput>();
            _mockUserOutput = new Mock<IUserOutput>();
            _mockBookCatalog = new Mock<IBookCatalog>();

            _menu = new Menu(_mockUserInput.Object, _mockUserOutput.Object, _mockBookCatalog.Object);
        }

        

        [Test]
        public async Task FindByTitleAsync_ShouldDisplayBooks_WhenBooksFound()
        {
            // Arrange
            var titleFragment = "Test";
            var books = new[] { new ConcreteBook("Test Book", "Test Author", new[] { "Fiction" }, 2023, "Test annotation", "1234567890") };
            _mockUserInput.Setup(input => input.ReadInput()).Returns(titleFragment);
            _mockBookCatalog.Setup(catalog => catalog.FindByTitleAsync(titleFragment)).ReturnsAsync(books);

            // Act
            await _menu.FindByTitleAsync();

            // Assert
            _mockUserOutput.Verify(output => output.WriteOutput("Название: Test Book, Автор: Test Author"), Times.Once);
        }

        [Test]
        public async Task FindByTitleAsync_ShouldDisplayNoBooks_WhenNoBooksFound()
        {
            // Arrange
            var titleFragment = "Nonexistent";
            _mockUserInput.Setup(input => input.ReadInput()).Returns(titleFragment);
            _mockBookCatalog.Setup(catalog => catalog.FindByTitleAsync(titleFragment)).ReturnsAsync(new ConcreteBook[0]);

            // Act
            await _menu.FindByTitleAsync();

            // Assert
            _mockUserOutput.Verify(output => output.WriteOutput("Книги не найдены."), Times.Once);
        }

        [Test]
        public async Task FindByAuthorAsync_ShouldDisplayBooks_WhenBooksFound()
        {
            // Arrange
            var authorName = "Test Author";
            var books = new[] { new ConcreteBook("Test Book", authorName, new[] { "Fiction" }, 2023, "Test annotation", "1234567890") };
            _mockUserInput.Setup(input => input.ReadInput()).Returns(authorName);
            _mockBookCatalog.Setup(catalog => catalog.FindByAuthorAsync(authorName)).ReturnsAsync(books);

            // Act
            await _menu.FindByAuthorAsync();

            // Assert
            _mockUserOutput.Verify(output => output.WriteOutput("Название: Test Book, Автор: Test Author"), Times.Once);
        }

        [Test]
        public async Task FindByAuthorAsync_ShouldDisplayNoBooks_WhenNoBooksFound()
        {
            // Arrange
            var authorName = "Nonexistent Author";
            _mockUserInput.Setup(input => input.ReadInput()).Returns(authorName);
            _mockBookCatalog.Setup(catalog => catalog.FindByAuthorAsync(authorName)).ReturnsAsync(new ConcreteBook[0]);

            // Act
            await _menu.FindByAuthorAsync();

            // Assert
            _mockUserOutput.Verify(output => output.WriteOutput("Книги не найдены."), Times.Once);
        }

        [Test]
        public async Task FindByISBNAsync_ShouldDisplayBook_WhenBookFound()
        {
            // Arrange
            var isbn = "1234567890";
            var book = new ConcreteBook("Test Book", "Test Author", new[] { "Fiction" }, 2023, "Test annotation", isbn);
            _mockUserInput.Setup(input => input.ReadInput()).Returns(isbn);
            _mockBookCatalog.Setup(catalog => catalog.FindByISBNAsync(isbn)).ReturnsAsync(book);

            // Act
            await _menu.FindByISBNAsync();

            // Assert
            _mockUserOutput.Verify(output => output.WriteOutput("Название: Test Book, Автор: Test Author"), Times.Once);
        }

        [Test]
        public async Task FindByISBNAsync_ShouldDisplayNoBooks_WhenNoBooksFound()
        {
            // Arrange
            var isbn = "NonexistentISBN";
            _mockUserInput.Setup(input => input.ReadInput()).Returns(isbn);
            _mockBookCatalog.Setup(catalog => catalog.FindByISBNAsync(isbn)).ReturnsAsync((ConcreteBook)null);

            // Act
            await _menu.FindByISBNAsync();

            // Assert
            _mockUserOutput.Verify(output => output.WriteOutput("Книги не найдены."), Times.Once);
        }
        
        [Test]
        public async Task ExitAsync_ShouldDisplayExitMessage()
        {
            // Act
            await _menu.ExitAsync();

            // Assert
            _mockUserOutput.Verify(output => output.WriteOutput("Выход из программы. До свидания!"), Times.Once);
        }
        
    }
}
