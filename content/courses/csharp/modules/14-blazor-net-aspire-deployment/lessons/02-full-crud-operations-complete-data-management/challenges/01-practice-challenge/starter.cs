// API
app.MapGet("/api/books", async (AppDbContext db) => {
    // Return all books
});

// Add other CRUD endpoints...

// Blazor - BookManager.razor
@inject HttpClient Http

<h3>Book Library</h3>

@if (showForm)
{
    <EditForm Model="@currentBook" OnValidSubmit="SaveBook">
        <!-- Form fields -->
    </EditForm>
}

<!-- Table of books -->

@code {
    private List<Book> books = new();
    private Book currentBook = new();
    private bool showForm = false;
    private bool isEditing = false;
}