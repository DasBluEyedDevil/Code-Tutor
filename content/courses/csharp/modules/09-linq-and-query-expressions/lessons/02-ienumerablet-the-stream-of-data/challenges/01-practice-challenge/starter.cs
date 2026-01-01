using System;
using System.Collections.Generic;
using System.Linq;

List<string> fruits = new List<string> { "apple", "banana", "apricot", "blueberry", "avocado" };

// Query 1: starts with 'a' (deferred)
// TODO: Add your .Where() condition for fruits starting with 'a'
IEnumerable<string> startsWithA = fruits.Where(f => /* condition */);
Console.WriteLine("Query created");

// Add item AFTER query creation
fruits.Add("cherry");

// Iterate query 1
foreach (string fruit in startsWithA)
{
    Console.WriteLine("Starts with A: " + fruit);
}

// Query 2: longer than 6 characters (materialized)
// TODO: Add your .Where() condition for fruits longer than 6 characters
List<string> longFruits = fruits.Where(f => /* condition */).ToList();
Console.WriteLine("Long fruits count: " + longFruits.Count);