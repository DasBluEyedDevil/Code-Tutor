using System.Text.Json;
using System.Text.Json.Serialization;

// TODO: Create ServerSettings class with:
// - Port (int)
// - Host (string)
// - EnableSsl (bool)
// - AllowedOrigins (List<string>)

// TODO: Create JsonSerializerContext with [JsonSerializable] attributes
// Hint: [JsonSerializable(typeof(ServerSettings))]
//       internal partial class SettingsJsonContext : JsonSerializerContext { }

// Main code
Console.WriteLine("=== AOT-Ready Configuration System ===");

// TODO: Create sample ServerSettings

// TODO: Serialize to JSON using source-generated context
// Hint: JsonSerializer.Serialize(settings, SettingsJsonContext.Default.ServerSettings)

// TODO: Print JSON (use JsonSerializerOptions for indentation)

// TODO: Deserialize and verify

// Print .csproj settings
Console.WriteLine("\n=== Required .csproj Settings ===");
Console.WriteLine("<PublishAot>true</PublishAot>");