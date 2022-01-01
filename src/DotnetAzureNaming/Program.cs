using CommandLine;
using Sharprompt;

Parser.Default.ParseArguments<Options>(args)
.WithParsed(o =>
{
    AzureResource azureResource = null;

    if (o.IsValid())
    {
        azureResource = new AzureResource
        {
            ProjectName = o.ProjectName,
            ComponentName = o.ComponentName,
            Environment = Environments.Find(o.Environment) ?? Environments.All.First(),
            ResourceType = AzureResourceTypes.Find(o.AzureResource),
        };

        azureResource.Write();

        if (!azureResource.IsValid()) return;
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
        var environment = Prompt.Select("Environment", Environments.All, defaultValue: Environments.All.First(), textSelector: x => x.Name);

        azureResource = new AzureResource
        {
            ProjectName = projectName,
            ComponentName = componentName,
            Environment = environment,
            ResourceType = resourceType,
        };
    }

    // ResourceName
    new ResourceName(azureResource).Write();

    // ResourceGroup
    new ResourceGroup(azureResource).Write();

    // TagTable
    new TagTable(azureResource).Write();
})
.WithNotParsed(e =>
{
    Console.WriteLine("Errors: " + string.Join(", ", e.Select(x => x.Tag)));
});
