/// <summary>
/// Transform the azure resource into a resource name.
/// </summary>
public static class ResourceName
{
    public static string Transform(string projectName, string componentName, Environment environment, AzureResourceType resourceType)
    {
        // try to transform the input to a resource name
        var resourceName = Transformer.Transform(
            $"{projectName} ${componentName} ${environment.Abbr} ${resourceType.Abbr}",
            AzureResourceTypes.FindTransformerName(resourceType.Abbr) ?? "alphanumericsHyphens"
        );

        return resourceName;
    }

    public static ValidationResult Validate(string resourceName, AzureResourceType resourceType)
    {
        // if there is a resource name, validate it
        var validationResult = Validator.Validate(resourceName, resourceType.Abbr);

        return validationResult;
    }
}
