using System.Diagnostics;

// Record types are fully AOT-compatible!
public record AppInfo(string Name, string Version, DateTime StartTime);

// Track startup time - Stopwatch is AOT-safe
var stopwatch = Stopwatch.StartNew();

Console.WriteLine("=== Native AOT Demo ===");

// Create instance - no reflection needed!
var appInfo = new AppInfo(
    Name: "AOT Demo App",
    Version: "1.0.0",
    StartTime: DateTime.Now
);

// String interpolation is AOT-compatible
Console.WriteLine($"App: {appInfo.Name}");
Console.WriteLine($"Version: {appInfo.Version}");
Console.WriteLine($"Started: {appInfo.StartTime:HH:mm:ss}");

// Simulate some work
Thread.Sleep(100);

// Stop and report elapsed time
stopwatch.Stop();
Console.WriteLine($"\nElapsed: {stopwatch.ElapsedMilliseconds}ms");

// Environment properties are AOT-safe
Console.WriteLine($"64-bit process: {Environment.Is64BitProcess}");

// Memory info - no reflection required
var memoryMB = Environment.WorkingSet / (1024.0 * 1024.0);
Console.WriteLine($"Working set: {memoryMB:F2} MB");

// Process info is AOT-compatible
Console.WriteLine($"Process ID: {Environment.ProcessId}");

Console.WriteLine("\nAll operations are AOT-compatible!");
Console.WriteLine("Publish with: dotnet publish -c Release -r win-x64");