using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

async Task<int> DownloadFileAsync(string filename, int sizeKB)
{
    // Implement download simulation
}

Console.WriteLine("Starting downloads...");

// Create list of download tasks
List<Task<int>> downloads = new List<Task<int>>();

downloads.Add(DownloadFileAsync("file1.zip", 50));
downloads.Add(DownloadFileAsync("file2.zip", 150));
downloads.Add(DownloadFileAsync("file3.zip", 100));
downloads.Add(DownloadFileAsync("file4.zip", 200));

// Wait for all
int[] sizes = await Task.WhenAll(downloads);

// Calculate total
int totalKB = sizes.Sum();
double totalMB = totalKB / 1024.0;

Console.WriteLine("Total downloaded: " + totalKB + "KB (" + totalMB + "MB)");