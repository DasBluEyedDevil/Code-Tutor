using System;
using System.Collections.Generic;
using System.Linq;

// Student class
class Student
{
    public string Name { get; set; }
    public string Grade { get; set; }
    public int Score { get; set; }
}

// Create students
var students = new List<Student>
{
    new Student { Name = "Alice", Grade = "A", Score = 95 },
    // Add more students...
};

// Group by grade and print
var byGrade = students.GroupBy(s => s.Grade);
foreach (var group in byGrade)
{
    Console.WriteLine($"Grade {group.Key}:");
    // Print each student in group
}

// Average score per grade
var gradeAverages = students
    .GroupBy(s => s.Grade)
    .Select(g => new { /* create summary */ });

// Print averages

// SelectMany example
var courses = new[]
{
    new { Name = "Math", StudentNames = new[] { "Alice", "Bob" } },
    // Add more courses...
};

var allStudentNames = courses.SelectMany(c => c.StudentNames);
Console.WriteLine("\nAll students: " + string.Join(", ", allStudentNames.Distinct()));