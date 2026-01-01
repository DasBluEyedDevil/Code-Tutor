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

Console.WriteLine("=== DbContext CHANGE TRACKING DEMONSTRATION ===");
Console.WriteLine("Showing how EF Core tracks entity states\n");

var task = new TaskItem { Title = "Learn EF Core", IsCompleted = false };

Console.WriteLine("[1] NEW TASK CREATED");
Console.WriteLine("    var task = new TaskItem { Title = 'Learn EF Core' };");
Console.WriteLine("    State: Detached (not tracked by DbContext)");
Console.WriteLine("    Database: No changes yet\n");

Console.WriteLine("[2] ADDED TO DbContext");
Console.WriteLine("    context.Tasks.Add(task);");
Console.WriteLine("    State: Added");
Console.WriteLine("    Database: Will INSERT on SaveChanges()\n");

Console.WriteLine("[3] SAVED TO DATABASE");
Console.WriteLine("    context.SaveChanges();");
Console.WriteLine("    State: Unchanged");
Console.WriteLine("    Database: Row inserted, EF in sync\n");

Console.WriteLine("[4] PROPERTY MODIFIED");
Console.WriteLine("    task.Title = 'Master EF Core';");
Console.WriteLine("    State: Modified (EF detected the change!)");
Console.WriteLine("    Database: Will UPDATE on SaveChanges()\n");

Console.WriteLine("[5] MARKED FOR DELETION");
Console.WriteLine("    context.Tasks.Remove(task);");
Console.WriteLine("    State: Deleted");
Console.WriteLine("    Database: Will DELETE on SaveChanges()\n");

Console.WriteLine("=== DbContext RESPONSIBILITIES ===");
Console.WriteLine("✓ Connection Management:");
Console.WriteLine("    Opens connection when needed, closes on Dispose");
Console.WriteLine("\n✓ Change Tracking:");
Console.WriteLine("    Tracks: Unchanged, Added, Modified, Deleted states");
Console.WriteLine("    Automatically detects property changes!");
Console.WriteLine("\n✓ Query Translation:");
Console.WriteLine("    LINQ: context.Tasks.Where(t => t.IsCompleted)");
Console.WriteLine("    SQL:  SELECT * FROM Tasks WHERE IsCompleted = 1");
Console.WriteLine("\n✓ Transaction Management:");
Console.WriteLine("    SaveChanges() wraps in transaction (atomic!)");
Console.WriteLine("    All changes succeed OR all fail (consistency!)\n");

Console.WriteLine("=== CRITICAL: Always Dispose! ===");
Console.WriteLine("✓ MODERN:  using var context = new DbContext();  // C# 8+");
Console.WriteLine("✓ CLASSIC: using (var context = new DbContext()) { ... }");
Console.WriteLine("✗ WRONG:   var context = new DbContext(); (never disposed!)");
Console.WriteLine("\nWithout dispose: Connection leaks, memory leaks, locks!");