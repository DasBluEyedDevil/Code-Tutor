using System;
using System.Threading;
using System.Threading.Tasks;

async Task ProcessFilesAsync(int fileCount, CancellationToken cancellationToken, IProgress<int> progress)
{
    for (int i = 0; i < fileCount; i++)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        Console.WriteLine("Processing file " + (i + 1) + "...");
        await Task.Delay(500, cancellationToken);
        
        int percentComplete = (i + 1) * 100 / fileCount;
        progress?.Report(percentComplete);
    }
    Console.WriteLine("All files processed!");
}

CancellationTokenSource cts = new CancellationTokenSource();

var progress = new Progress<int>(percent =>
{
    Console.WriteLine("Progress: " + percent + "%");
});

Task task = ProcessFilesAsync(10, cts.Token, progress);

cts.CancelAfter(3000);

try
{
    await task;
    Console.WriteLine("Processing completed successfully!");
}
catch (OperationCanceledException)
{
    Console.WriteLine("\nProcessing was cancelled!");
}

cts.Dispose();