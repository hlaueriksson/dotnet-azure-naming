using CommandLine;
using Microsoft.Extensions.Configuration;
using Sharprompt;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();
Settings.Current = config.GetSection("Settings").Get<Settings>();

AzureResourceResult result = null;

Parser.Default.ParseArguments<Options>(args)
.WithParsed(o =>
{
    if (o.IsValid())
    {
        result = new(
            o.ProjectName,
            o.ComponentName,
            Environments.Find(o.Environment) ?? Environments.All().First(),
            AzureResourceTypes.Find(o.AzureResource));
    }
    else
    {
        // Azure Resource
        var resourceType = Prompt.Select("Azure Resource*", AzureResourceTypes.All(), 10, textSelector: x => x.Type);

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
})
.WithNotParsed(e =>
{
    Console.WriteLine("Errors: " + string.Join(", ", e.Select(x => x.Tag)));
});

return result.ValidationResult == ValidationResult.Success ? 0 : -1;
