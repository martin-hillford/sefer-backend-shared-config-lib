namespace Sefer.Backend.SharedConfig.Lib.Services;

/// <summary>
/// The NetworkProviders deals with retrieving all the information related to sites and regions.
/// </summary>
public class NetworkProvider(IConfiguration configuration) : INetworkProvider
{
    private static Dictionary<string, Site> _sites;

    private static Dictionary<string, Region> _regions;

    private static Dictionary<string, Models.Environment> _environments;

    private static Dictionary<string, string> _admins;

    public async Task<ISite> GetSiteAsync(string hostname)
    {
        if (_sites == null) await EnsureData();
        if (_sites == null) return null;
        var contains = _sites.TryGetValue(hostname, out var site);
        return contains ? site.Clone() : null;
    }

    public async Task<List<IRegion>> GetRegionsAsync()
    {
        if (_regions == null) await EnsureData();
        return _regions?.Values.Select(r => r.Clone()).Cast<IRegion>().ToList();
    }

    public async Task<List<ISite>> GetSitesAsync()
    {
        if (_sites == null) await EnsureData();
        return _sites?.Values.Select(r => r.Clone()).Cast<ISite>().ToList();
    }

    public async Task<IRegion> GetRegionAsync(string regionId)
    {
        if (_regions == null) await EnsureData();
        if (_regions == null) return null;
        var contains = _regions.TryGetValue(regionId, out var region);
        return contains ? region.Clone() : null;
    }

    public async Task<IEnvironment> GetEnvironmentAsync(string name)
    {
        if (_environments == null) await EnsureData();
        if (_environments == null) return null;
        var contains = _environments.TryGetValue(name, out var environment);
        return contains ? environment : null;
    }

    public async Task<string> GetEnvironmentNameForAdminAsync(string hostname)
    {
        if (_admins == null) await EnsureData();
        return _admins?.GetValueOrDefault(hostname);
    }

    public async Task<string> GetAdminConfigAsync(string environment)
    {
        try
        {
            var client = GetBlobContainerClient();
            var configFile = $"/admin/{environment}.json";
            var content = (await client.DownloadContentAsync(configFile))?.Trim();
            if (string.IsNullOrEmpty(content)) return null;
            return content;
        }
        catch (Exception) { return null; }
    }

    public void PurgeCache()
    {
        _sites = null;
        _regions = null;
        _environments = null;
    }

    private async Task EnsureData()
    {
        // 1. Connect with the azure blob storage to retrieve all the data
        var containerClient = GetBlobContainerClient();

        // 2. Read the index.json file that contains the file information
        var client = containerClient.GetBlobClient("index.json");
        var index = await client.DownloadJsonAsync<Models.Index>();

        // 3. Store the admins
        _admins = index.Admins.ToDictionary(a => a.Host, a => a.Environment);

        // 4. Given the index read all the environments settings
        _environments = await containerClient
            .DownloadCollectionAsync<Models.Environment>(index.Environments, "environments")
            .ToDictionaryAsync(s => s.EnvironmentName);

        // 5. Given the index read all the region settings
        _regions = await containerClient
            .DownloadCollectionAsync<Region>(index.Regions, "regions")
            .ToDictionaryAsync(s => s.Id);

        // 6. Given the index read all the sites
        _sites = await containerClient
            .DownloadCollectionAsync<Site>(index.Sites, "sites")
            .ToDictionaryAsync(s => s.Hostname);
    }

    private BlobContainerClient GetBlobContainerClient()
    {
        var sasUrl =
            EnvVar.GetEnvironmentVariable("CONFIG_STORAGE") ??
            configuration.GetValue<string>("ConfigStorage");
        return new BlobContainerClient(new Uri(sasUrl));
    }
}