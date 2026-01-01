using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public string Name;
    public int Grade;
    public string Subject;
}

List<Student> students = new List<Student>
{
    new Student { Name = "Alice", Grade = 85, Subject = "Math" },
    new Student { Name = "Bob", Grade = 92, Subject = "Science" },
    new Student { Name = "Charlie", Grade = 78, Subject = "Math" },
    new Student { Name = "Diana", Grade = 95, Subject = "Science" },
    new Student { Name = "Eve", Grade = 88, Subject = "Math" },
    new Student { Name = "Frank", Grade = 72, Subject = "English" },
    new Student { Name = "Grace", Grade = 90, Subject = "English" },
    new Student { Name = "Henry", Grade = 83, Subject = "Math" }
};

var byGrade = students.OrderByDescending(s => s.Grade);
Console.WriteLine("Students by grade (high to low):");
foreach (var s in byGrade)
{
    Console.WriteLine(s.Name + ": " + s.Grade);
}

int totalStudents = students.Count();
double avgGrade = students.Average(s => s.Grade);
int highest = students.Max(s => s.Grade);
int lowest = students.Min(s => s.Grade);
int totalGrades = students.Sum(s => s.Grade);

Console.WriteLine("\nStatistics:");
Console.WriteLine("Total students: " + totalStudents);
Console.WriteLine("Average grade: " + avgGrade);
Console.WriteLine("Highest: " + highest + ", Lowest: " + lowest);
Console.WriteLine("Sum of all grades: " + totalGrades);

var mathStudents = students
    .Where(s => s.Subject == "Math")
    .OrderBy(s => s.Name);

Console.WriteLine("\nMath students (alphabetical):");
foreach (var s in mathStudents)
{
    Console.WriteLine(s.Name + ": " + s.Grade);
}

double mathAvg = students
    .Where(s => s.Subject == "Math")
    .Average(s => s.Grade);

Console.WriteLine("\nMath average: " + mathAvg);