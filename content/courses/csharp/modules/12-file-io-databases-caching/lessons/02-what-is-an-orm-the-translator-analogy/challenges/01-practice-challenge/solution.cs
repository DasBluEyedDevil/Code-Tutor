using System;
using System.Collections.Generic;
using System.Linq;

class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public decimal Price { get; set; }
    public int Year { get; set; }
}

Console.WriteLine("=== ORM TRANSLATION EXAMPLES ===");

Console.WriteLine("\nQuery 1: Find books by Orwell");
Console.WriteLine("C# LINQ: books.Where(b => b.Author == \"Orwell\")");
Console.WriteLine("SQL:     SELECT * FROM Books WHERE Author = 'Orwell'");

Console.WriteLine("\nQuery 2: Books over $20, sorted by price");
Console.WriteLine("C# LINQ: books.Where(b => b.Price > 20).OrderBy(b => b.Price)");
Console.WriteLine("SQL:     SELECT * FROM Books WHERE Price > 20 ORDER BY Price");

Console.WriteLine("\nQuery 3: Titles of books from 2020+");
Console.WriteLine("C# LINQ: books.Where(b => b.Year >= 2020).Select(b => b.Title)");
Console.WriteLine("SQL:     SELECT Title FROM Books WHERE Year >= 2020");

Console.WriteLine("\n=== WHY ORM (Entity Framework) IS BETTER ===");
Console.WriteLine("✅ Type Safety: Compiler catches typos! 'b.Autor' = compile error. SQL string typo = runtime crash!");
Console.WriteLine("✅ LINQ Familiarity: Same syntax as collections! No learning SQL for basic queries.");
Console.WriteLine("✅ Refactoring: Rename 'Title' to 'BookTitle'? IDE updates all LINQ! SQL strings = manual find/replace.");
Console.WriteLine("✅ Relationships: 'customer.Orders' just works! No manual JOINs for basic navigation.");
Console.WriteLine("✅ Change Tracking: EF knows what changed. Just modify object and SaveChanges()!");
Console.WriteLine("✅ Database Agnostic: Switch SQL Server → PostgreSQL? Change connection string. LINQ stays same!");
Console.WriteLine("\nORM = Productivity + Safety + Maintainability!");