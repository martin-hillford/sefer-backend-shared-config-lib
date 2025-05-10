namespace Sefer.Backend.SharedConfig.Lib.Providers;

public sealed class FileConfigurationProvider(string filename)  : ConfigurationProvider
{
    public override void Load()
    {
        // Simply try to read the file as text file. if it crashes, it crashes
        var path = filename.Replace("file://", "");
        var content = File.ReadAllText(path);

        // And load all the configuration
        var json = JsonNode.Parse(content);
        Data = json.ToDictionary(":");

        // Also add the environment to the config
        var environment = EnvVar.GetEnvironmentName();
        Data.Add("Environment", environment);
    }
}