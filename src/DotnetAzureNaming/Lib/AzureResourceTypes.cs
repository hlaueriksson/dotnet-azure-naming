using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

/// <summary>
/// The source for these abbreviations is here.
/// https://docs.microsoft.com/en-us/azure/cloud-adoption-framework/ready/azure-best-practices/resource-abbreviations
///
/// The validation rules for each resource type is here.
/// https://docs.microsoft.com/en-us/azure/azure-resource-manager/management/resource-name-rules
///
/// Copy the file from:
/// https://raw.githubusercontent.com/klabbet/azure-naming/main/scripts/azure-resource-types.csv
/// </summary>
public static class AzureResourceTypes
{
    private static readonly Dictionary<string, AzureResourceType> _data;

    static AzureResourceTypes()
    {
        var path = Settings.Current.AzureResourceTypesPath;
        using (var reader = new StreamReader(path))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
        }))
        {
            csv.Context.RegisterClassMap<AzureResourceTypeMap>();
            _data = csv.GetRecords<AzureResourceType>().ToDictionary(x => x.Abbr);
        }
    }

    public static AzureResourceType[] All()
    {
        return _data.Select(x => x.Value).OrderBy(x => x.Type).ToArray();
    }

    public static AzureResourceType Find(string azureResource) => _data.FirstOrDefault(x => x.Value.IsMatch(azureResource)).Value;
}

public class AzureResourceTypeMap : ClassMap<AzureResourceType>
{
    public AzureResourceTypeMap()
    {
        Map(m => m.Type).Name("Asset type");
        Map(m => m.NS).Name("Resource provider namespace/Entity");
        Map(m => m.Abbr).Name("Abbreviation");
        Map(m => m.Transformer).Name("Transformer");
        Map(m => m.Validations).Convert(x => new[] { x.Row.GetField("Validator1"), x.Row.GetField("Validator2"), x.Row.GetField("Validator3"), x.Row.GetField("Validator4") }.Where(x => x.Any()).ToArray());
    }
}
