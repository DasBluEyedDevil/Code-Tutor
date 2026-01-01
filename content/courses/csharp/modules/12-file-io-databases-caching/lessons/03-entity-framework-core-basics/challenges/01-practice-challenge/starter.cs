using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Major { get; set; }
}

class SchoolDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=school.db");
    }
}

Console.WriteLine("=== ENTITY FRAMEWORK CORE SIMULATION ===");

// Simulate CREATE
Console.WriteLine("\n[CREATE] Adding students...");
var student1 = new Student { Name = "Alice", Age = 22, Major = "Computer Science" };
var student2 = new Student { Name = "Bob", Age = 19, Major = "Mathematics" };
var student3 = new Student { Name = "Charlie", Age = 23, Major = "Physics" };

Console.WriteLine("SQL: INSERT INTO Students (Name, Age, Major) VALUES ('Alice', 22, 'Computer Science')");
Console.WriteLine("SQL: INSERT INTO Students (Name, Age, Major) VALUES ('Bob', 19, 'Mathematics')");
Console.WriteLine("SQL: INSERT INTO Students (Name, Age, Major) VALUES ('Charlie', 23, 'Physics')");
Console.WriteLine("3 students added!");

// Simulate READ
Console.WriteLine("\n[READ] Querying students over age 20...");
Console.WriteLine("SQL: SELECT * FROM Students WHERE Age > 20");
Console.WriteLine("Found: Alice (22), Charlie (23)");

// Simulate UPDATE
Console.WriteLine("\n[UPDATE] Changing Alice's major...");
Console.WriteLine("SQL: UPDATE Students SET Major = 'Data Science' WHERE Id = 1");
Console.WriteLine("Updated!");

// Simulate DELETE
Console.WriteLine("\n[DELETE] Removing student with ID 2...");
Console.WriteLine("SQL: DELETE FROM Students WHERE Id = 2");
Console.WriteLine("Deleted!");