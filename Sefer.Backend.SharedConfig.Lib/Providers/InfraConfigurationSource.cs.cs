namespace Sefer.Backend.SharedConfig.Lib.Providers;

public sealed class InfraConfigurationSource(string blobStorageUri) : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder) =>
        new InfraConfigurationProvider(blobStorageUri);
}