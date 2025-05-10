// Note, this class is used to parse a json node and this not initiated in compile time.
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
namespace Sefer.Backend.SharedConfig.Lib.Models;

internal class Site : ISite
{
    public string Hostname { get; set; }

    public string SiteUrl => $"https://{Hostname}";

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SiteType Type { get; set; } = SiteType.Redirect;

    public string Destination { get; set; }

    public string RegionId { get; set; }

    public string Name { get; set; }

    public string Alt { get; set; }

    public string ImageSuffix { get; set; }
    
    public string Brand { get; set; }

    public string SupportEmail { get; set; }

    public Socials SocialMedia { get; set; }

    public string StaticContentUrl { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Mode? Mode { get; set; } = null;

    public string SendEmail { get; set; }

    public string Homepage { get; set; } = "default";

    public string Language { get; set; } = "nl";

    public bool Enabled { get; set; } = true;

    public string Environment { get; set; }

    ISocials ISite.SocialMedia => SocialMedia;

    public Site Clone()
    {
        var clone = (Site)MemberwiseClone();
        clone.SocialMedia = SocialMedia?.Clone();
        return clone;
    }
}