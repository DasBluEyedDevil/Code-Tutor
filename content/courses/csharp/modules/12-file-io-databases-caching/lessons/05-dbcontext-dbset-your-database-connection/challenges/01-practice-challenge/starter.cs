using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}

class TaskDbContext : DbContext
{
    public DbSet<TaskItem> Tasks { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=tasks.db");
    }
}

Console.WriteLine("=== DbContext CHANGE TRACKING DEMO ===");

// Simulate tracking
var task = new TaskItem { Title = "Learn EF Core", IsCompleted = false };

Console.WriteLine("\n[1] NEW TASK CREATED");
Console.WriteLine("Task object created in memory");
Console.WriteLine("State: Not tracked (Detached)");

Console.WriteLine("\n[2] ADDED TO DbContext");
Console.WriteLine("context.Tasks.Add(task)");
Console.WriteLine("State: Added (will INSERT on SaveChanges)");

Console.WriteLine("\n[3] SAVED TO DATABASE");
Console.WriteLine("context.SaveChanges()");
Console.WriteLine("State: Unchanged (in sync with database)");

Console.WriteLine("\n[4] MODIFIED");
Console.WriteLine("task.Title = 'Master EF Core'");
Console.WriteLine("State: Modified (will UPDATE on SaveChanges)");

Console.WriteLine("\n[5] MARKED FOR DELETION");
Console.WriteLine("context.Tasks.Remove(task)");
Console.WriteLine("State: Deleted (will DELETE on SaveChanges)");

Console.WriteLine("\n=== DbContext RESPONSIBILITIES ===");
Console.WriteLine("✓ Connection Management: Opens/closes database connection");
Console.WriteLine("✓ Change Tracking: Remembers Added/Modified/Deleted");
Console.WriteLine("✓ Query Translation: Converts LINQ to SQL");
Console.WriteLine("✓ Transaction: SaveChanges() is atomic (all-or-nothing)");

Console.WriteLine("\n⚠️  IMPORTANT: Always use 'using' with DbContext!");
Console.WriteLine("   using var context = new DbContext();  // Modern C# 8+ syntax");