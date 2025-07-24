namespace SilrithMapper;

public static class MapRuleRegistry
{
    private static readonly ConcurrentDictionary<string, Delegate> _rules = new();

    public static void RegisterRule<TSource, TDestination>(
        string propertyName,
        string context,
        CustomMapRule<TSource> rule)
    {
        var key = GenerateKey<TSource, TDestination>(propertyName, context);
        _rules[key] = rule;
    }

    public static CustomMapRule<TSource>? GetRule<TSource, TDestination>(
        string propertyName,
        string context)
    {
        var key = GenerateKey<TSource, TDestination>(propertyName, context);
        if (_rules.TryGetValue(key, out var rule))
        {
            return rule as CustomMapRule<TSource>;
        }

        return null;
    }

    private static string GenerateKey<TSource, TDestination>(string prop, string context)
        => $"{typeof(TSource).FullName}->{typeof(TDestination).FullName}:{prop}@{context}";
}
