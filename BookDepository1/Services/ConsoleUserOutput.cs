using BookDepository1.Interfaces;

namespace BookDepository1.Services;

public class ConsoleUserOutput : IUserOutput
{
    public void WriteOutput(string message)
    {
        Console.ForegroundColor = message.StartsWith("Ошибка") ? ConsoleColor.Red : ConsoleColor.White;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}