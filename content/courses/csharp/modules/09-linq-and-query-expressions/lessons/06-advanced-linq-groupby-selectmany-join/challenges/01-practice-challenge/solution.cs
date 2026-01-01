using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public string Name { get; set; }
    public string Grade { get; set; }
    public int Score { get; set; }
}

var students = new List<Student>
{
    new Student { Name = "Alice", Grade = "A", Score = 95 },
    new Student { Name = "Bob", Grade = "B", Score = 82 },
    new Student { Name = "Carol", Grade = "A", Score = 91 },
    new Student { Name = "Dave", Grade = "C", Score = 74 },
    new Student { Name = "Eve", Grade = "B", Score = 88 },
    new Student { Name = "Frank", Grade = "A", Score = 97 }
};

Console.WriteLine("=== Students by Grade ===");
var byGrade = students.GroupBy(s => s.Grade).OrderBy(g => g.Key);
foreach (var group in byGrade)
{
    Console.WriteLine($"\nGrade {group.Key}:");
    foreach (var student in group)
    {
        Console.WriteLine($"  {student.Name} ({student.Score})");
    }
}

Console.WriteLine("\n=== Average Score per Grade ===");
var gradeAverages = students
    .GroupBy(s => s.Grade)
    .Select(g => new { Grade = g.Key, Average = g.Average(s => s.Score) })
    .OrderBy(x => x.Grade);

foreach (var avg in gradeAverages)
{
    Console.WriteLine($"Grade {avg.Grade}: Average = {avg.Average:F1}");
}

var courses = new[]
{
    new { Name = "Math", StudentNames = new[] { "Alice", "Bob", "Carol" } },
    new { Name = "Science", StudentNames = new[] { "Bob", "Dave", "Eve" } },
    new { Name = "English", StudentNames = new[] { "Alice", "Eve", "Frank" } }
};

var allStudentNames = courses.SelectMany(c => c.StudentNames).Distinct();
Console.WriteLine("\n=== All Students (SelectMany) ===");
Console.WriteLine(string.Join(", ", allStudentNames));