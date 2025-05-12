namespace Sefer.Backend.SharedConfig.Lib.Config.Azure;

internal sealed class FrontendConfigProvider(string sasUrl) : IFrontendConfigProvider
{
    public async Task<FrontendConfig> GetFrontendConfig()
    {
        // 1. Connect with the azure blob storage to retrieve all the data
        var containerClient = new BlobContainerClient(new Uri(sasUrl));

        // 2. Read the index.json file that contains the file information
        var client = containerClient.GetBlobClient("index.json");
        var index = await client.DownloadJsonAsync<Models.Index>();
        
        // 4. Given the index read all the environments settings
        var environments = await containerClient
            .DownloadCollectionAsync<Models.Environment>(index.Environments, "environments");

        // 5. Given the index read all the region settings
        var regions = await containerClient
            .DownloadCollectionAsync<Region>(index.Regions, "regions");

        // 6. Given the index read all the sites
        var sites = await containerClient
            .DownloadCollectionAsync<Site>(index.Sites, "sites");

        return new FrontendConfig(index.Admins, environments.ToList(), regions.ToList(), sites.ToList());
    }
}