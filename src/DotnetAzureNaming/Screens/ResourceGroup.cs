/// <summary>
/// Transform the azure resource to a resource group name.
/// </summary>
public static class ResourceGroup
{
    public static string Transform(string projectName, string componentName, Environment environment)
    {
        // transform the appropriate name for the resource group
        var resourceGroup = Transformer.Transform(
            $"{projectName} ${componentName} ${environment.Abbr} rg",
            "alphanumericsHyphens"
        );

        return resourceGroup;
    }
}
