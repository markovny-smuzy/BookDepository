using System.Diagnostics.CodeAnalysis;
using BookDepository1.Models;

namespace TestProject1
{
    [ExcludeFromCodeCoverage]
    public class ConcreteBookTests
    {
        private ConcreteBook _concreteBook;

        [SetUp]
        public void SetUp()
        {
            // Инициализация тестового объекта ConcreteBook
            _concreteBook = new ConcreteBook(
                "1984", 
                "George Orwell", 
                new string[] { "Dystopian", "Political Fiction" },
                1949, 
                "A dystopian social science fiction novel and cautionary tale.",
                "9780451524935"
            );
        }

        [Test]
        public void Constructor_ShouldInitializeConcreteBookPropertiesCorrectly()
        {
            // Arrange
            var title = "1984";
            var author = "George Orwell";
            var genres = new string[] { "Dystopian", "Political Fiction" };
            var publicationYear = 1949;
            var annotation = "A dystopian social science fiction novel and cautionary tale.";
            var isbn = "9780451524935";

            // Act
            var concreteBook = new ConcreteBook(title, author, genres, publicationYear, annotation, isbn);

            // Assert
            Assert.That(concreteBook.Title, Is.EqualTo(title));
            Assert.That(concreteBook.Author, Is.EqualTo(author));
            Assert.That(concreteBook.Genres, Is.EquivalentTo(genres));
            Assert.That(concreteBook.PublicationYear, Is.EqualTo(publicationYear));
            Assert.That(concreteBook.Annotation, Is.EqualTo(annotation));
            Assert.That(concreteBook.ISBN, Is.EqualTo(isbn));
        }

        [Test]
        public void ContainsKeyword_ShouldReturnTrue_WhenKeywordIsInTitle()
        {
            // Arrange
            var keyword = "1984";

            // Act
            var result = _concreteBook.ContainsKeyword(keyword);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ContainsKeyword_ShouldReturnTrue_WhenKeywordIsInAuthor()
        {
            // Arrange
            var keyword = "orwell";

            // Act
            var result = _concreteBook.ContainsKeyword(keyword);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ContainsKeyword_ShouldReturnTrue_WhenKeywordIsInAnnotation()
        {
            // Arrange
            var keyword = "dystopian";

            // Act
            var result = _concreteBook.ContainsKeyword(keyword);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ContainsKeyword_ShouldReturnFalse_WhenKeywordIsNotInTitleAuthorOrAnnotation()
        {
            // Arrange
            var keyword = "utopia";

            // Act
            var result = _concreteBook.ContainsKeyword(keyword);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ContainsKeyword_ShouldBeCaseInsensitive()
        {
            // Arrange
            var keyword = "DYSTOPIAN";

            // Act
            var result = _concreteBook.ContainsKeyword(keyword);

            // Assert
            Assert.That(result, Is.True);
        }
        
    }
}
