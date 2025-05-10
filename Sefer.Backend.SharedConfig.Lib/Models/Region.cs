// Note, this class is used to parse a json node and this not initiated in compile time.
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Sefer.Backend.SharedConfig.Lib.Models;

internal class Region : IRegion
{
    public string Id { get; set; }

    public string Director { get; set; }

    public string CountryCode { get; set; }

    public bool EnableRewards { get; set; }

    public bool IsDefault { get; set; }

    public Region Clone() => (Region)MemberwiseClone();
}