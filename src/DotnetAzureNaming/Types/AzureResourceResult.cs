public class AzureResourceResult
{
    // Input
    public string ProjectName { get; set; }
    public string ComponentName { get; set; }
    public Environment Environment { get; set; }
    public AzureResourceType ResourceType { get; set; }

    // Output
    public string ResourceName { get; set; }
    public string ResourceGroup { get; set; }
    public ValidationResult ValidationResult { get; set; }

    internal AzureResourceResult()
    {
        // for XmlFormat
    }

    public AzureResourceResult(string projectName, string componentName, Environment environment, AzureResourceType resourceType)
    {
        ProjectName = projectName;
        ComponentName = componentName;
        Environment = environment;
        ResourceType = resourceType;

        if (ResourceType == null)
        {
            ValidationResult = new ValidationResult { Message = "Resource type is invalid" };
            return;
        }
        if (ProjectName == null)
        {
            ValidationResult = new ValidationResult { Message = "Project name is invalid" };
            return;
        }

        ResourceName = GetResourceName();
        ResourceGroup = GetResourceGroup();
        ValidationResult = Validate(ResourceName);
    }

    /// <summary>
    /// Transform the azure resource into a resource name.
    /// </summary>
    /// <returns>Resource name.</returns>
    private string GetResourceName()
    {
        // try to transform the input to a resource name
        return
            Transformer.Transform(
                Settings.Current.ResourceNameFormat
                .Replace("{projectName}", ProjectName)
                .Replace("{componentName}", ComponentName)
                .Replace("{environment}", Environment.Abbr)
                .Replace("{resourceType}", ResourceType.Abbr),
                ResourceType.Transformer ?? "alphanumericsHyphens");
    }

    /// <summary>
    /// Transform the azure resource to a resource group name.
    /// </summary>
    /// <returns>Resource group name.</returns>
    private string GetResourceGroup()
    {
        // transform the appropriate name for the resource group
        return
            Transformer.Transform(
                Settings.Current.ResourceGroupFormat
                .Replace("{projectName}", ProjectName)
                .Replace("{componentName}", ComponentName)
                .Replace("{environment}", Environment.Abbr),
                "alphanumericsHyphens");
    }

    private ValidationResult Validate(string resourceName)
    {
        // if there is a resource name, validate it
        return Validator.Validate(resourceName, ResourceType);
    }
}
