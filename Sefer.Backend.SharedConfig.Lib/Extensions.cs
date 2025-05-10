using System.Globalization;

namespace Sefer.Backend.SharedConfig.Lib;

public static class Extensions
{
    public static string GetString(this HttpClient httpClient, string requestUri)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var response = httpClient.Send(request).EnsureSuccessStatusCode();
        using var streamReader = new StreamReader(response.Content.ReadAsStream());
        return streamReader.ReadToEnd();
    }

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public static Dictionary<TKey, TValue> Add<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Dictionary<TKey, TValue> merging)
    {
        foreach (var (key, value) in merging)
        {
            dictionary.Add(key, value);
        }
        return dictionary;
    }

    private static string GetNodePath(this JsonNode node, string separator)
    {
        var nodePath = node?.GetPath();
        if (nodePath is null or "$") return string.Empty;
        var path = nodePath.Replace("[", ".").Replace("]", string.Empty);
        if (path.StartsWith("$.")) path = path[2..];
        return path.Replace(".", separator);
    }

    public static Dictionary<string, string> ToDictionary(this JsonNode node, string separator = ".")
    {
        var data = new Dictionary<string, string>();
        if (node == null) return data;

        var path = node.GetNodePath(separator);
        switch (node.GetValueKind())
        {
            case JsonValueKind.Object:
                var jsonObject = node.AsObject();
                foreach (var (_, childNode) in jsonObject)
                {
                    data.Add(ToDictionary(childNode, separator));
                }
                break;
            case JsonValueKind.Array:
                var jsonArray = node.AsArray();
                foreach (var childNode in jsonArray)
                {
                    data.Add(ToDictionary(childNode, separator));
                }
                break;
            case JsonValueKind.String:
                if (path != null) data.Add(path, node.GetValue<string>());
                break;
            case JsonValueKind.Number:
                var number = node.GetValue<double>().ToString(CultureInfo.InvariantCulture);
                if (path != null) data.Add(path, number);
                break;
            case JsonValueKind.True:
                if (path != null) data.Add(path, "true");
                break;
            case JsonValueKind.False:
                if (path != null) data.Add(path, "false");
                break;
            case JsonValueKind.Null:
            case JsonValueKind.Undefined:    
            default:
                if (path != null) data.Add(path, "null");
                break;
        }
        return data;
    }

    public static async Task<T> DownloadJsonAsync<T>(this BlobClient client)
    {
        try
        {
            var response = await client.DownloadContentAsync();
            return response?.Value?.Content == null ? default : response.Value.Content.ToObjectFromJson<T>();
        }
        catch (Exception) { return default; }
    }

    public static async Task<string> DownloadStringAsync(this BlobClient client)
    {
        try
        {
            var response = await client.DownloadContentAsync();
            return response?.Value?.Content?.ToString();
        }
        catch (Exception) { return null; }
    }

    internal static async Task<IEnumerable<T>> DownloadCollectionAsync<T>(this BlobContainerClient client, List<string> items, string folder)
    {
        var tasks = items.Select(item =>
        {
            var blobClient = client.GetBlobClient($"{folder}/{item}.json");
            return blobClient.DownloadJsonAsync<T>();
        });

        var collection = await Task.WhenAll(tasks);
        return collection.Where(c => c != null);
    }

    public static async Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this Task<IEnumerable<TSource>> source, Func<TSource, TKey> keySelector) where TKey : notnull
    {
        var data = await source;
        return data.ToDictionary(keySelector);
    }

    public static string DownloadContent(this BlobContainerClient client, string blobName)
    {
        var blobClient = client.GetBlobClient(blobName);
        var response = blobClient.DownloadContent();
        return response.Value.Content.ToString();
    }

    public static async Task<string> DownloadContentAsync(this BlobContainerClient client, string blobName)
    {
        var blobClient = client.GetBlobClient(blobName);
        var response = await blobClient.DownloadContentAsync();
        return response.Value.Content.ToString();
    }
}