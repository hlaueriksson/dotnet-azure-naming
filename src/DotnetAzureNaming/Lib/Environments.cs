public static class Environments
{
    private static readonly Dictionary<EnvironmentType, Environment> _environments = new()
    {
        { EnvironmentType.dev, new Environment { Name = "Development", Abbr = EnvironmentType.dev } },
        { EnvironmentType.test, new Environment { Name = "Test", Abbr = EnvironmentType.test } },
        { EnvironmentType.stage, new Environment { Name = "Staging", Abbr = EnvironmentType.stage } },
        { EnvironmentType.prod, new Environment { Name = "Production", Abbr = EnvironmentType.prod } },
    };

    public static Environment[] All()
    {
        return _environments.Select(x => x.Value).ToArray();
    }
}
