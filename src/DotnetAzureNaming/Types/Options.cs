using CommandLine;

public class Options
{
    [Option('r', "resource-type", Required = false, HelpText = "Azure Resource Type\nAzure Resource Type SHOULD be one of the types in the Microsoft list of Azure resource type abbreviations (https://tinyurl.com/az-abbr).\nIf the azure type is not in the list, you should make up your own abbreviation that doesn't conflict with any of the official ones.")]
    public string ResourceType { get; set; }

    [Option('p', "project-name", Required = false, HelpText = "Project Name\nYou MUST include a project that that MAY be the application name.\nYou SHOULD find a short name for your project or application that is easy to understand without specific domain knowledge.\nYou SHOULD NOT include redundant information in your name, i.e. the name of your company.\nYou SHOULD NOT use abbreviations in your name.")]
    public string ProjectName { get; set; }

    [Option('c', "component-name", Required = false, HelpText = "Component Name\nYou SHOULD include a component name if your project or application consists of several components. Examples of components are web, api, service.\nYou SHOULD NOT use resource type as component name, i.e. database, function, insights, vm.\nYou MAY omit the component name if the project name is sufficient.")]
    public string ComponentName { get; set; }

    [Option('e', "environment", Required = false, HelpText = "Environment\nYou MUST use the correct environment specifier for your environment.\nSpecifier:\tEnvironment:\ndev\t\tDevelopment\ntest\t\tTest\nstage\t\tStaging\nprod\t\tProduction\nYou MAY add custom environment specifiers to your naming convention.")]
    public string Environment { get; set; }

    [Option('f', "format", Required = false, HelpText = "Format the result as {json|xml}")]
    public string Format { get; set; }

    [Option("settings", Required = false, HelpText = "Display current settings.")]
    public bool Settings { get; set; }

    public bool IsValid()
    {
        return ResourceType != null && ProjectName != null;
    }
}
