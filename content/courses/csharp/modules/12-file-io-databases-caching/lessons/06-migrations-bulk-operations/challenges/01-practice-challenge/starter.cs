using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public string Department { get; set; } = string.Empty;
}

class CompanyDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=company.db");
    }
}

Console.WriteLine("=== MIGRATIONS WORKFLOW ===");
Console.WriteLine("\n1. Create initial entities and DbContext");
Console.WriteLine("   Command: dotnet ef migrations add InitialCreate");
Console.WriteLine("   Result: Creates Migrations/InitialCreate.cs");

Console.WriteLine("\n2. Apply migration to database");
Console.WriteLine("   Command: dotnet ef database update");
Console.WriteLine("   Result: Database created with Employees table");

Console.WriteLine("\n3. Add new property to Employee class");
Console.WriteLine("   Code: public bool IsActive { get; set; }");

Console.WriteLine("\n4. Create migration for new property");
Console.WriteLine("   Command: dotnet ef migrations add AddIsActive");
Console.WriteLine("   Result: Migration file for schema change");

Console.WriteLine("\n5. Apply the migration");
Console.WriteLine("   Command: dotnet ef database update");
Console.WriteLine("   Result: IsActive column added to Employees table");

Console.WriteLine("\n=== BULK OPERATIONS (EF Core 7+) ===");

Console.WriteLine("\nBULK UPDATE: 10% raise for Engineering");
Console.WriteLine("Code:");
Console.WriteLine("  context.Employees");
Console.WriteLine("    .Where(e => e.Department == 'Engineering')");
Console.WriteLine("    .ExecuteUpdate(s => s.SetProperty(e => e.Salary, e => e.Salary * 1.1m));");
Console.WriteLine("\nGenerated SQL:");
Console.WriteLine("  UPDATE Employees SET Salary = Salary * 1.1 WHERE Department = 'Engineering'");
Console.WriteLine("✓ FAST: Single SQL statement!");

Console.WriteLine("\nBULK DELETE: Remove low-salary employees");
Console.WriteLine("Code:");
Console.WriteLine("  context.Employees.Where(e => e.Salary < 30000).ExecuteDelete();");
Console.WriteLine("\nGenerated SQL:");
Console.WriteLine("  DELETE FROM Employees WHERE Salary < 30000");
Console.WriteLine("✓ FAST: Single SQL statement!");

Console.WriteLine("\n=== PERFORMANCE COMPARISON ===");
Console.WriteLine("\nTRADITIONAL (Slow):");
Console.WriteLine("  var employees = context.Employees.Where(e => ...).ToList();");
Console.WriteLine("  foreach (var e in employees) { e.Salary *= 1.1; }");
Console.WriteLine("  context.SaveChanges();");
Console.WriteLine("  Result: 1000 employees = 1000 UPDATE statements!");

Console.WriteLine("\nBULK (Fast):");
Console.WriteLine("  context.Employees.Where(e => ...).ExecuteUpdate(...);");
Console.WriteLine("  Result: 1000 employees = 1 UPDATE statement!");
Console.WriteLine("  Performance: 100x-1000x faster for large datasets!");