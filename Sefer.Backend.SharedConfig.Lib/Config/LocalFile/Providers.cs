namespace Sefer.Backend.SharedConfig.Lib.Config.LocalFile;

internal static class Providers
{
    internal static ConfigProvider.Providers Get(string folder)
    {
        return new ConfigProvider.Providers
        {
            BackendConfigProvider = new BackendConfigProvider(folder),
            ConfigurationSource = new BackendConfigSource(folder),
            FrontendConfigProvider = new FrontendConfigProvider(folder)
        };
    }
}