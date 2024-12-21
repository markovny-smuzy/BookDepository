namespace BookDepository1.Services;

using BookDepository1.Interfaces;

public class ConsoleUserInput : IUserInput
{
    public string ReadInput() => Console.ReadLine() ?? string.Empty;
}