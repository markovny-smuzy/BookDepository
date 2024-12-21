using BookDepository1.Models;
using BookDepository1.Services;
namespace BookDepository1;

class Program
{
    static async Task Main()
    {
        var userInput = new ConsoleUserInput();
        var userOutput = new ConsoleUserOutput();
        var serializer = new JsonSerializer();
        var bookCatalog = new JsonBookRepository(serializer);
        var menu = new Menu(userInput, userOutput, bookCatalog);
        await menu.ShowAsync();
    }
}