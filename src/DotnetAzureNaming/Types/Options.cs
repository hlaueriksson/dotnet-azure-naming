using CommandLine;

public class Options
{
    [Option('r', "resource-type", Required = false, HelpText = "Azure Resource Type")]
    public string ResourceType { get; set; }

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
        return ResourceType != null && ProjectName != null;
    }
}
