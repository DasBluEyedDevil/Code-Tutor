using System.Text.Json;
using System.Text.Json.Serialization;

// AOT-compatible configuration class
public class ServerSettings
{
    public int Port { get; set; }
    public string Host { get; set; } = "";
    public bool EnableSsl { get; set; }
    public List<string> AllowedOrigins { get; set; } = new();
}

// Source-generated JSON context for AOT compatibility
[JsonSerializable(typeof(ServerSettings))]
[JsonSerializable(typeof(List<ServerSettings>))]
[JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SettingsJsonContext : JsonSerializerContext { }

// Main code
Console.WriteLine("=== AOT-Ready Configuration System ===");

// Create sample settings
var settings = new ServerSettings
{
    Port = 8080,
    Host = "api.example.com",
    EnableSsl = true,
    AllowedOrigins = new List<string>
    {
        "https://app.example.com",
        "https://admin.example.com"
    }
};

Console.WriteLine("\nOriginal settings:");
Console.WriteLine($"  Host: {settings.Host}:{settings.Port}");
Console.WriteLine($"  SSL: {settings.EnableSsl}");
Console.WriteLine($"  Origins: {settings.AllowedOrigins.Count}");

// Serialize using source-generated context (AOT-safe!)
var json = JsonSerializer.Serialize(settings, SettingsJsonContext.Default.ServerSettings);

Console.WriteLine("\nSerialized JSON:");
Console.WriteLine(json);

// Deserialize back
var loaded = JsonSerializer.Deserialize(json, SettingsJsonContext.Default.ServerSettings);

Console.WriteLine("\nDeserialized and verified:");
Console.WriteLine($"  Port matches: {loaded?.Port == settings.Port}");
Console.WriteLine($"  Host matches: {loaded?.Host == settings.Host}");
Console.WriteLine($"  SSL matches: {loaded?.EnableSsl == settings.EnableSsl}");

// Print .csproj settings
Console.WriteLine("\n=== Required .csproj Settings ===");
Console.WriteLine("<PublishAot>true</PublishAot>");
Console.WriteLine("<TrimMode>full</TrimMode>");
Console.WriteLine("<InvariantGlobalization>true</InvariantGlobalization>");
Console.WriteLine("<EnableAotAnalyzer>true</EnableAotAnalyzer>");