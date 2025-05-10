namespace Sefer.Backend.SharedConfig.Lib.Abstractions;

public interface IRegion
{
    /// <summary>
    /// The id of the region,
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// The name of the director of the region
    /// (used to sign the certificates)
    /// </summary>
    public string Director { get; }

    /// <summary>
    /// The ISO 3166-1 alpha-2 country code for this region.
    /// </summary>
    public string CountryCode { get; }

    /// <summary>
    /// Holds if for this region the rewards are enabled
    /// </summary>
    public bool EnableRewards { get; }

    /// <summary>
    /// Returns the default region (should be 1)
    /// </summary>
    public bool IsDefault { get; }
}