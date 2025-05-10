namespace Sefer.Backend.SharedConfig.Lib.Abstractions;

/// <summary>
/// Defines the types of sites
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SiteType { Redirect, Static, Dynamic }