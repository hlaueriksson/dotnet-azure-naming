public static class Environments
{
    public static readonly Environment[] All = new[]
    {
        new Environment { Name = "Development", Abbr = EnvironmentType.dev },
        new Environment { Name = "Test", Abbr = EnvironmentType.test },
        new Environment { Name = "Staging", Abbr = EnvironmentType.stage },
        new Environment { Name = "Production", Abbr = EnvironmentType.prod },
    };

    public static Environment Find(string environment) => All.FirstOrDefault(x => x.IsMatch(environment));
}
