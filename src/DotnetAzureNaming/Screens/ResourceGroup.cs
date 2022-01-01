/// <summary>
/// Transform the azure resource to a resource group name.
/// </summary>
public class ResourceGroup
{
    private readonly AzureResource _azureResource;

    public ResourceGroup(AzureResource azureResource) => _azureResource = azureResource;

    public static string Transform(string projectName, string componentName, Environment environment)
    {
        // transform the appropriate name for the resource group
        var resourceGroup = Transformer.Transform(
            $"{projectName} ${componentName} ${environment.Abbr} rg",
            "alphanumericsHyphens"
        );

        return resourceGroup;
    }

    public void Write()
    {
        var resourceGroup = Transform(_azureResource.ProjectName, _azureResource.ComponentName, _azureResource.Environment);

        Console.WriteLine($"RESOURCE GROUP: {resourceGroup}");
    }
}
