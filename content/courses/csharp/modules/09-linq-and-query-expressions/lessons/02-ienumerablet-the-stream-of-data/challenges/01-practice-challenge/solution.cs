using System;
using System.Collections.Generic;
using System.Linq;

List<string> fruits = new List<string> { "apple", "banana", "apricot", "blueberry", "avocado" };

IEnumerable<string> startsWithA = fruits.Where(f => f.StartsWith("a"));
Console.WriteLine("Query created (deferred execution)");

fruits.Add("cherry");
Console.WriteLine("Added 'cherry' to list");

Console.WriteLine("\nFruits starting with 'a':");
foreach (string fruit in startsWithA)
{
    Console.WriteLine("- " + fruit);
}

List<string> longFruits = fruits.Where(f => f.Length > 6).ToList();
Console.WriteLine("\nLong fruits (>6 chars): " + longFruits.Count);
foreach (string fruit in longFruits)
{
    Console.WriteLine("- " + fruit);
}