// C# 12 fallback: Uses params string[] instead of params IEnumerable<string>
// Demonstrates the same logging concept with C# 12 compatible params

void LogMessages(params string[] messages)
{
    foreach (var msg in messages)
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {msg}");
}

// Test inline items (works the same way)
LogMessages("Starting", "Processing", "Done");

// Test with explicit array (C# 12 compatible)
LogMessages(new[] { "Error occurred", "Retrying..." });

// Test existing collection (must convert to array for params string[])
var logs = new List<string> { "Init", "Load", "Complete" };
LogMessages(logs.ToArray());
