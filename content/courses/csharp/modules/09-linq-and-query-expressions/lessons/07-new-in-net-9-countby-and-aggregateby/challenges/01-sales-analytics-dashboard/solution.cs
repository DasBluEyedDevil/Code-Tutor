using System;
using System.Linq;

var sales = new[]
{
    new { Region = "North", Amount = 500m },
    new { Region = "South", Amount = 300m },
    new { Region = "North", Amount = 200m },
    new { Region = "East", Amount = 400m },
    new { Region = "South", Amount = 600m }
};

// Count sales per region
var countPerRegion = sales.CountBy(s => s.Region);
Console.WriteLine("Sales count per region:");
foreach (var (region, count) in countPerRegion)
    Console.WriteLine($"  {region}: {count} sales");

// Sum amount per region
var totalPerRegion = sales.AggregateBy(
    s => s.Region,
    0m,
    (sum, sale) => sum + sale.Amount);

Console.WriteLine("\nTotal sales per region:");
foreach (var (region, total) in totalPerRegion)
    Console.WriteLine($"  {region}: ${total}");