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

Console.WriteLine("═══════════════════════════════════════════");
Console.WriteLine("  EF CORE: MIGRATIONS & BULK OPERATIONS");
Console.WriteLine("═══════════════════════════════════════════\n");

Console.WriteLine("=== PART 1: MIGRATIONS WORKFLOW ===");
Console.WriteLine("\nStep 1: Create Initial Schema");
Console.WriteLine("  → dotnet ef migrations add InitialCreate");
Console.WriteLine("  Creates: Migrations/20240115120000_InitialCreate.cs");
Console.WriteLine("  Contains: Up() creates Employees table, Down() drops it\n");

Console.WriteLine("Step 2: Apply to Database");
Console.WriteLine("  → dotnet ef database update");
Console.WriteLine("  Result: Database created with schema from code\n");

Console.WriteLine("Step 3: Evolve Schema (Add property)");
Console.WriteLine("  Code change: public bool IsActive { get; set; }");
Console.WriteLine("  → dotnet ef migrations add AddIsActive");
Console.WriteLine("  Creates: New migration file\n");

Console.WriteLine("Step 4: Apply New Migration");
Console.WriteLine("  → dotnet ef database update");
Console.WriteLine("  Result: IsActive column added to table");
Console.WriteLine("  ✓ Database schema evolves with code!");

Console.WriteLine("\n=== PART 2: BULK OPERATIONS (EF Core 7+) ===");

Console.WriteLine("\n[BULK UPDATE] 10% raise for Engineering department");
Console.WriteLine("\n  OLD WAY (Slow):");
Console.WriteLine("    var engineers = context.Employees");
Console.WriteLine("                          .Where(e => e.Department == 'Engineering')");
Console.WriteLine("                          .ToList();  // Load into memory");
Console.WriteLine("    foreach (var e in engineers)");
Console.WriteLine("        e.Salary *= 1.1m;");
Console.WriteLine("    context.SaveChanges();  // N UPDATE statements!");
Console.WriteLine("    Performance: 1000 rows = 1000 database round-trips\n");

Console.WriteLine("  NEW WAY (Fast):");
Console.WriteLine("    context.Employees");
Console.WriteLine("        .Where(e => e.Department == 'Engineering')");
Console.WriteLine("        .ExecuteUpdate(s => s.SetProperty(e => e.Salary, e => e.Salary * 1.1m));");
Console.WriteLine("\n    Generated SQL:");
Console.WriteLine("      UPDATE Employees ");
Console.WriteLine("      SET Salary = Salary * 1.1 ");
Console.WriteLine("      WHERE Department = 'Engineering'");
Console.WriteLine("\n    Performance: 1000 rows = 1 database statement!");
Console.WriteLine("    ✓ 100x-1000x FASTER!\n");

Console.WriteLine("[BULK DELETE] Remove employees with salary < $30,000");
Console.WriteLine("\n  Code:");
Console.WriteLine("    int deleted = context.Employees");
Console.WriteLine("        .Where(e => e.Salary < 30000)");
Console.WriteLine("        .ExecuteDelete();");
Console.WriteLine("\n  Generated SQL:");
Console.WriteLine("    DELETE FROM Employees WHERE Salary < 30000");
Console.WriteLine("\n  ✓ Single SQL statement, no loading into memory!");

Console.WriteLine("\n=== KEY BENEFITS ===");
Console.WriteLine("\nMigrations:");
Console.WriteLine("  ✓ Version control for database schema");
Console.WriteLine("  ✓ Rollback capability (go to previous version)");
Console.WriteLine("  ✓ Team-friendly (migrations in source control)");
Console.WriteLine("  ✓ Deployment automation (apply on production)");

Console.WriteLine("\nBulk Operations:");
Console.WriteLine("  ✓ Massive performance gains (100x+)");
Console.WriteLine("  ✓ Less memory usage (no loading into C#)");
Console.WriteLine("  ✓ Atomic operations (all-or-nothing)");
Console.WriteLine("  ✓ Database-side execution (efficient!)");