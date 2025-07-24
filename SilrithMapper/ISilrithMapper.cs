namespace SilrithMapper;

public interface ISilrithMapper
{
    TDestination Map<TSource, TDestination>(TSource source, MappingConfig? config = null)
        where TDestination : new();
}
