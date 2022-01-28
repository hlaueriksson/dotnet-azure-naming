using CommandLine;
using Microsoft.Extensions.Configuration;
using Sharprompt;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();
Settings.Current = config.GetSection("DotnetAzureNamingSettings").Get<Settings>();

AzureResourceResult result = null;

Parser.Default.ParseArguments<Options>(args)
.WithParsed(o =>
{
    if (o.Settings)
    {
        SettingsFormat.Write();
        return;
    }

    if (o.IsValid())
    {
        result = new(
            o.ProjectName,
            o.ComponentName,
            Environments.Find(o.Environment) ?? Environments.All().First(),
            AzureResourceTypes.Find(o.ResourceType));
    }
    else
    {
        // Resource Type
        var resourceType = Prompt.Select("Resource Type*", AzureResourceTypes.All(), 10, textSelector: x => x.Type);

        // Project Name
        var projectName = Prompt.Input<string>("Project Name*", validators: new[] { Validators.Required() });

        // Component Name
        var componentName = Prompt.Input<string>("Component Name");

        // Environment
        var environment = Prompt.Select("Environment", Environments.All(), defaultValue: Environments.All().First(), textSelector: x => x.Name);

        result = new(
            projectName,
            componentName,
            environment,
            resourceType);
    }

    switch (o.Format?.ToLower())
    {
        case "json":
            JsonFormat.Write(result);
            break;
        case "xml":
            XmlFormat.Write(result);
            break;
        default:
            ConsoleFormat.Write(result, o.IsValid());
            break;
    }
});

return result?.GetExitCode() ?? -1;
