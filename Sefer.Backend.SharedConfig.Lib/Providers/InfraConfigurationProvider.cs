namespace Sefer.Backend.SharedConfig.Lib.Providers;

public sealed class InfraConfigurationProvider(string blobStorageUri) : ConfigurationProvider
{
    public override void Load()
    {
        // Check if local configuration (traditional appsettings.json) should
        // be used. If that is the case, don't add any data.
        if (UseLocalConfig()) return;

        // Get the endpoint where to load the settings
        var environment = EnvVar.GetEnvironmentName();

        var client = new BlobContainerClient(new Uri(blobStorageUri));
        var blobClient = client.GetBlobClient($"environments/{environment}.json");
        var response = blobClient.DownloadContent();
        var content = response.Value.Content.ToString();

        // And load all the configuration
        var json = JsonNode.Parse(content);
        Data = json.ToDictionary(":");

        // Also add the environment to the config
        Data.Add("Environment", environment);
    }

    private static bool UseLocalConfig()
    {
        var useLocal = EnvVar.GetEnvironmentVariable("LOCAL_CONFIG") ?? "";
        return useLocal == "true";
    }
}