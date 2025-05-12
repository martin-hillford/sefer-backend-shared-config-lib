namespace Sefer.Backend.SharedConfig.Lib.Config;

public class ConfigProvider(IHostApplicationBuilder builder)
{
    internal Providers GetProviders()
    {
        // Get the store and the location. If no location is present, shared config cannot be used.
        var store = GetConfigStore();
        var location = GetConfigFileLocation();
        if (string.IsNullOrEmpty(location)) return null;

        return store?.ToLower() switch
        {
            "azure" => Azure.Providers.Get(location),
            "local-file" => LocalFile.Providers.Get(location),
            _ => null
        };
    }
    
    private string GetConfigStore()
    {
        return
            EnvVar.GetEnvironmentVariable("SHARED_CONFIG_STORE") ??
            builder.Configuration.GetValue<string>("SharedConfigStore");    
    }

    private string GetConfigFileLocation()
    {
        return
            EnvVar.GetEnvironmentVariable("SHARED_CONFIG_LOCATION") ??
            builder.Configuration.GetValue<string>("SharedConfigLocation");
    }

    internal class Providers
    {
        internal IConfigurationSource ConfigurationSource { get; init; }
        
        internal IFrontendConfigProvider FrontendConfigProvider { get; init; }
        
        internal IBackendConfigProvider BackendConfigProvider { get; init; }
    }
}