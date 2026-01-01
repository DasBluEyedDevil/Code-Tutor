void LogMessages(params IEnumerable<string> messages)
{
    foreach (var msg in messages)
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {msg}");
}

// Test inline items
LogMessages("Starting", "Processing", "Done");

// Test collection expression
LogMessages(["Error occurred", "Retrying..."]);

// Test existing collection
var logs = new List<string> { "Init", "Load", "Complete" };
LogMessages(logs);