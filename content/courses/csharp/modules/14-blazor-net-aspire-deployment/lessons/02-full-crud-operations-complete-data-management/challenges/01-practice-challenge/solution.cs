Console.WriteLine(@"
=== FULL CRUD API ===");
Console.WriteLine(@"
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();

class Book {
    public int Id { get; set; }
    public string Title { get; set; } = """";
    public string Author { get; set; } = """";
    public int Year { get; set; }
    public string ISBN { get; set; } = """";
}

// CREATE
app.MapPost(""/api/books"", async (Book book, AppDbContext db) => {
    db.Books.Add(book);
    await db.SaveChangesAsync();
    return Results.Created($""/api/books/{book.Id}"", book);
});

// READ - All
app.MapGet(""/api/books"", async (AppDbContext db) =>
    await db.Books.ToListAsync());

// READ - Single
app.MapGet(""/api/books/{id}"", async (int id, AppDbContext db) => {
    var book = await db.Books.FindAsync(id);
    return book is not null ? Results.Ok(book) : Results.NotFound();
});

// UPDATE
app.MapPut(""/api/books/{id}"", async (int id, Book updated, AppDbContext db) => {
    var book = await db.Books.FindAsync(id);
    if (book is null) return Results.NotFound();
    
    book.Title = updated.Title;
    book.Author = updated.Author;
    book.Year = updated.Year;
    book.ISBN = updated.ISBN;
    
    await db.SaveChangesAsync();
    return Results.Ok(book);
});

// DELETE
app.MapDelete(""/api/books/{id}"", async (int id, AppDbContext db) => {
    var book = await db.Books.FindAsync(id);
    if (book is null) return Results.NotFound();
    
    db.Books.Remove(book);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
"");

Console.WriteLine(@"
=== BLAZOR CRUD COMPONENT ===");
Console.WriteLine(@"
// BookManager.razor
@inject HttpClient Http

<div class=""container"">
    <h3>📚 Book Library Manager</h3>
    
    <button class=""btn btn-primary"" @onclick=""ShowCreateForm"">➕ Add New Book</button>
    
    @if (showForm)
    {
        <div class=""card mt-3"">
            <div class=""card-body"">
                <h5>@(isEditing ? ""Edit Book"" : ""Add New Book"")</h5>
                <EditForm Model=""@currentBook"" OnValidSubmit=""SaveBook"">
                    <div class=""mb-2"">
                        <label>Title:</label>
                        <InputText @bind-Value=""currentBook.Title"" class=""form-control"" />
                    </div>
                    <div class=""mb-2"">
                        <label>Author:</label>
                        <InputText @bind-Value=""currentBook.Author"" class=""form-control"" />
                    </div>
                    <div class=""mb-2"">
                        <label>Year:</label>
                        <InputNumber @bind-Value=""currentBook.Year"" class=""form-control"" />
                    </div>
                    <div class=""mb-2"">
                        <label>ISBN:</label>
                        <InputText @bind-Value=""currentBook.ISBN"" class=""form-control"" />
                    </div>
                    <button type=""submit"" class=""btn btn-success"">💾 Save</button>
                    <button type=""button"" class=""btn btn-secondary"" @onclick=""CancelForm"">Cancel</button>
                </EditForm>
            </div>
        </div>
    }
    
    <table class=""table table-striped mt-3"">
        <thead>
            <tr>
                <th>Title</th>
                <th>Author</th>
                <th>Year</th>
                <th>ISBN</th>
                <th>Actions</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var book in books)
            {
                <tr>
                    <td>@book.Title</td>
                    <td>@book.Author</td>
                    <td>@book.Year</td>
                    <td>@book.ISBN</td>
                    <td>
                        <button class=""btn btn-sm btn-warning"" @onclick=""() => EditBook(book)"">✏️ Edit</button>
                        <button class=""btn btn-sm btn-danger"" @onclick=""() => DeleteBook(book.Id)"">🗑️ Delete</button>
                    </td>
                </tr>
            }
        </tbody>
    </table>
    
    <p><strong>Total Books: @books.Count</strong></p>
</div>

@code {
    private class Book {
        public int Id { get; set; }
        public string Title { get; set; } = """";
        public string Author { get; set; } = """";
        public int Year { get; set; }
        public string ISBN { get; set; } = """";
    }
    
    private List<Book> books = new();
    private Book currentBook = new();
    private bool showForm = false;
    private bool isEditing = false;
    
    protected override async Task OnInitializedAsync() {
        await LoadBooks();
    }
    
    private async Task LoadBooks() {
        books = await Http.GetFromJsonAsync<List<Book>>(""https://localhost:5001/api/books"") ?? new();
    }
    
    private void ShowCreateForm() {
        currentBook = new Book();
        isEditing = false;
        showForm = true;
    }
    
    private void EditBook(Book book) {
        currentBook = new Book {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Year = book.Year,
            ISBN = book.ISBN
        };
        isEditing = true;
        showForm = true;
    }
    
    private async Task SaveBook() {
        if (isEditing) {
            await Http.PutAsJsonAsync($""https://localhost:5001/api/books/{currentBook.Id}"", currentBook);
        } else {
            await Http.PostAsJsonAsync(""https://localhost:5001/api/books"", currentBook);
        }
        
        showForm = false;
        await LoadBooks();
    }
    
    private async Task DeleteBook(int id) {
        if (confirm(\""Delete this book?\"")) {
            await Http.DeleteAsync($""https://localhost:5001/api/books/{id}"");
            await LoadBooks();
        }
    }
    
    private void CancelForm() {
        showForm = false;
    }
}
"");

Console.WriteLine(@"
=== CRUD COMPLETE! ===");
Console.WriteLine("✓ CREATE: POST with form data");
Console.WriteLine("✓ READ: GET for list and details");
Console.WriteLine("✓ UPDATE: PUT with modified data");
Console.WriteLine("✓ DELETE: DELETE by ID");
Console.WriteLine("\nYou've built a complete data management system!");