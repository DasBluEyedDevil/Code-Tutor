using System.Diagnostics;

// TODO: Define AppInfo record with Name, Version, StartTime

// Track startup time
var stopwatch = Stopwatch.StartNew();

Console.WriteLine("=== Native AOT Demo ===");

// TODO: Create AppInfo instance

// TODO: Print app name and version

// Simulate some work
Thread.Sleep(100);

// TODO: Stop the stopwatch and print elapsed time

// TODO: Print if process is 64-bit (Environment.Is64BitProcess)

// TODO: Print working set memory in MB
// Hint: Environment.WorkingSet / (1024 * 1024)

Console.WriteLine("\nAll operations are AOT-compatible!");