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

// Query 1
Console.WriteLine("\nQuery 1: Find books by Orwell");
Console.WriteLine("C# LINQ: books.Where(b => b.Author == \"Orwell\")");
Console.WriteLine("SQL: SELECT * FROM Books WHERE Author = 'Orwell'");

// Query 2
Console.WriteLine("\nQuery 2: Books over $20, sorted by price");
Console.WriteLine("C# LINQ: /* write LINQ query */");
Console.WriteLine("SQL: /* write equivalent SQL */");

// Query 3
Console.WriteLine("\nQuery 3: Titles of books from 2020+");
Console.WriteLine("C# LINQ: /* write LINQ query */");
Console.WriteLine("SQL: /* write equivalent SQL */");

// Benefits
Console.WriteLine("\n=== WHY ORM IS BETTER ===");
Console.WriteLine("✅ Type safety: Compiler catches 'b.Autor' typo! SQL strings = runtime error!");
// Add more benefits