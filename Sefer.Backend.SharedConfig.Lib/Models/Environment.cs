// Note, this class is used to parse a json node and this not initiated in compile time.
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Sefer.Backend.SharedConfig.Lib.Models;

/// <summary>
/// This class represents some basic settings for the network to run
/// </summary>
internal class Environment : IEnvironment
{
    /// <summary>
    /// The name of the environment
    /// </summary>
    public string EnvironmentName { get; set; }

    /// <summary>
    /// The api url to be used
    /// </summary>
    public string Api { get; set; }

    /// <summary>
    /// A shared api key to be used
    /// </summary>
    public string SharedKey { get; set; }

    /// <summary>
    /// This method clones the environment in a new object
    /// </summary>
    public Environment Clone() => (Environment)MemberwiseClone();
}