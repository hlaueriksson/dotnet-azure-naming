public record Environment
{
    public string Name { get; set; }
    public EnvironmentType Abbr { get; set; }

    public bool IsMatch(string value) => value != null && Abbr.ToString().Equals(value, StringComparison.InvariantCultureIgnoreCase);
}

public enum EnvironmentType
{
    dev,
    test,
    stage,
    prod
}
