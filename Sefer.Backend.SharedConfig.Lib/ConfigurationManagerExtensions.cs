namespace Sefer.Backend.SharedConfig.Lib;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public static class ConfigurationManagerExtensions
{
    /// <summary>
    /// Gets the network provider. This provider can return all kinds of info on the
    /// connected sites and regions
    /// </summary>
    public static INetworkProvider GetNetworkProvider(this IServiceProvider serviceProvider)
    {
        return serviceProvider.GetService<INetworkProvider>()!;
    }

    /// <summary>
    /// Add the shared config to the builder
    /// </summary>
    public static IHostApplicationBuilder WithSharedConfig(this IHostApplicationBuilder builder)
        => builder.AddSharedConfig();
    
    /// <summary>
    /// Add the shared config to the builder
    /// </summary>
    /// <remarks>
    /// Although WebApplicationBuilder implements IHostApplicationBuilder this extension method is helpful because then
    /// the chaining is easier because WebApplicationBuilder contains the Build() method.
    /// </remarks>
    public static WebApplicationBuilder WithSharedConfig(this WebApplicationBuilder builder)
    {
        builder.AddSharedConfig();
        return builder;
    }
    
    // This method is actually adding the providers to the 
    private static IHostApplicationBuilder AddSharedConfig(this IHostApplicationBuilder builder)
    {
        var provider = new ConfigProvider(builder);
        var providers = provider.GetProviders();
        if (providers == null) return builder;

        builder.Configuration.Add(providers.ConfigurationSource);
        var networkProvider = new NetworkProvider(providers.FrontendConfigProvider, providers.BackendConfigProvider);
        builder.Services.AddSingleton<INetworkProvider>(networkProvider);
        return builder;
    }
}