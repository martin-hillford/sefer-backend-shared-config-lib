namespace Sefer.Backend.SharedConfig.Lib.Models;

internal sealed class FrontendConfig
{
    public readonly Dictionary<string, Site> Sites;

    public readonly Dictionary<string, Environment> Environments;

    public readonly Dictionary<string, Region> Regions;

    public readonly Dictionary<string, string> Admins;

    internal FrontendConfig(List<Admin> admins, List<Environment> environments, List<Region> regions, List<Site> sites)
    {
        Sites = sites.ToDictionary(s => s.Hostname);
        Environments = environments.ToDictionary(s => s.EnvironmentName);
        Regions = regions.ToDictionary(s => s.Id);
        Admins = admins.ToDictionary(a => a.Host, a => a.Environment);
    }
}