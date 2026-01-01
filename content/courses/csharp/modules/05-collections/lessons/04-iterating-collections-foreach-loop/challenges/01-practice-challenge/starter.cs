// Grade Book Manager

using System;
using System.Collections.Generic;

List<string> names = new List<string>();
List<int> grades = new List<int>();

// Add 5 students
names.Add("Alice");
grades.Add(85);
// Add 4 more...

// Display all students
Console.WriteLine("=== GRADE BOOK ===");
for (int i = 0; i < names.Count; i++)
{
    Console.WriteLine($"{names[i]}: {grades[i]}");
}

// Calculate statistics using foreach

// Sort and display