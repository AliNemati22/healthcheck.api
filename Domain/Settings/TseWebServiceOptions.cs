namespace healthcheck.api.Domain.Settings;

public class TseWebServiceOptions
{
    public const string ConfigurationSectionName = "TseWebServiceOptions";

    public string? UserName { get; set; }
    public string? Password { get; set; }
}