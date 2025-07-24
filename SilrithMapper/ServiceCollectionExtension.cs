namespace SilrithMapper;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSilrithMapper(this IServiceCollection services)
    {
        services.TryAddSingleton<ISilrithMapper, SilrithMapperService>();
        return services;
    }
}
