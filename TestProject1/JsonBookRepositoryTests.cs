using System.Diagnostics.CodeAnalysis;
using BookDepository1.Interfaces;
using BookDepository1.Models;
using Moq;

namespace TestProject1
{
    [ExcludeFromCodeCoverage]
    public class JsonBookRepositoryTests
    {
        private Mock<ISerializer> _serializerMock;
        private JsonBookRepository _jsonBookRepository;
        private ConcreteBook _book;

        [SetUp]
        public void SetUp()
        {
            // Мокируем ISerializer
            _serializerMock = new Mock<ISerializer>();
            
            // Мокируем метод DeserializeAsync для тестирования LoadBooks
            _serializerMock.Setup(s => s.DeserializeAsync<List<ConcreteBook>>(It.IsAny<string>()))
                .ReturnsAsync(new List<ConcreteBook>()); // Возвращаем пустой список, как будто файл пуст

            // Создаем объект JsonBookRepository с замокированным ISerializer
            _jsonBookRepository = new JsonBookRepository(_serializerMock.Object);
            
            // Создаем книгу для тестов
            _book = new ConcreteBook("1984", "George Orwell", new string[] { "Dystopian", "Fiction" }, 1949, "A novel about a totalitarian regime.", "9780451524935");
        }

        [Test]
        public async Task AddBookAsync_ShouldAddBookToTheList()
        {
            // Act
            await _jsonBookRepository.AddBookAsync(_book);

            // Assert
            // Проверяем, что книга была добавлена
            var books = await _jsonBookRepository.FindByTitleAsync("1984");
            Assert.That(books.Any(), Is.True); // Проверяем, что коллекция не пуста
            Assert.That(books.First().Title, Is.EqualTo("1984"));
        }

        [Test]
        public async Task FindByTitleAsync_ShouldReturnCorrectBooks()
        {
            // Arrange
            await _jsonBookRepository.AddBookAsync(_book);
            var anotherBook = new ConcreteBook("Animal Farm", "George Orwell", new string[] { "Dystopian", "Political Fiction" }, 1945, "A political satire.", "9780451524936");
            await _jsonBookRepository.AddBookAsync(anotherBook);

            // Act
            var books = await _jsonBookRepository.FindByTitleAsync("1984");

            // Assert
            Assert.That(books.Any(), Is.True); // Проверяем, что коллекция не пуста
            Assert.That(books.First().Title, Is.EqualTo("1984"));
        }

        [Test]
        public async Task FindByAuthorAsync_ShouldReturnCorrectBooks()
        {
            // Arrange
            await _jsonBookRepository.AddBookAsync(_book);
            var anotherBook = new ConcreteBook("Animal Farm", "George Orwell", new string[] { "Dystopian", "Political Fiction" }, 1945, "A political satire.", "9780451524936");
            await _jsonBookRepository.AddBookAsync(anotherBook);

            // Act
            var books = await _jsonBookRepository.FindByAuthorAsync("George Orwell");

            // Assert
            Assert.That(books.Any(), Is.True); // Проверяем, что коллекция не пуста
        }

        [Test]
        public async Task FindByISBNAsync_ShouldReturnCorrectBook()
        {
            // Arrange
            await _jsonBookRepository.AddBookAsync(_book);

            // Act
            var book = await _jsonBookRepository.FindByISBNAsync("9780451524935");

            // Assert
            Assert.That(book, Is.Not.Null);
            Assert.That(book.ISBN, Is.EqualTo("9780451524935"));
        }

        [Test]
        public async Task FindByISBNAsync_ShouldReturnNull_WhenBookNotFound()
        {
            // Act
            var book = await _jsonBookRepository.FindByISBNAsync("NonExistingISBN");

            // Assert
            Assert.That(book, Is.Null);
        }

        [Test]
        public async Task LoadBooks_ShouldReturnEmptyList_WhenFileDoesNotExist()
        {
            // Arrange
            _serializerMock.Setup(s => s.DeserializeAsync<List<ConcreteBook>>(It.IsAny<string>())).Throws<System.IO.FileNotFoundException>();

            // Act
            var books = await _jsonBookRepository.FindByTitleAsync("NonExistingBook");

            // Assert
            Assert.That(books.Any(), Is.False); // Проверяем, что коллекция пуста
        }

        [Test]
        public async Task LoadBooks_ShouldReturnEmptyList_WhenDeserializationFails()
        {
            // Arrange
            _serializerMock.Setup(s => s.DeserializeAsync<List<ConcreteBook>>(It.IsAny<string>())).Throws<System.Exception>();

            // Act
            var books = await _jsonBookRepository.FindByTitleAsync("NonExistingBook");

            // Assert
            Assert.That(books.Any(), Is.False); // Проверяем, что коллекция пуста
        }
    }
}
