public record AzureResourceType
{
    /// <summary>The resource type name.</summary>
    public string Type { get; set; }
    /// <summary>The resource type namespace.</summary>
    public string NS { get; set; }
    /// <summary>The resource type abbreviation.</summary>
    public string Abbr { get; set; }
    /// <summary>The transformer to use on the resource name.</summary>
    public string Transformer { get; set; }
    /// <summary>The name validations that apply to this resource type.</summary>
    public string[] Validations { get; set; }

    public bool IsMatch(string value) => Abbr.Equals(value, StringComparison.InvariantCultureIgnoreCase) || Type.Equals(value, StringComparison.InvariantCultureIgnoreCase);
}
