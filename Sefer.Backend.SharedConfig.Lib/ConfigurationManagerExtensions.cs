namespace Sefer.Backend.SharedConfig.Lib;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public static class ConfigurationManagerExtensions
{
    public static IServiceCollection AddSeferInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<INetworkProvider, NetworkProvider>();
        return services;
    }

    public static INetworkProvider GetNetworkProvider(this IServiceProvider serviceProvider)
    {
        return serviceProvider.GetService<INetworkProvider>()!;
    }

    public static IHostApplicationBuilder WithSharedConfig(this IHostApplicationBuilder builder)
    {
        builder.Configuration.AddInfrastructureConfiguration();
        builder.Services.AddSeferInfrastructure();
        return builder;
    }
    
    public static IConfigurationManager AddInfrastructureConfiguration(this IConfigurationManager manager)
    {
        var storageUri = manager.GetConfigStore();
        if (storageUri.StartsWith("file://"))
        {
            manager.Add(new Providers.FileConfigurationSource(storageUri));
        }
        else
        {
            manager.Add(new InfraConfigurationSource(storageUri));
        }
        return manager;
    }
    
    public static string GetConfigStore(this IConfigurationManager manager)
    {
        return
            EnvVar.GetEnvironmentVariable("CONFIG_STORAGE") ??
            manager.GetValue<string>("ConfigStorage");
    }
}