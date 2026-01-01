using System;
using System.Threading.Tasks;

async Task<int> LoadDataAsync()
{
    Console.WriteLine("Loading data...");
    await Task.Delay(1000);
    Console.WriteLine("Data loaded!");
    return 42;
}

async Task<int> ProcessDataAsync(int input)
{
    Console.WriteLine("Processing data...");
    await Task.Delay(1500);
    Console.WriteLine("Processing complete!");
    return input * 2;
}

async Task<string> SaveResultAsync(int result)
{
    Console.WriteLine("Saving result...");
    await Task.Delay(800);
    Console.WriteLine("Result saved!");
    return "Saved: " + result;
}

async Task RunPipelineAsync()
{
    int data = await LoadDataAsync();
    int processed = await ProcessDataAsync(data);
    string message = await SaveResultAsync(processed);
    Console.WriteLine("Pipeline result: " + message);
}

await RunPipelineAsync();
Console.WriteLine("All done!");