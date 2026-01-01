using System.Net;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopFlow.Application.Auth.DTOs;
using ShopFlow.Infrastructure.Persistence;

namespace ShopFlow.Tests.Integration;

/// <summary>
/// Custom WebApplicationFactory for Auth API tests.
/// Uses InMemory database and configures JWT settings.
/// </summary>
public class AuthApiTestFactory : WebApplicationFactory<Program>
{
    private readonly string _dbName = "TestDb_Auth_" + Guid.NewGuid().ToString();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            // Remove ALL EF Core related registrations
            var descriptorsToRemove = services
                .Where(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>) ||
                            d.ServiceType == typeof(DbContextOptions) ||
                            d.ServiceType == typeof(AppDbContext) ||
                            d.ServiceType.FullName?.Contains("EntityFrameworkCore") == true ||
                            d.ServiceType.FullName?.Contains("DbContext") == true)
                .ToList();

            foreach (var descriptor in descriptorsToRemove)
            {
                services.Remove(descriptor);
            }

            // Add InMemory database
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase(_dbName);
            });
        });
    }
}

public class AuthApiTests : IClassFixture<AuthApiTestFactory>, IDisposable
{
    private readonly AuthApiTestFactory _factory;
    private readonly HttpClient _client;
    private readonly IServiceScope _scope;
    private readonly AppDbContext _context;

    public AuthApiTests(AuthApiTestFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
        _scope = _factory.Services.CreateScope();
        _context = _scope.ServiceProvider.GetRequiredService<AppDbContext>();
        _context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        _client.Dispose();
        _scope.Dispose();
    }

