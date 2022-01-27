using Sharprompt;
using static System.Console;

public static class ConsoleFormat
{
    public static void Write(AzureResourceResult result, bool displayOptions)
    {
        if (displayOptions)
        {
            if (result.ResourceType != null)
                WriteLine($"Azure Resource: {result.ResourceType.Type}");
            else
                WriteError("Azure Resource*:");
            if (result.ProjectName != null)
                WriteLine($"Project Name:   {result.ProjectName}");
            else
                WriteError("Project Name*:");
            WriteLine($"Component Name: {result.ComponentName}");
            WriteLine($"Environment:    {result.Environment.Name}");
        }

        WriteLine();
        WriteLine($"COMPUTED NAME:  {result.ResourceName}");

        if (result.ValidationResult != ValidationResult.Success)
            WriteError(result.ValidationResult.Message);

        WriteLine($"RESOURCE GROUP: {result.ResourceGroup}");

        WriteLine("RESOURCE TAGS");
        WriteLine($"Project:        {result.ProjectName}");
        WriteLine($"Component:      {result.ComponentName}");
        WriteLine($"Environment:    {result.Environment.Name}");
    }

    private static void WriteError(string value)
    {
        ForegroundColor = Prompt.ColorSchema.Error;
        WriteLine(value);
        ResetColor();
    }
}
