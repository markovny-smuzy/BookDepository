using BookDepository1.Interfaces;

namespace BookDepository1.Models;

public class Book : IBook
{
    public string Title { get; }
    public string Author { get; }
    public string[] Genres { get; }
    public int PublicationYear { get; }
    public string Annotation { get; }
    public string ISBN { get; }

    public Book(string title, string author, string[] genres, int publicationYear, string annotation, string isbn)
    {
        Title = title;
        Author = author;
        Genres = genres;
        PublicationYear = publicationYear;
        Annotation = annotation;
        ISBN = isbn;
    }

    public bool ContainsKeyword(string keyword)
    {
        keyword = keyword.ToLower();
        return Title.ToLower().Contains(keyword) ||
               Author.ToLower().Contains(keyword) ||
               Annotation.ToLower().Contains(keyword);
    }
}