    private async Task<AuthResultDto> RegisterUserAsync(string email = "test@example.com", string password = "Password123!")
    {
        var request = new
        {
            Email = email,
            Password = password,
            FirstName = "Test",
            LastName = "User"
        };

        var response = await _client.PostAsJsonAsync("/api/auth/register", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AuthResultDto>()
            ?? throw new Exception("Failed to deserialize auth result");
    }

    #region POST /api/auth/register

    [Fact]
    public async Task Register_WithValidData_ReturnsCreated()
    {
        // Arrange
        var uniqueEmail = $"test_{Guid.NewGuid()}@example.com";
        var request = new
        {
            Email = uniqueEmail,
            Password = "Password123!",
            FirstName = "John",
            LastName = "Doe"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<AuthResultDto>();
        Assert.NotNull(result);
        Assert.NotEmpty(result.AccessToken);
        Assert.NotEmpty(result.RefreshToken);
        Assert.Equal(uniqueEmail, result.User.Email);
        Assert.Equal("John", result.User.FirstName);
        Assert.Equal("Doe", result.User.LastName);
    }

    [Fact]
    public async Task Register_WithDuplicateEmail_ReturnsBadRequest()
    {
        // Arrange
        var uniqueEmail = $"duplicate_{Guid.NewGuid()}@example.com";
        await RegisterUserAsync(uniqueEmail);

        var request = new
        {
            Email = uniqueEmail,
            Password = "Password123!",
            FirstName = "Another",
            LastName = "User"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData("short")]
    [InlineData("nouppercase1")]
    [InlineData("NOLOWERCASE1")]
    [InlineData("NoDigitsHere")]
    public async Task Register_WithWeakPassword_ReturnsBadRequest(string weakPassword)
    {
        // Arrange
        var request = new
        {
            Email = $"weak_{Guid.NewGuid()}@example.com",
            Password = weakPassword,
            FirstName = "John",
            LastName = "Doe"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Register_WithInvalidEmail_ReturnsBadRequest()
    {
        // Arrange
        var request = new
        {
            Email = "invalid-email",
            Password = "Password123!",
            FirstName = "John",
            LastName = "Doe"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    #endregion

    #region POST /api/auth/login

    [Fact]
    public async Task Login_WithValidCredentials_ReturnsOk()
    {
        // Arrange
        var uniqueEmail = $"login_{Guid.NewGuid()}@example.com";
        await RegisterUserAsync(uniqueEmail);

        var request = new
        {
            Email = uniqueEmail,
            Password = "Password123!"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/login", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<AuthResultDto>();
        Assert.NotNull(result);
        Assert.NotEmpty(result.AccessToken);
        Assert.NotEmpty(result.RefreshToken);
    }

    [Fact]
    public async Task Login_WithInvalidEmail_ReturnsBadRequest()
    {
        // Arrange
        var request = new
        {
            Email = "nonexistent@example.com",
            Password = "Password123!"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/login", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Login_WithWrongPassword_ReturnsBadRequest()
    {
        // Arrange
        var uniqueEmail = $"wrongpwd_{Guid.NewGuid()}@example.com";
        await RegisterUserAsync(uniqueEmail);

        var request = new
        {
            Email = uniqueEmail,
            Password = "WrongPassword123!"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/login", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    #endregion

    #region POST /api/auth/refresh

    [Fact]
    public async Task RefreshToken_WithValidToken_ReturnsNewTokens()
    {
        // Arrange
        var uniqueEmail = $"refresh_{Guid.NewGuid()}@example.com";
        var authResult = await RegisterUserAsync(uniqueEmail);

        var request = new
        {
            RefreshToken = authResult.RefreshToken
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/refresh", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<TokenRefreshResultDto>();
        Assert.NotNull(result);
        Assert.NotEmpty(result.AccessToken);
        Assert.NotEmpty(result.RefreshToken);
        Assert.NotEqual(authResult.AccessToken, result.AccessToken);
        Assert.NotEqual(authResult.RefreshToken, result.RefreshToken);
    }

    [Fact]
    public async Task RefreshToken_WithInvalidToken_ReturnsUnauthorized()
    {
        // Arrange
        var request = new
        {
            RefreshToken = "invalidToken123"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/refresh", request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task RefreshToken_WithRevokedToken_ReturnsUnauthorized()
    {
        // Arrange
        var uniqueEmail = $"revoked_{Guid.NewGuid()}@example.com";
        var authResult = await RegisterUserAsync(uniqueEmail);

        // First, logout to revoke the token
        await _client.PostAsJsonAsync("/api/auth/logout", new { RefreshToken = authResult.RefreshToken });

        // Then try to use the revoked token
        var request = new
        {
            RefreshToken = authResult.RefreshToken
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/refresh", request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    #endregion

    #region POST /api/auth/logout

    [Fact]
    public async Task Logout_WithValidToken_ReturnsNoContent()
    {
        // Arrange
        var uniqueEmail = $"logout_{Guid.NewGuid()}@example.com";
        var authResult = await RegisterUserAsync(uniqueEmail);

        var request = new
        {
            RefreshToken = authResult.RefreshToken
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/logout", request);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Logout_WithInvalidToken_ReturnsNoContent()
    {
        // Arrange - Logout should succeed even with invalid token
        var request = new
        {
            RefreshToken = "invalidToken123"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/logout", request);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    #endregion

    #region Protected Endpoints Tests

    [Fact]
    public async Task Cart_WithoutToken_ReturnsUnauthorized()
    {
        // Act
        var response = await _client.GetAsync("/api/carts/user1");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Cart_WithValidToken_ReturnsOk()
    {
        // Arrange
        var uniqueEmail = $"cartauth_{Guid.NewGuid()}@example.com";
        var authResult = await RegisterUserAsync(uniqueEmail);

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", authResult.AccessToken);

        // Act
        var response = await _client.GetAsync("/api/carts/user1");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Order_WithoutToken_ReturnsUnauthorized()
    {
        // Act
        var response = await _client.GetAsync("/api/users/user1/orders");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Products_WithoutToken_ReturnsOk()
    {
        // Act - Products should be publicly accessible
        var response = await _client.GetAsync("/api/products");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    #endregion
}
