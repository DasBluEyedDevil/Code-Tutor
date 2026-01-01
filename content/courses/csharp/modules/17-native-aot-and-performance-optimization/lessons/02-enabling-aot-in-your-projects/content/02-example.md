---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// ===== COMPLETE AOT PROJECT FILE =====
// MyApp.csproj

// <Project Sdk="Microsoft.NET.Sdk">
//   <PropertyGroup>
//     <OutputType>Exe</OutputType>
//     <TargetFramework>net9.0</TargetFramework>
//     <Nullable>enable</Nullable>
//     <ImplicitUsings>enable</ImplicitUsings>
//     
//     <!-- NATIVE AOT SETTINGS -->
//     <PublishAot>true</PublishAot>
//     
//     <!-- TRIMMING (removes unused code) -->
//     <TrimMode>full</TrimMode>
//     <IsTrimmable>true</IsTrimmable>
//     
//     <!-- SIZE OPTIMIZATION -->
//     <InvariantGlobalization>true</InvariantGlobalization>
//     <OptimizationPreference>Size</OptimizationPreference>
//     
//     <!-- SINGLE FILE OUTPUT -->
//     <PublishSingleFile>true</PublishSingleFile>
//     <SelfContained>true</SelfContained>
//     
//     <!-- AOT WARNINGS AS ERRORS (recommended!) -->
//     <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
//     <EnableTrimAnalyzer>true</EnableTrimAnalyzer>
//     <EnableAotAnalyzer>true</EnableAotAnalyzer>
//   </PropertyGroup>
// </Project>

// ===== AOT-COMPATIBLE CODE =====
using System.Text.Json;
using System.Text.Json.Serialization;

// Source generator for JSON (AOT-compatible!)
[JsonSerializable(typeof(Config))]
[JsonSerializable(typeof(List<string>))]
internal partial class AppJsonContext : JsonSerializerContext { }

public class Config
{
    public string AppName { get; set; } = "";
    public int MaxConnections { get; set; }
    public List<string> AllowedHosts { get; set; } = new();
}

class Program
{
    static void Main()
    {
        Console.WriteLine("AOT-Enabled Application");
        
        // Create configuration
        var config = new Config
        {
            AppName = "My AOT App",
            MaxConnections = 100,
            AllowedHosts = new List<string> { "localhost", "api.example.com" }
        };
        
        // AOT-compatible JSON serialization (using source generator!)
        var json = JsonSerializer.Serialize(config, AppJsonContext.Default.Config);
        Console.WriteLine($"Serialized: {json}");
        
        // Deserialize back
        var loaded = JsonSerializer.Deserialize(json, AppJsonContext.Default.Config);
        Console.WriteLine($"Loaded: {loaded?.AppName}");
        
        // LINQ works in AOT!
        var hosts = config.AllowedHosts.Where(h => h.Contains(".")).ToList();
        Console.WriteLine($"Filtered hosts: {string.Join(", ", hosts)}");
        
        // Collections work fine
        var numbers = Enumerable.Range(1, 10).Select(n => n * 2).ToArray();
        Console.WriteLine($"Doubled: {string.Join(", ", numbers)}");
    }
}

// ===== PUBLISH COMMANDS =====
// Windows:
//   dotnet publish -c Release -r win-x64
//
// Linux:
//   dotnet publish -c Release -r linux-x64
//
// macOS (Apple Silicon):
//   dotnet publish -c Release -r osx-arm64
//
// Output: bin/Release/net9.0/[rid]/publish/MyApp.exe

// ===== VERIFY AOT OUTPUT =====
// - Single executable file
// - No .dll files
// - No runtimeconfig.json needed
// - Just copy and run!

Console.WriteLine("\nPublish with: dotnet publish -c Release -r win-x64");
Console.WriteLine("Output will be a single native executable!");
```
