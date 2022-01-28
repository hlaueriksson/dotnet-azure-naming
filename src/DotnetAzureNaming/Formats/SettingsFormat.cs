using System.Text.Json;

public static class SettingsFormat
{
    public static void Write()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var result = JsonSerializer.Serialize(Settings.Current, options);
        result = result.Replace(@"\\", "/");
        Console.WriteLine(result);
    }
}
