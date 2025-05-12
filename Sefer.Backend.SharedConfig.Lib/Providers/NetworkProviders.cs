namespace Sefer.Backend.SharedConfig.Lib.Providers;

/// <summary>
/// The NetworkProviders deals with retrieving all the information related to sites and regions.
/// </summary>
public class NetworkProvider : INetworkProvider
{
    private static FrontendConfig _config;

    private readonly IFrontendConfigProvider _frontendConfigProvider;
    
    private readonly IBackendConfigProvider _backendConfigProvider;

    internal NetworkProvider(IFrontendConfigProvider frontendConfigProvider, IBackendConfigProvider backendConfigProvider)
    {
        _frontendConfigProvider = frontendConfigProvider;
        _backendConfigProvider = backendConfigProvider;
    }

    /// <summary>
    /// Returns the config of a site given the hostname. If no site is found, null is returned
    /// </summary>
    public async Task<ISite> GetSiteAsync(string hostname)
    {
        var config = await GetConfig();
        if (config == null) return null;
        var contains = config.Sites.TryGetValue(hostname, out var site);
        return contains ? site.Clone() : null;
    }

    public async Task<List<IRegion>> GetRegionsAsync()
    {
        var config = await GetConfig();
        return config.Regions?.Values.Select(r => r.Clone()).Cast<IRegion>().ToList();
    }

    public async Task<List<ISite>> GetSitesAsync()
    {
        var config = await GetConfig();
        return config.Sites?.Values.Select(r => r.Clone()).Cast<ISite>().ToList();
    }

    public async Task<IRegion> GetRegionAsync(string regionId)
    {
        var config = await GetConfig();
        if (config == null) return null;
        var contains = config.Regions.TryGetValue(regionId, out var region);
        return contains ? region.Clone() : null;
    }

    public async Task<IEnvironment> GetEnvironmentAsync(string name)
    {
        var config = await GetConfig();
        if (config == null) return null;
        var contains = config.Environments.TryGetValue(name, out var environment);
        return contains ? environment : null;
    }

    public async Task<string> GetEnvironmentNameForAdminAsync(string hostname)
    {
        var config = await GetConfig();
        return config.Admins?.GetValueOrDefault(hostname);
    }

    public Task<string> GetAdminConfigAsync(string environment)
    {
        try
        {
            var config = _backendConfigProvider.GetBackendConfig(environment);
            return Task.FromResult(config);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public void PurgeCache()
    {
        _config = null;
    }

    private async Task<FrontendConfig> GetConfig()
    {
        if(_config != null) return _config;
        _config = await _frontendConfigProvider.GetFrontendConfig();
        return _config;
    }
}