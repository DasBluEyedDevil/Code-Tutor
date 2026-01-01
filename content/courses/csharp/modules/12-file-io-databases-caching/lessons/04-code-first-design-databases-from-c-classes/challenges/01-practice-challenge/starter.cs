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
        // Configure relationship
        modelBuilder.Entity<Author>()
            .HasMany(a => a.BlogPosts)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId);
        
        // Seed data
        modelBuilder.Entity<Author>().HasData(
            new Author { Id = 1, Name = "Jane Smith", Email = "jane@example.com" }
        );
    }
}

Console.WriteLine("=== CODE-FIRST DATABASE SCHEMA ===");
Console.WriteLine("\nTable: Authors");
Console.WriteLine("  - Id (INT, PRIMARY KEY)");
Console.WriteLine("  - Name (VARCHAR(100), NOT NULL)");
Console.WriteLine("  - Email (VARCHAR, NOT NULL)");

Console.WriteLine("\nTable: BlogPosts");
Console.WriteLine("  - Id (INT, PRIMARY KEY)");
Console.WriteLine("  - Title (VARCHAR(200), NOT NULL)");
Console.WriteLine("  - Content (TEXT, NOT NULL)");
Console.WriteLine("  - PublishedDate (DATETIME)");
Console.WriteLine("  - AuthorId (INT, FOREIGN KEY → Authors.Id)");

Console.WriteLine("\nRelationship:");
Console.WriteLine("  Author 1-to-Many BlogPosts");