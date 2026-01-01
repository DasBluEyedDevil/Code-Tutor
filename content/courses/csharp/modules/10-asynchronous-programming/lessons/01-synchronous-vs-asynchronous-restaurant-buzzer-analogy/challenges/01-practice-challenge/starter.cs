using System;
using System.Threading.Tasks;

async Task BrewCoffeeAsync(string type)
{
    // Print starting message
    // await Task.Delay(2000)
    // Print ready message
}

async Task ToastBreadAsync()
{
    // Print starting message
    // await Task.Delay(1000)
    // Print ready message
}

// Main async code
Console.WriteLine("Starting breakfast...");

// Start both tasks
Task coffeeTask = BrewCoffeeAsync("Espresso");
Task toastTask = ToastBreadAsync();

Console.WriteLine("Doing other tasks...");

// Wait for both
await Task.WhenAll(coffeeTask, toastTask);

Console.WriteLine("Breakfast ready!");