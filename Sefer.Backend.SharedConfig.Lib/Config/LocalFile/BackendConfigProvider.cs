namespace Sefer.Backend.SharedConfig.Lib.Config.LocalFile;

public sealed class BackendConfigProvider(string folder)  : ConfigurationProvider, IBackendConfigProvider
{
    public override void Load()
    {
        // Simply try to read the file as text file. if it crashes, it crashes
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
        // Simply try to read the file as text file. if it crashes, it crashes
        var path = $"{folder}/environments/{environment}.json";
        return File.ReadAllText(path);
    }
}