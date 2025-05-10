namespace Sefer.Backend.SharedConfig.Lib.Abstractions;

public interface IEnvironment
{
    /// <summary>
    /// Holds the name of the environment (e.g. acceptance or production)
    /// </summary>
    public string EnvironmentName { get; }

    /// <summary>
    /// The shared api key that can be used to interact between apis
    /// </summary>
    public string SharedKey { get; }

    /// <summary>
    /// The uri of the general purposes api
    /// </summary>
    public string Api { get; }
}