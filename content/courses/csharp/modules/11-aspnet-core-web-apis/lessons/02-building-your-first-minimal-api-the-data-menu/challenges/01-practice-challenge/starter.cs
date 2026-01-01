using Microsoft.AspNetCore.Builder;
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
    // Add 4-5 books
};

// GET all books
app.MapGet("/api/books", () => books);

// GET book by ID
app.MapGet("/api/books/{id}", (int id) =>
{
    // Find and return book or NotFound
});

// GET available books
app.MapGet("/api/books/available", () =>
{
    // Filter available books
});

// GET books by author
app.MapGet("/api/books/author/{author}", (string author) =>
{
    // Filter by author
});

// GET statistics
app.MapGet("/api/books/stats", () =>
{
    // Return stats object
});

Console.WriteLine("Book Library API Ready!");