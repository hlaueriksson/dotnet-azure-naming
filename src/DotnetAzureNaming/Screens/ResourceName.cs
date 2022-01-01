using static System.Console;
using static ConsoleExtensions;

/// <summary>
/// Transform the azure resource into a resource name.
/// </summary>
public class ResourceName
{
    private readonly AzureResource _azureResource;

    public ResourceName(AzureResource azureResource) => _azureResource = azureResource;

    public static string Transform(string projectName, string componentName, Environment environment, AzureResourceType resourceType)
    {
        // try to transform the input to a resource name
        var resourceName = Transformer.Transform(
            $"{projectName} ${componentName} ${environment.Abbr} ${resourceType.Abbr}",
            resourceType.Transformer ?? "alphanumericsHyphens"
        );

        return resourceName;
    }

    public static ValidationResult Validate(string resourceName, AzureResourceType resourceType)
    {
        // if there is a resource name, validate it
        var validationResult = Validator.Validate(resourceName, resourceType);

        return validationResult;
    }

    public void Write()
    {
        var resourceName = Transform(_azureResource.ProjectName, _azureResource.ComponentName, _azureResource.Environment, _azureResource.ResourceType);
        var validationResult = Validate(resourceName, _azureResource.ResourceType);

        WriteLine();
        WriteLine($"COMPUTED NAME:  {resourceName}");
        if (validationResult != ValidationResult.Success)
            WriteError(validationResult.Message);
    }
}
