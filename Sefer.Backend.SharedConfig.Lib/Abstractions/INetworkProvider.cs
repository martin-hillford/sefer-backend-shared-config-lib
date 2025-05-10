namespace Sefer.Backend.SharedConfig.Lib.Abstractions;

/// <summary>
/// The NetworkProvider deals with retrieving all the information related to sites and regions.
/// </summary>
public interface INetworkProvider
{
    /// <summary>
    /// Returns all the available sites within the network
    /// </summary>
    public Task<List<ISite>> GetSitesAsync();

    /// <summary>
    /// Returns the site given a hostname
    /// </summary>
    public Task<ISite> GetSiteAsync(string hostname);

    /// <summary>
    /// Returns all the available region within the network
    /// </summary>
    public Task<List<IRegion>> GetRegionsAsync();

    /// <summary>
    /// Returns a region given its id. When no region exists
    /// for the given id null is returned
    /// </summary>
    public Task<IRegion> GetRegionAsync(string regionId);

    /// <summary>
    /// This method returns show common information on the settings for
    /// </summary>
    /// <param name="name">The name of the environment</param>
    public Task<IEnvironment> GetEnvironmentAsync(string name);

    /// <summary>
    /// Purge the settings cache
    /// </summary>
    public void PurgeCache();

    /// <summary>
    /// Get the admin config (front-end) for a given environment
    /// </summary>
    public Task<string> GetAdminConfigAsync(string environment);

    /// <summary>
    /// Returns the admin environment for the given hostname.
    /// If no environment can be found, null is returned
    /// </summary>
    public Task<string> GetEnvironmentNameForAdminAsync(string hostname);
}