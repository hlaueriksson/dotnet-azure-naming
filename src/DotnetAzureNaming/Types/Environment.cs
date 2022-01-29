public record Environment
{
    public string Name { get; set; }
    public string Abbr { get; set; }

    public bool IsMatch(string value) => value != null && (Abbr.Equals(value, StringComparison.InvariantCultureIgnoreCase) || Name.Equals(value, StringComparison.InvariantCultureIgnoreCase));
}
