using static System.Console;
using static ConsoleExtensions;

public class AzureResource
{
    public string ProjectName { get; set; }
    public string ComponentName { get; set; }
    public Environment Environment { get; set; }
    public AzureResourceType ResourceType { get; set; }

    public void Write()
    {
        if (ResourceType != null)
            WriteLine($"Azure Resource: {ResourceType.Type}");
        else
            WriteError("Azure Resource*:");
        if (ProjectName != null)
            WriteLine($"Project Name:   {ProjectName}");
        else
            WriteError("Project Name*:");
        WriteLine($"Component Name: {ComponentName}");
        WriteLine($"Environment:    {Environment.Name}");
    }

    public bool IsValid()
    {
        return ResourceType != null && ProjectName != null;
    }
}
