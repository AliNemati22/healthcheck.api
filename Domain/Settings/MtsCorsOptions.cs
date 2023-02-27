using Microsoft.AspNetCore.Cors.Infrastructure;

public class MtsCorsOptions
{
    public const string DefaultSectionName = nameof(MtsCorsOptions);
    public string[] AllowedOrigins { get; set; }
}