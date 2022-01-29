using System.Reflection;
using CommandLine;
using CommandLine.Text;

[assembly: AssemblyCopyright(@"
This tool will help you name Azure Resources.
It follows the naming convention defined by Klabbet (https://tinyurl.com/klabbet-naming).
You can create your own naming convention by editing the appsettings.json file.
See instructions at: https://github.com/hlaueriksson/dotnet-azure-naming

The original, online version of this tool can be found at: https://azure-naming.klabbet.com/
")]

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

    [Option('f', "format", Required = false, HelpText = "Format the result as {JSON|XML}")]
    public string Format { get; set; }

    [Option("settings", Required = false, HelpText = "Display current settings.")]
    public bool Settings { get; set; }

    [Usage(ApplicationAlias = "azure-naming")]
    public static IEnumerable<Example> Examples
    {
        get
        {
            yield return new Example("Long", new Options { ResourceType = "Function app", ProjectName = "Titanic", ComponentName = "Web", Environment = "Development" });
            yield return new Example("Short", new UnParserSettings { PreferShortName = true }, new Options { ResourceType = "func", ProjectName = "Titanic", ComponentName = "Web", Environment = "dev" });
            yield return new Example("Format as JSON", new UnParserSettings { PreferShortName = true }, new Options { ResourceType = "func", ProjectName = "Titanic", ComponentName = "Web", Environment = "dev", Format = "json" });
        }
    }

    public bool IsValid()
    {
        return ResourceType != null && ProjectName != null;
    }
}
