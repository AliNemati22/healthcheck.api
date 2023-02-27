namespace healthcheck.api.Domain.Settings;

public class WebServicesOptions
{
    public const string ConfigurationSectionName = "WebServicesOptions";
    public string DefaultRequestType { get; set; }
    public WebService[] WebServices { get; set; }
}

// ReSharper disable once ClassNeverInstantiated.Global
public class WebService
{
    public string Name { get; set; }
    public string? RequestType { get; set; }
    public IConfigurationSection Options { get; set; }
}