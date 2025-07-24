namespace SilrithMapper;

public class SilrithMapperService : ISilrithMapper
{
    public TDestination Map<TSource, TDestination>(TSource source, MappingConfig? config = null)
        where TDestination : new()
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        var destination = new TDestination();
        var sourceProps = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var destProps = typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var destProp in destProps)
        {
            var matchingSourceProp = sourceProps.FirstOrDefault(p =>
                p.Name == destProp.Name &&
                p.PropertyType == destProp.PropertyType);

            if (matchingSourceProp != null && destProp.CanWrite)
            {
                var value = matchingSourceProp.GetValue(source);
                destProp.SetValue(destination, value);
            }

            if (config != null)
            {
                var rule = MapRuleRegistry.GetRule<TSource, TDestination>(destProp.Name, config.Context);
                if (rule != null)
                {
                    var value = rule(source);
                    destProp.SetValue(destination, value);
                }
            }
        }

        return destination;
    }
}
