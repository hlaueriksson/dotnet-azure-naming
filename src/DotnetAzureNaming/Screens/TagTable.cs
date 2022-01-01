/// <summary>
/// Transform the azure resource into a resource name.
/// </summary>
public class TagTable
{
    private readonly AzureResource _azureResource;

    public TagTable(AzureResource azureResource) => _azureResource = azureResource;

    public void Write()
    {
        Console.WriteLine("RESOURCE TAGS");
        Console.WriteLine($"Project:        {_azureResource.ProjectName}");
        Console.WriteLine($"Component:      {_azureResource.ComponentName}");
        Console.WriteLine($"Environment:    {_azureResource.Environment.Name}");
    }
}
