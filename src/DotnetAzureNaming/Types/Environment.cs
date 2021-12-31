public record Environment
{
    public string Name { get; set; }
    public EnvironmentType Abbr { get; set; }
}

public enum EnvironmentType
{
    dev,
    test,
    stage,
    prod
}
