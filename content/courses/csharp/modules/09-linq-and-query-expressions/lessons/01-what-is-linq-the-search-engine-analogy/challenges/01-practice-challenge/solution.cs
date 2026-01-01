using System;
using System.Collections.Generic;
using System.Linq;

List<int> numbers = new List<int> { 10, 25, 5, 30, 15, 40, 20, 35 };

var greaterThan20 = numbers.Where(n => n > 20);
Console.WriteLine("Greater than 20: " + string.Join(", ", greaterThan20));

var divisibleByFive = numbers.Where(n => n % 5 == 0);
Console.WriteLine("Divisible by 5: " + string.Join(", ", divisibleByFive));

var sorted = numbers.OrderByDescending(n => n);
Console.WriteLine("Sorted descending: " + string.Join(", ", sorted));

var topThree = sorted.Take(3);
Console.WriteLine("Top 3: " + string.Join(", ", topThree));