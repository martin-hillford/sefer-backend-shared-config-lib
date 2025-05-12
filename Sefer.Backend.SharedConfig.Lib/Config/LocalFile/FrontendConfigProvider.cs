namespace Sefer.Backend.SharedConfig.Lib.Config.LocalFile;

internal class FrontendConfigProvider(string folder) : IFrontendConfigProvider
{
    public async Task<FrontendConfig> GetFrontendConfig()
    {
        // 1. Read the index.json file that contains the file information
        var index = await ReadJsonFile<Models.Index>($"{folder}/index.json");
        
        // 4. Given the index read all the environments settings
        var environments = await ReadJsonFiles<Models.Environment>($"{folder}/environments"); 

        // 5. Given the index read all the region settings
        var regions = await ReadJsonFiles<Region>($"{folder}/regions");

        // 6. Given the index read all the sites
        var sites = await ReadJsonFiles<Site>($"{folder}/sites");
        
        return new FrontendConfig(index.Admins, environments.ToList(), regions.ToList(), sites.ToList());
    }
    
    private static async Task<T> ReadJsonFile<T>(string filePath)
    {
        var content = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<T>(content);
    }

    private static async Task<List<T>> ReadJsonFiles<T>(string filePath)
    {
        var folder = new DirectoryInfo(filePath);
        var files = folder.GetFiles().Where(x => x.Name.EndsWith(".json")).ToList();
        var tasks = files.Select(file => ReadJsonFile<T>(file.FullName)).ToList();
        var result =  await Task.WhenAll(tasks);
        return result.ToList();
    }
}