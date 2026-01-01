using System;
using System.Threading;
using System.Threading.Tasks;

async Task ProcessFilesAsync(int fileCount, CancellationToken cancellationToken, IProgress<int> progress)
{
    for (int i = 0; i < fileCount; i++)
    {
        // Check cancellation
        
        // Process file
        Console.WriteLine("Processing file " + (i + 1) + "...");
        await Task.Delay(500, cancellationToken);
        
        // Report progress
        int percentComplete = (i + 1) * 100 / fileCount;
        progress?.Report(percentComplete);
    }
    Console.WriteLine("All files processed!");
}

// Create cancellation source
CancellationTokenSource cts = new CancellationTokenSource();

// Create progress reporter
var progress = new Progress<int>(percent =>
{
    Console.WriteLine("Progress: " + percent + "%");
});

// Start processing
Task task = ProcessFilesAsync(10, cts.Token, progress);

// Cancel after 3 seconds
cts.CancelAfter(3000);

try
{
    await task;
    Console.WriteLine("Processing completed successfully!");
}
catch (OperationCanceledException)
{
    Console.WriteLine("Processing was cancelled!");
}