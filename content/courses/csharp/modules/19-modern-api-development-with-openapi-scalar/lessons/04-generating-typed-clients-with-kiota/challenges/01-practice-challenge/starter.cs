using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

Console.WriteLine("=== Kiota Client Generation ===");

// TODO: Print the Kiota CLI command to generate the client
// kiota generate -l CSharp -o ./BookstoreClient ...

Console.WriteLine("\n=== Using Generated Client ===");

// TODO: Create authentication provider (anonymous for demo)

// TODO: Create HTTP client adapter with base URL

// TODO: Create the typed API client
// var client = new BookstoreApiClient(adapter);

// Simulated client usage (comments showing what real code would look like)
Console.WriteLine("\nTyped API Calls:");

// TODO: Show GET all books
// var books = await client.Books.GetAsync();

// TODO: Show GET book by ISBN
// var book = await client.Books["978-0-13-468599-1"].GetAsync();

// TODO: Show POST create book
// var newBook = await client.Books.PostAsync(new CreateBookRequest { ... });

// TODO: Show search with query parameters
// var results = await client.Books.GetAsync(config => {
//     config.QueryParameters.Genre = "Fiction";
// });

// TODO: Show error handling for 404

// TODO: Print benefits of Kiota
Console.WriteLine("\n=== Benefits of Kiota ===");