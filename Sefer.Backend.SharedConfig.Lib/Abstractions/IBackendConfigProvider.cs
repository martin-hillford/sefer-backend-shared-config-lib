namespace Sefer.Backend.SharedConfig.Lib.Abstractions;

public interface IBackendConfigProvider
{
    public string GetBackendConfig(string environment);
}