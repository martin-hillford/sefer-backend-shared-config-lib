namespace Sefer.Backend.SharedConfig.Lib.Config.Azure;

internal sealed class BackendConfigSource(string blobStorageUri) : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder) =>
        new BackendConfigProvider(blobStorageUri);
}