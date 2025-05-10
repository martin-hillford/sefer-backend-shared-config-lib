namespace Sefer.Backend.SharedConfig.Lib;

public static class EnvVar
{
    public static string GetEnvironmentVariable(string name)
    {
        try
        {
            var value = System.Environment.GetEnvironmentVariable(name)?.Trim();
            return string.IsNullOrEmpty(value) ? null : value;
        }
        catch (Exception) { return null; }
    }

    public static string GetEnvironmentName()
    {
        var environment =
            EnvVar.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ??
            EnvVar.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ??
            "development";

        return environment.ToLower();
    }

    public static bool IsAcceptanceEnv() => GetEnvironmentName() == "acceptance";

    public static bool IsProductionEnv() => GetEnvironmentName() == "production";

    public static bool IsDevelopmentEnv() => GetEnvironmentName() == "development";
}