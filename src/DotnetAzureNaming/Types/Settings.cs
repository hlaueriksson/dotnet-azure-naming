public class Settings
{
    public string AzureResourceTypesPath { get; set; }
    public Environment[] Environments { get; set; }
    public string ResourceNameFormat { get; set; }
    public string ResourceGroupFormat { get; set; }

    public static Settings Current
    {
        get => _current ?? _default;
        internal set
        {
            _current = value ?? _default;
            if (string.IsNullOrEmpty(_current.AzureResourceTypesPath))
                _current.AzureResourceTypesPath = _default.AzureResourceTypesPath;
            if (_current.Environments == null || _current.Environments.Length == 0)
                _current.Environments = _default.Environments;
            if (string.IsNullOrEmpty(_current.ResourceNameFormat))
                _current.ResourceNameFormat = _default.ResourceNameFormat;
            if (string.IsNullOrEmpty(_current.ResourceGroupFormat))
                _current.ResourceGroupFormat = _default.ResourceGroupFormat;
        }
    }

    private static Settings _current;

    private static Settings _default => new()
    {
        AzureResourceTypesPath = "azure-resource-types.csv",
        Environments = new[]
        {
            new Environment { Name = "Development", Abbr = "dev" },
            new Environment { Name = "Test", Abbr = "test" },
            new Environment { Name = "Staging", Abbr = "stage" },
            new Environment { Name = "Production", Abbr = "prod" },
        },
        ResourceNameFormat = "{projectName} {componentName} {environment} {resourceType}",
        ResourceGroupFormat = "{projectName} {componentName} {environment} rg",
    };
}
