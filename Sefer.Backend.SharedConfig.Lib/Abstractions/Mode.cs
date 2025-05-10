namespace Sefer.Backend.SharedConfig.Lib.Abstractions;

/// <summary>
/// Defines to possible modes for a site.
/// Web: normal default mode
/// App: the website is
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Mode { Web, App }