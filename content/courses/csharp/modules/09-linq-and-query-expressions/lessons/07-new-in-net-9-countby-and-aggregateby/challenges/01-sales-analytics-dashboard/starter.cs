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

// TODO: Count sales per region using CountBy
Console.WriteLine("Sales count per region:");

// TODO: Sum sales amount per region using AggregateBy
Console.WriteLine("\nTotal sales per region:");