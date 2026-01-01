using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

class BlogPost
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Content { get; set; } = string.Empty;
    
    public DateTime PublishedDate { get; set; }
    
    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;
}

class Author
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string Email { get; set; } = string.Empty;
    
    public List<BlogPost> BlogPosts { get; set; } = new();
}

class BlogDbContext : DbContext
{
    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<Author> Authors { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=blog.db");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
            .HasMany(a => a.BlogPosts)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId);
        
        modelBuilder.Entity<Author>().HasData(
            new Author { Id = 1, Name = "Jane Smith", Email = "jane@example.com" }
        );
    }
}

Console.WriteLine("=== CODE-FIRST DATABASE SCHEMA ===");
Console.WriteLine("Database generated from C# classes!\n");

Console.WriteLine("Table: Authors");
Console.WriteLine("  Columns:");
Console.WriteLine("    - Id (INT, PRIMARY KEY, AUTO-INCREMENT)");
Console.WriteLine("    - Name (VARCHAR(100), NOT NULL)");
Console.WriteLine("    - Email (VARCHAR, NOT NULL)");

Console.WriteLine("\nTable: BlogPosts");
Console.WriteLine("  Columns:");
Console.WriteLine("    - Id (INT, PRIMARY KEY, AUTO-INCREMENT)");
Console.WriteLine("    - Title (VARCHAR(200), NOT NULL)");
Console.WriteLine("    - Content (TEXT, NOT NULL)");
Console.WriteLine("    - PublishedDate (DATETIME)");
Console.WriteLine("    - AuthorId (INT, FOREIGN KEY → Authors.Id)");

Console.WriteLine("\nRelationships:");
Console.WriteLine("  ✓ Author HAS MANY BlogPosts (1-to-Many)");
Console.WriteLine("  ✓ BlogPost BELONGS TO one Author");

Console.WriteLine("\nSeed Data:");
Console.WriteLine("  ✓ Author: Jane Smith (jane@example.com)");

Console.WriteLine("\n=== BENEFITS OF CODE-FIRST ===");
Console.WriteLine("✅ C# classes = Source of truth");
Console.WriteLine("✅ Database auto-generated from classes");
Console.WriteLine("✅ Refactor code → Database updates via migrations");
Console.WriteLine("✅ Version control friendly (classes in Git!)");