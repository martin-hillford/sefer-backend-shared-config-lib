namespace Sefer.Backend.SharedConfig.Lib.Abstractions;

public interface ISite
{
    /// <summary>
    /// The hostname of the site
    /// </summary>
    public string Hostname { get; }

    /// <summary>
    /// The url for the site, This should a fully qualified name starting with
    /// https or and other protocol like capacitor://
    /// </summary>
    public string SiteUrl { get; }

    /// <summary>
    /// The type of the site (redirect, fixed, dynamic)
    /// </summary>
    public SiteType Type { get; }

    /// <summary>
    /// The destination of the site (when this is a redirect site)
    /// </summary>
    public string Destination { get; }

    /// <summary>
    /// The region is this site belongs of (null or empty for a dynamic site)
    /// </summary>
    public string RegionId { get; }

    /// <summary>
    /// The name of this site
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The alt name for the website
    /// </summary>
    public string Alt { get; }

    /// <summary>
    /// Url to the logo that should be displayed
    /// </summary>
    public string ImageSuffix { get; }

    /// <summary>
    /// The brand this site belong to
    /// </summary>
    public string Brand { get; }

    /// <summary>
    /// An e-mail to contact support
    /// </summary>
    public string SupportEmail { get; }

    /// <summary>
    /// The e-mail from which e-mail is send by the website
    /// </summary>
    public string SendEmail { get; }

    /// <summary>
    /// Information on all the social media accounts for this site
    /// </summary>
    public ISocials SocialMedia { get; }

    /// <summary>
    /// The url where all the files can be downloaded (images etc.)
    /// </summary>
    public string StaticContentUrl { get; }

    /// <summary>
    /// This is the mode for the site and will determine is the site will
    /// be displayed in the normal mode or is presented in app mode
    /// </summary>
    public Mode? Mode { get; }

    /// <summary>
    /// Since there are multiple homepage, defines the homepage to be used.
    /// </summary>
    public string Homepage { get; }

    /// <summary>
    /// The language for this site (current only Dutch is supported)
    /// </summary>
    public string Language { get; }

    /// <summary>
    /// Defines if the site enabled
    /// </summary>
    public bool Enabled { get; }

    /// <summary>
    /// The environment this site is running in
    /// </summary>
    public string Environment { get; }
}

public static class SiteExtensions
{
    /// <summary>
    /// Returns the header logo for an ISite given a region.
    /// NB. Do not use RegionId from site since that may not be set,
    /// e.g. when the site is dynamic
    /// </summary>
    public static string GetHeaderLogo(this ISite site, IRegion region)
    {
        return $"{site.StaticContentUrl}/{region.Id}/logos/header{site.ImageSuffix}.png";
    }

    /// <summary>
    /// Returns the diploma/logo for an ISite given a region.
    /// NB. Do not use RegionId from site since that may not be set,
    /// e.g. when the site is dynamic
    /// </summary>
    public static string GetLogoLarge(this ISite site, IRegion region)
    {
        return $"{site.StaticContentUrl}/{region.Id}/pdf/logo{site.ImageSuffix}.png";
    }

    /// <summary>
    /// Returns the header logo for an ISite given a region.
    /// NB. Do not use RegionId from site since that may not be set,
    /// e.g. when the site is dynamic
    /// </summary>
    public static string GetLogoSvg(this ISite site, IRegion region)
    {
        return $"{site.StaticContentUrl}/{region.Id}/logos/header{site.ImageSuffix}.svg";
    }

    /// <summary>
    /// Returns is the region contains the site
    /// </summary>
    public static bool ContainsSite(this IRegion region, ISite site)
    {
        if (site == null || region == null) return false;
        return site.Type switch
        {
            SiteType.Dynamic => true,
            SiteType.Static => site.RegionId == region.Id,
            _ => false
        };
    }
}