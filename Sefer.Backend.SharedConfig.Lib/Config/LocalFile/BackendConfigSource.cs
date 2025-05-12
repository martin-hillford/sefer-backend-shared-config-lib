namespace Sefer.Backend.SharedConfig.Lib.Config.LocalFile;

public class BackendConfigSource(string filename) : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder) => new BackendConfigProvider(filename);
}