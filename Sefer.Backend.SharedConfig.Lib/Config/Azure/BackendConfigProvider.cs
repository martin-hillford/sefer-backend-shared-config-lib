namespace Sefer.Backend.SharedConfig.Lib.Config.Azure;

internal sealed class BackendConfigProvider(string blobStorageUri) : ConfigurationProvider, IBackendConfigProvider
{
    public override void Load()
    {
        // Get the endpoint where to load the settings
        var environment = EnvVar.GetEnvironmentName();
        var content = GetBackendConfig(environment);

        // And load all the configuration
        var json = JsonNode.Parse(content);
        Data = json.ToDictionary(":");

        // Also add the environment to the config
        Data.Add("Environment", environment);
    }

    public string GetBackendConfig(string environment)
    {
        var client = new BlobContainerClient(new Uri(blobStorageUri));
        var blobClient = client.GetBlobClient($"environments/{environment}.json");
        var response = blobClient.DownloadContent();
        return response.Value.Content.ToString();
    }
}