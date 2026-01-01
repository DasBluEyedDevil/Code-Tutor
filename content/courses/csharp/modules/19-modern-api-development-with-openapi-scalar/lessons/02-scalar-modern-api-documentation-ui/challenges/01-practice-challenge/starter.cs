// TODO: Add using statement for Scalar

var builder = WebApplication.CreateBuilder(args);

// TODO: Add OpenAPI services

var app = builder.Build();

// TODO: Map OpenAPI endpoint

// TODO: Add Scalar UI with configuration:
// - Title: "Movie Database API"
// - Theme: DeepSpace
// - Dark mode: true
// - Default client: Python

var movies = new List<Movie>
{
    new(1, "The Matrix", "Sci-Fi", 1999, 8.7),
    new(2, "Inception", "Sci-Fi", 2010, 8.8),
    new(3, "The Dark Knight", "Action", 2008, 9.0)
};

// TODO: GET /movies endpoint
// - WithName("GetMovies")
// - WithSummary("List all movies")
// - WithDescription("Returns the complete movie catalog")
// - WithTags("Movies")

// TODO: GET /movies/{id} endpoint

// TODO: GET /movies/search endpoint with genre and year query params

// TODO: POST /movies endpoint

// TODO: Print Scalar UI URL

app.Run();

// TODO: Define Movie record
// TODO: Define CreateMovieRequest record