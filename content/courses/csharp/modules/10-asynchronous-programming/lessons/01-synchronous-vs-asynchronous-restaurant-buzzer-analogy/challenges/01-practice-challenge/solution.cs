using System;
using System.Threading.Tasks;

async Task BrewCoffeeAsync(string type)
{
    Console.WriteLine("Brewing " + type + " coffee...");
    await Task.Delay(2000);
    Console.WriteLine(type + " coffee ready!");
}

async Task ToastBreadAsync()
{
    Console.WriteLine("Toasting bread...");
    await Task.Delay(1000);
    Console.WriteLine("Toast ready!");
}

Console.WriteLine("Starting breakfast...");

Task coffeeTask = BrewCoffeeAsync("Espresso");
Task toastTask = ToastBreadAsync();

Console.WriteLine("Doing other tasks...");

await Task.WhenAll(coffeeTask, toastTask);

Console.WriteLine("Breakfast ready!");