namespace Sefer.Backend.SharedConfig.Lib.Abstractions;

internal interface IFrontendConfigProvider
{
    internal Task<FrontendConfig> GetFrontendConfig();
}