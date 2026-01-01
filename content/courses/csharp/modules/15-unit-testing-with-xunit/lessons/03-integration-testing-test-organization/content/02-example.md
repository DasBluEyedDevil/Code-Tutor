---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// ===== PROJECT STRUCTURE =====
// src/
//   MyApp/
//     Services/
//       UserService.cs
//     Data/
//       AppDbContext.cs
// tests/
//   MyApp.Tests/
//     Unit/
//       UserServiceTests.cs
//     Integration/
//       UserServiceIntegrationTests.cs

using Xunit;
using Microsoft.EntityFrameworkCore;

// ===== THE DBCONTEXT =====
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options) { }
    
    public DbSet<User> Users { get; set; }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

public class UserService
{
    private readonly AppDbContext _context;
    
    public UserService(AppDbContext context)
    {
        _context = context;
    }
    
    public User CreateUser(string name, string email)
    {
        var user = new User { Name = name, Email = email };
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }
    
    public User GetByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);
    }
}

// ===== INTEGRATION TESTS with In-Memory Database =====
public class UserServiceIntegrationTests : IDisposable
{
    private readonly AppDbContext _context;
    private readonly UserService _service;
    
    public UserServiceIntegrationTests()
    {
        // Use in-memory database for tests
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        _context = new AppDbContext(options);
        _service = new UserService(_context);
    }
    
    [Fact]
    public void CreateUser_ValidData_PersistsToDatabase()
    {
        // Act
        var user = _service.CreateUser("John", "john@test.com");
        
        // Assert - Actually query the database!
        var dbUser = _context.Users.Find(user.Id);
        Assert.NotNull(dbUser);
        Assert.Equal("John", dbUser.Name);
    }
    
    [Fact]
    public void GetByEmail_UserExists_ReturnsUser()
    {
        // Arrange - Create user first
        _service.CreateUser("Jane", "jane@test.com");
        
        // Act
        var found = _service.GetByEmail("jane@test.com");
        
        // Assert
        Assert.NotNull(found);
        Assert.Equal("Jane", found.Name);
    }
    
    [Fact]
    public void GetByEmail_UserNotExists_ReturnsNull()
    {
        var found = _service.GetByEmail("nobody@test.com");
        Assert.Null(found);
    }
    
    // IDisposable - cleanup after each test
    public void Dispose()
    {
        _context.Dispose();
    }
}

// ===== TEST ORGANIZATION TIPS =====
// 1. Mirror source structure in test project
// 2. One test class per class being tested
// 3. Use folders: Unit/, Integration/, E2E/
// 4. Naming: [ClassName]Tests.cs
// 5. Use IClassFixture<T> for shared setup across tests

Console.WriteLine("Integration test patterns defined!");
Console.WriteLine("Key: Use in-memory database for EF Core integration tests");
Console.WriteLine("IDisposable ensures cleanup between tests");
```
