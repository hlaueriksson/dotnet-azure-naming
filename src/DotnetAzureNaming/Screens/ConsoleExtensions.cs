using Sharprompt;

public static class ConsoleExtensions
{
    public static void WriteError(string value)
    {
        Console.ForegroundColor = Prompt.ColorSchema.Error;
        Console.WriteLine(value);
        Console.ResetColor();
    }
}
