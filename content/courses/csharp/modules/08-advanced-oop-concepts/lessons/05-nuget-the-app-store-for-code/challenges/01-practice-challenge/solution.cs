using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
}

List<Book> books = new List<Book>();
books.Add(new Book { Title = "1984", Author = "Orwell", Year = 1949, Price = 15.99m });
books.Add(new Book { Title = "To Kill a Mockingbird", Author = "Lee", Year = 1960, Price = 12.99m });
books.Add(new Book { Title = "The Great Gatsby", Author = "Fitzgerald", Year = 1925, Price = 10.99m });

string json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
Console.WriteLine("JSON:\n" + json);

File.WriteAllText("books.json", json);
Console.WriteLine("\nSaved to books.json");

string fileContent = File.ReadAllText("books.json");
List<Book> loadedBooks = JsonSerializer.Deserialize<List<Book>>(fileContent)!;

Console.WriteLine("\nLoaded books:");
foreach (Book book in loadedBooks)
{
    Console.WriteLine("- " + book.Title + " by " + book.Author);
}