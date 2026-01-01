using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Book
{
    // Define properties
}

// Create list of books
List<Book> books = new List<Book>();

// Add 3 books

// Serialize to JSON
string json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
Console.WriteLine("JSON:\n" + json);

// Save to file

// Read from file and deserialize

// Display book titles