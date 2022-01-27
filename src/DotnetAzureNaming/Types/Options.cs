using CommandLine;

public class Options
{
    [Option('a', "azure-resource", Required = false, HelpText = "Azure Resource Type")]
    public string AzureResource { get; set; }

    [Option('p', "project-name", Required = false, HelpText = "Project Name")]
    public string ProjectName { get; set; }

    [Option('c', "component-name", Required = false, HelpText = "Component Name")]
    public string ComponentName { get; set; }

    [Option('e', "environment", Required = false, HelpText = "Environment")]
    public string Environment { get; set; }

    [Option('f', "format", Required = false, HelpText = "Format the result as {json|xml}")]
    public string Format { get; set; }

    public bool IsValid()
    {
        return AzureResource != null && ProjectName != null;
    }
}
