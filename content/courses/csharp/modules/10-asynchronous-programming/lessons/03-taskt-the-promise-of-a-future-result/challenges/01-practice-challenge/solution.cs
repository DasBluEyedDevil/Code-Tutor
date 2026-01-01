using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

async Task<int> DownloadFileAsync(string filename, int sizeKB)
{
    Console.WriteLine("Downloading " + filename + " (" + sizeKB + "KB)...");
    await Task.Delay(sizeKB * 10);
    Console.WriteLine(filename + " complete!");
    return sizeKB;
}

Console.WriteLine("Starting downloads...");

List<Task<int>> downloads = new List<Task<int>>();

downloads.Add(DownloadFileAsync("file1.zip", 50));
downloads.Add(DownloadFileAsync("file2.zip", 150));
downloads.Add(DownloadFileAsync("file3.zip", 100));
downloads.Add(DownloadFileAsync("file4.zip", 200));

int[] sizes = await Task.WhenAll(downloads);

int totalKB = sizes.Sum();
double totalMB = totalKB / 1024.0;

Console.WriteLine("\nAll downloads complete!");
Console.WriteLine("Total downloaded: " + totalKB + "KB (" + totalMB.ToString("F2") + "MB)");