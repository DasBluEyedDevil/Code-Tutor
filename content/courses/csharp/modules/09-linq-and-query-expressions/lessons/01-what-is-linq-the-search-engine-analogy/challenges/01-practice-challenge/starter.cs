using System;
using System.Collections.Generic;
using System.Linq;

List<int> numbers = new List<int> { 10, 25, 5, 30, 15, 40, 20, 35 };

// Find numbers > 20
var greaterThan20 = numbers.Where(n => /* condition */);

// Find numbers divisible by 5
var divisibleByFive = numbers.Where(n => /* condition */);

// Sort descending
var sorted = numbers.OrderByDescending(n => n);

// First 3 from sorted
var topThree = sorted.Take(3);

// Display results
Console.WriteLine("Greater than 20: " + string.Join(", ", greaterThan20));