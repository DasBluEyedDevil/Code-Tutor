using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add OpenAPI services
builder.Services.AddOpenApi();

var app = builder.Build();

// Map OpenAPI endpoint
app.MapOpenApi();

// Add Scalar UI with custom configuration
app.MapScalarApiReference(options =>
{
    options
        .WithTitle("Movie Database API")
        .WithTheme(ScalarTheme.DeepSpace)
        .WithDarkMode(true)
        .WithDefaultHttpClient(ScalarTarget.Python, ScalarClient.Requests);
});

var movies = new List<Movie>
{
    new(1, "The Matrix", "Sci-Fi", 1999, 8.7),
    new(2, "Inception", "Sci-Fi", 2010, 8.8),
    new(3, "The Dark Knight", "Action", 2008, 9.0)
};

// GET /movies - List all movies
app.MapGet("/movies", () => movies)
    .WithName("GetMovies")
    .WithSummary("List all movies")
    .WithDescription("Returns the complete movie catalog with all available films")
    .WithTags("Movies")
    .Produces<List<Movie>>(StatusCodes.Status200OK);

// GET /movies/{id} - Get movie by ID
app.MapGet("/movies/{id}", (int id) =>
{
    var movie = movies.FirstOrDefault(m => m.Id == id);
    return movie is not null
        ? Results.Ok(movie)
        : Results.NotFound($"Movie with ID {id} not found");
})
    .WithName("GetMovieById")
    .WithSummary("Get movie by ID")
    .WithDescription("Returns a specific movie by its unique identifier")
    .WithTags("Movies")
    .Produces<Movie>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound);

// GET /movies/search - Search movies
app.MapGet("/movies/search", (string? genre, int? year) =>
{
    var results = movies.AsEnumerable();
    
    if (!string.IsNullOrEmpty(genre))
        results = results.Where(m => m.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));
    if (year.HasValue)
        results = results.Where(m => m.Year == year);
    
    return results.ToList();
})
    .WithName("SearchMovies")
    .WithSummary("Search movies")
    .WithDescription("Search movies by genre and/or release year")
    .WithTags("Movies")
    .Produces<List<Movie>>(StatusCodes.Status200OK);

// POST /movies - Add new movie
app.MapPost("/movies", (CreateMovieRequest request) =>
{
    var movie = new Movie(
        movies.Max(m => m.Id) + 1,
        request.Title,
        request.Genre,
        request.Year,
        request.Rating
    );
    movies.Add(movie);
    return Results.Created($"/movies/{movie.Id}", movie);
})
    .WithName("CreateMovie")
    .WithSummary("Add new movie")
    .WithDescription("Adds a new movie to the database")
    .WithTags("Movies")
    .Accepts<CreateMovieRequest>("application/json")
    .Produces<Movie>(StatusCodes.Status201Created);

// Print Scalar UI URL
Console.WriteLine("Scalar API Documentation: http://localhost:5000/scalar/v1");

app.Run();

public record Movie(int Id, string Title, string Genre, int Year, double Rating);

public record CreateMovieRequest(string Title, string Genre, int Year, double Rating);