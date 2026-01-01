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
    // Add 6 more students
};

// Sort by grade descending
var byGrade = students.OrderByDescending(s => /* key */);

// Aggregations
int totalStudents = students.Count();
double avgGrade = students.Average(s => /* selector */);
int highest = students.Max(s => /* selector */);
int lowest = students.Min(s => /* selector */);
int totalGrades = students.Sum(s => /* selector */);

// Math students sorted by name
var mathStudents = students
    .Where(s => /* filter */)
    .OrderBy(s => /* key */);

// Math average
double mathAvg = students
    .Where(s => /* filter */)
    .Average(s => /* selector */);