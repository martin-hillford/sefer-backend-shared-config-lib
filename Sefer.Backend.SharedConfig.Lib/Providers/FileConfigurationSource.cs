namespace Sefer.Backend.SharedConfig.Lib.Providers;

public class FileConfigurationSource(string filename) : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder) => new FileConfigurationProvider(filename);
}