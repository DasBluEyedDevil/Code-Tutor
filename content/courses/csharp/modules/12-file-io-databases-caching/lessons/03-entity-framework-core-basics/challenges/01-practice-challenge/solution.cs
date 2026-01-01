using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Major { get; set; } = string.Empty;
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
Console.WriteLine("Demonstrating CRUD operations with EF Core\n");

Console.WriteLine("[CREATE] Adding students...");
var student1 = new Student { Id = 1, Name = "Alice", Age = 22, Major = "Computer Science" };
var student2 = new Student { Id = 2, Name = "Bob", Age = 19, Major = "Mathematics" };
var student3 = new Student { Id = 3, Name = "Charlie", Age = 23, Major = "Physics" };

Console.WriteLine("  context.Students.Add(student1);");
Console.WriteLine("  context.Students.Add(student2);");
Console.WriteLine("  context.Students.Add(student3);");
Console.WriteLine("  context.SaveChanges();");
Console.WriteLine("\n  Generated SQL:");
Console.WriteLine("    INSERT INTO Students (Name, Age, Major) VALUES ('Alice', 22, 'Computer Science')");
Console.WriteLine("    INSERT INTO Students (Name, Age, Major) VALUES ('Bob', 19, 'Mathematics')");
Console.WriteLine("    INSERT INTO Students (Name, Age, Major) VALUES ('Charlie', 23, 'Physics')");
Console.WriteLine("  ✓ 3 students added!\n");

Console.WriteLine("[READ] Querying students over age 20...");
Console.WriteLine("  var adults = context.Students.Where(s => s.Age > 20).ToList();");
Console.WriteLine("\n  Generated SQL:");
Console.WriteLine("    SELECT * FROM Students WHERE Age > 20");
Console.WriteLine("  ✓ Found: Alice (22), Charlie (23)\n");

Console.WriteLine("[UPDATE] Changing Alice's major...");
Console.WriteLine("  var alice = context.Students.Find(1);");
Console.WriteLine("  alice.Major = 'Data Science';");
Console.WriteLine("  context.SaveChanges();");
Console.WriteLine("\n  Generated SQL:");
Console.WriteLine("    UPDATE Students SET Major = 'Data Science' WHERE Id = 1");
Console.WriteLine("  ✓ Updated!\n");

Console.WriteLine("[DELETE] Removing student with ID 2...");
Console.WriteLine("  var bob = context.Students.Find(2);");
Console.WriteLine("  context.Students.Remove(bob);");
Console.WriteLine("  context.SaveChanges();");
Console.WriteLine("\n  Generated SQL:");
Console.WriteLine("    DELETE FROM Students WHERE Id = 2");
Console.WriteLine("  ✓ Deleted!\n");

Console.WriteLine("=== EF CORE TRACKS CHANGES AUTOMATICALLY ===");
Console.WriteLine("You modify objects in C# → EF Core generates SQL → Database updated!");