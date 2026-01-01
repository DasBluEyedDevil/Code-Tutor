// Grade Book Manager

using System;
using System.Collections.Generic;

List<string> names = new List<string>();
List<int> grades = new List<int>();

// Add 5 students
names.Add("Alice");
grades.Add(85);
names.Add("Bob");
grades.Add(72);
names.Add("Charlie");
grades.Add(91);
names.Add("Diana");
grades.Add(68);
names.Add("Eve");
grades.Add(95);

// Display all students
Console.WriteLine("=== GRADE BOOK ===");
for (int i = 0; i < names.Count; i++)
{
    Console.WriteLine($"{names[i]}: {grades[i]}");
}

// Calculate total and average
int total = 0;
foreach (int grade in grades)
{
    total += grade;
}
double average = (double)total / grades.Count;

// Find highest and lowest
int highest = grades[0];
int lowest = grades[0];
foreach (int grade in grades)
{
    if (grade > highest)
        highest = grade;
    if (grade < lowest)
        lowest = grade;
}

// Count passing
int passedCount = 0;
foreach (int grade in grades)
{
    if (grade >= 60)
        passedCount++;
}

Console.WriteLine($"\nTotal: {total}");
Console.WriteLine($"Average: {average:F1}");
Console.WriteLine($"Highest: {highest}");
Console.WriteLine($"Lowest: {lowest}");
Console.WriteLine($"Passed: {passedCount}/{grades.Count}");

// Sort and display
List<int> sortedGrades = new List<int>(grades);  // Copy first
sortedGrades.Sort();

Console.Write("\nSorted grades: ");
foreach (int grade in sortedGrades)
{
    Console.Write(grade + " ");
}

// Check contains
Console.WriteLine($"\n\nContains 90? {grades.Contains(90)}");