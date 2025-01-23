using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;
using BookDepository1.Models;

namespace BookDepository1.Tests
{
    [ExcludeFromCodeCoverage]
    public class BookTests
    {
        private Book _book;

        [SetUp]
        public void SetUp()
        {
            // Инициализация тестового объекта Book
            _book = new Book(
                "The Great Gatsby", 
                "F. Scott Fitzgerald", 
                new string[] { "Fiction", "Classics" },
                1925, 
                "A novel about the American dream", 
                "9780743273565"
            );
        }

        [Test]
        public void Constructor_ShouldInitializeBookPropertiesCorrectly()
        {
            // Arrange
            var title = "The Great Gatsby";
            var author = "F. Scott Fitzgerald";
            var genres = new string[] { "Fiction", "Classics" };
            var publicationYear = 1925;
            var annotation = "A novel about the American dream";
            var isbn = "9780743273565";

            // Act
            var book = new Book(title, author, genres, publicationYear, annotation, isbn);

            // Assert
            Assert.That(book.Title, Is.EqualTo(title));
            Assert.That(book.Author, Is.EqualTo(author));
            Assert.That(book.Genres, Is.EquivalentTo(genres));
            Assert.That(book.PublicationYear, Is.EqualTo(publicationYear));
            Assert.That(book.Annotation, Is.EqualTo(annotation));
            Assert.That(book.ISBN, Is.EqualTo(isbn));
        }

        [Test]
        public void ContainsKeyword_ShouldReturnTrue_WhenKeywordIsInTitle()
        {
            // Arrange
            var keyword = "great";

            // Act
            var result = _book.ContainsKeyword(keyword);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ContainsKeyword_ShouldReturnTrue_WhenKeywordIsInAuthor()
        {
            // Arrange
            var keyword = "fitzgerald";

            // Act
            var result = _book.ContainsKeyword(keyword);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ContainsKeyword_ShouldReturnTrue_WhenKeywordIsInAnnotation()
        {
            // Arrange
            var keyword = "dream";

            // Act
            var result = _book.ContainsKeyword(keyword);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ContainsKeyword_ShouldReturnFalse_WhenKeywordIsNotInTitleAuthorOrAnnotation()
        {
            // Arrange
            var keyword = "unknown";

            // Act
            var result = _book.ContainsKeyword(keyword);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ContainsKeyword_ShouldBeCaseInsensitive()
        {
            // Arrange
            var keyword = "GREAT";

            // Act
            var result = _book.ContainsKeyword(keyword);

            // Assert
            Assert.That(result, Is.True);
        }
        
    }
}
