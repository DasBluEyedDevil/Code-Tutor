var builder = WebApplication.CreateBuilder(args);

// TODO: Add OpenAPI services

var app = builder.Build();

// TODO: Map OpenAPI endpoint

// Sample data
var books = new List<Book>
{
    new("978-0-13-468599-1", "Clean Code", "Robert Martin", 39.99m),
    new("978-0-596-51774-8", "JavaScript: The Good Parts", "Douglas Crockford", 29.99m)
};

// TODO: GET /books - Return all books
// - WithName("GetBooks")
// - WithDescription("Returns all books in the catalog")
// - WithTags("Books")
// - Produces<List<Book>>(StatusCodes.Status200OK)

// TODO: GET /books/{isbn} - Return book by ISBN
// - WithName("GetBookByIsbn")
// - WithDescription("Returns a specific book by ISBN")
// - WithTags("Books")
// - Produces<Book>(200), Produces(404)

// TODO: POST /books - Create new book
// - WithName("CreateBook")
// - WithDescription("Adds a new book to the catalog")
// - WithTags("Books")
// - Accepts<CreateBookRequest>, Produces<Book>(201)

// TODO: DELETE /books/{isbn} - Delete book
// - WithName("DeleteBook")
// - WithDescription("Removes a book from the catalog")
// - WithTags("Books")
// - Produces(204), Produces(404)

app.Run();

// TODO: Define Book record (Isbn, Title, Author, Price)
// TODO: Define CreateBookRequest record (Title, Author, Price)