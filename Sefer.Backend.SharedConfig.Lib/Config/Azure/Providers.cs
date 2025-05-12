namespace Sefer.Backend.SharedConfig.Lib.Config.Azure;

internal static class Providers
{
    internal static ConfigProvider.Providers Get(string blobStorageUri)
    {
        return new ConfigProvider.Providers
        {
            BackendConfigProvider = new BackendConfigProvider(blobStorageUri),
            ConfigurationSource = new BackendConfigSource(blobStorageUri),
            FrontendConfigProvider = new FrontendConfigProvider(blobStorageUri)
        };
    }
}