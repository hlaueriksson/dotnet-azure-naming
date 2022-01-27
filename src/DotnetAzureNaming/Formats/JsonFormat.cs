using System.Text.Json;

public static class JsonFormat
{
    public static void Write(AzureResourceResult result)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        Console.WriteLine(JsonSerializer.Serialize(result, options));
    }
}
