public static class Environments
{
    public static Environment[] All() => Settings.Current.Environments;

    public static Environment Find(string environment) => All().FirstOrDefault(x => x.IsMatch(environment));
}
