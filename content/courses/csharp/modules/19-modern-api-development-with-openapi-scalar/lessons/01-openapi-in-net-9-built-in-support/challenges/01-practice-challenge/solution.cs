var builder = WebApplication.CreateBuilder(args);

// Add OpenAPI services (.NET 9 built-in!)
builder.Services.AddOpenApi();

var app = builder.Build();

// Expose OpenAPI document at /openapi/v1.json
app.MapOpenApi();

// Sample data
var books = new List<Book>
{
    new("978-0-13-468599-1", "Clean Code", "Robert Martin", 39.99m),
    new("978-0-596-51774-8", "JavaScript: The Good Parts", "Douglas Crockford", 29.99m)
};

// GET /books - Return all books
app.MapGet("/books", () => books)
    .WithName("GetBooks")
    .WithDescription("Returns all books in the catalog")
    .WithTags("Books")
    .Produces<List<Book>>(StatusCodes.Status200OK);

// GET /books/{isbn} - Return book by ISBN
app.MapGet("/books/{isbn}", (string isbn) =>
{
    var book = books.FirstOrDefault(b => b.Isbn == isbn);
    return book is not null 
        ? Results.Ok(book) 
        : Results.NotFound($"Book with ISBN {isbn} not found");
})
    .WithName("GetBookByIsbn")
    .WithDescription("Returns a specific book by its ISBN identifier")
    .WithTags("Books")
    .Produces<Book>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound);

// POST /books - Create new book
app.MapPost("/books", (CreateBookRequest request) =>
{
    var isbn = $"978-{Random.Shared.Next(1000000000):D10}";
    var book = new Book(isbn, request.Title, request.Author, request.Price);
    books.Add(book);
    return Results.Created($"/books/{book.Isbn}", book);
})
    .WithName("CreateBook")
    .WithDescription("Adds a new book to the catalog")
    .WithTags("Books")
    .Accepts<CreateBookRequest>("application/json")
    .Produces<Book>(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status400BadRequest);

// DELETE /books/{isbn} - Delete book
app.MapDelete("/books/{isbn}", (string isbn) =>
{
    var book = books.FirstOrDefault(b => b.Isbn == isbn);
    if (book is null)
        return Results.NotFound($"Book with ISBN {isbn} not found");
    
    books.Remove(book);
    return Results.NoContent();
})
    .WithName("DeleteBook")
    .WithDescription("Removes a book from the catalog")
    .WithTags("Books")
    .Produces(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status404NotFound);

Console.WriteLine("OpenAPI available at: /openapi/v1.json");
app.Run();

// Data models
public record Book(string Isbn, string Title, string Author, decimal Price);

public record CreateBookRequest(string Title, string Author, decimal Price);