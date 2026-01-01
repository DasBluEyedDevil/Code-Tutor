using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public bool IsAvailable { get; set; }
}

var books = new List<Book>
{
    new Book { Id = 1, Title = "1984", Author = "Orwell", Year = 1949, IsAvailable = true },
    new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Lee", Year = 1960, IsAvailable = false },
    new Book { Id = 3, Title = "Animal Farm", Author = "Orwell", Year = 1945, IsAvailable = true },
    new Book { Id = 4, Title = "The Great Gatsby", Author = "Fitzgerald", Year = 1925, IsAvailable = true }
};

app.MapGet("/api/books", () => books);

app.MapGet("/api/books/{id}", (int id) =>
{
    var book = books.FirstOrDefault(b => b.Id == id);
    return book is not null ? Results.Ok(book) : Results.NotFound();
});

app.MapGet("/api/books/available", () =>
{
    return books.Where(b => b.IsAvailable);
});

app.MapGet("/api/books/author/{author}", (string author) =>
{
    return books.Where(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase));
});

app.MapGet("/api/books/stats", () =>
{
    return new 
    { 
        Total = books.Count, 
        Available = books.Count(b => b.IsAvailable),
        Unavailable = books.Count(b => !b.IsAvailable)
    };
});

Console.WriteLine("Book Library API Ready!");
Console.WriteLine("Endpoints:");
Console.WriteLine("  GET /api/books");
Console.WriteLine("  GET /api/books/{id}");
Console.WriteLine("  GET /api/books/available");
Console.WriteLine("  GET /api/books/author/{author}");
Console.WriteLine("  GET /api/books/stats");