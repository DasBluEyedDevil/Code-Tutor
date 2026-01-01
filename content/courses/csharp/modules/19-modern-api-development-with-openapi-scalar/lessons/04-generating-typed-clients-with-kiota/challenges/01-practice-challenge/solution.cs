using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

Console.WriteLine("=== Kiota Client Generation ===");
Console.WriteLine();

// Print the Kiota CLI command
Console.WriteLine("Generate client with this command:");
Console.WriteLine();
Console.WriteLine("kiota generate \\");
Console.WriteLine("  -l CSharp \\");
Console.WriteLine("  -o ./BookstoreClient \\");
Console.WriteLine("  -d https://api.bookstore.com/openapi.json \\");
Console.WriteLine("  -c BookstoreApiClient \\");
Console.WriteLine("  -n Bookstore.Client");

Console.WriteLine("\n=== Using Generated Client ===");
Console.WriteLine();

// Create authentication provider
Console.WriteLine("// Set up authentication (anonymous for public API)");
Console.WriteLine("var authProvider = new AnonymousAuthenticationProvider();");
var authProvider = new AnonymousAuthenticationProvider();

// Create HTTP client adapter
Console.WriteLine();
Console.WriteLine("// Create adapter with base URL");
Console.WriteLine("var adapter = new HttpClientRequestAdapter(authProvider)");
Console.WriteLine("{{");
Console.WriteLine("    BaseUrl = \"https://api.bookstore.com\"");
Console.WriteLine("}}");

// Create client
Console.WriteLine();
Console.WriteLine("// Create strongly-typed API client");
Console.WriteLine("var client = new BookstoreApiClient(adapter);");

Console.WriteLine("\n=== Typed API Calls ===");
Console.WriteLine();

// GET all books
Console.WriteLine("// GET all books - fully typed!");
Console.WriteLine("var books = await client.Books.GetAsync();");
Console.WriteLine("foreach (var book in books)");
Console.WriteLine("{{");
Console.WriteLine("    Console.WriteLine($\"{{book.Title}} by {{book.Author}}\");");
Console.WriteLine("}}");

Console.WriteLine();

// GET by ISBN
Console.WriteLine("// GET single book by ISBN");
Console.WriteLine("var book = await client.Books[\"978-0-13-468599-1\"].GetAsync();");
Console.WriteLine("Console.WriteLine($\"Found: {{book?.Title}}\");");

Console.WriteLine();

// POST create
Console.WriteLine("// POST create new book");
Console.WriteLine("var newBook = await client.Books.PostAsync(new CreateBookRequest");
Console.WriteLine("{{");
Console.WriteLine("    Title = \"My New Book\",");
Console.WriteLine("    Author = \"Jane Doe\",");
Console.WriteLine("    Price = 29.99m");
Console.WriteLine("}});");

Console.WriteLine();

// Search with query parameters
Console.WriteLine("// Search with typed query parameters");
Console.WriteLine("var results = await client.Books.GetAsync(config =>");
Console.WriteLine("{{");
Console.WriteLine("    config.QueryParameters.Genre = \"Science Fiction\";");
Console.WriteLine("    config.QueryParameters.MinPrice = 10m;");
Console.WriteLine("    config.QueryParameters.MaxPrice = 50m;");
Console.WriteLine("}});");

Console.WriteLine();

// Error handling
Console.WriteLine("// Handle 404 gracefully");
Console.WriteLine("try");
Console.WriteLine("{{");
Console.WriteLine("    var book = await client.Books[\"invalid-isbn\"].GetAsync();");
Console.WriteLine("}}");
Console.WriteLine("catch (ApiException ex) when (ex.ResponseStatusCode == 404)");
Console.WriteLine("{{");
Console.WriteLine("    Console.WriteLine(\"Book not found!\");");
Console.WriteLine("}}");

Console.WriteLine("\n=== Benefits of Kiota ===");
Console.WriteLine();
Console.WriteLine("1. Strongly Typed: All requests and responses have proper types");
Console.WriteLine("2. IntelliSense: IDE shows available endpoints and parameters");
Console.WriteLine("3. Compile-Time Errors: Catch typos before runtime");
Console.WriteLine("4. Auto-Updated: Regenerate when API changes");
Console.WriteLine("5. Fluent API: client.Books[id].GetAsync() is intuitive");
Console.WriteLine("6. Cross-Platform: Same patterns for C#, Python, TypeScript, etc.");
Console.WriteLine("7. Lightweight: Minimal dependencies unlike other generators");
Console.WriteLine();
Console.WriteLine("Compare to manual HttpClient:");
Console.WriteLine("  BEFORE: await http.GetAsync(\"/books/" + isbn);");
Console.WriteLine("  AFTER:  await client.Books[isbn].GetAsync();");