using Sharprompt;

// Azure Resource
var resourceType = Prompt.Select("Azure Resource*", AzureResourceTypes.All(), 10, textSelector: x => x.Type);

// Project Name
var projectName = Prompt.Input<string>("Project Name*", validators: new[] { Validators.Required() });

// Component Name
var componentName = Prompt.Input<string>("Component Name");

// Environment
var environment = Prompt.Select("Environment", Environments.All(), defaultValue: Environments.All().First(), textSelector: x => x.Name);

// ResourceName
var resourceName = ResourceName.Transform(projectName, componentName, environment, resourceType);
var validationResult = ResourceName.Validate(resourceName, resourceType);
Console.WriteLine($"COMPUTED NAME: {resourceName}");
if (validationResult != ValidationResult.Success)
{
    Console.ForegroundColor = Prompt.ColorSchema.Error;
    Console.WriteLine(validationResult.Message);
    Console.ResetColor();
}

// ResourceGroup
var resourceGroup = ResourceGroup.Transform(projectName, componentName, environment);
Console.WriteLine($"RESOURCE GROUP: {resourceGroup}");
Console.WriteLine("RESOURCE TAGS");
Console.WriteLine($"Project: {projectName}");
Console.WriteLine($"Component: {componentName}");
Console.WriteLine($"Environment: {environment.Name}");
