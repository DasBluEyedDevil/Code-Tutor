---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public int Age { get; set; }
}

var users = new List<User>
{
    new User { Id = 1, Name = "Alice", Email = "alice@example.com", Age = 30 }
};

int nextId = 2;

// 200 OK - Standard success
app.MapGet("/api/users", () => 
{
    return Results.Ok(users);  // Explicit 200
    // OR just: return users;  // Implicit 200
});

// 200 OK or 404 Not Found
app.MapGet("/api/users/{id}", (int id) =>
{
    var user = users.FirstOrDefault(u => u.Id == id);
    return user is not null 
        ? Results.Ok(user)        // 200 with data
        : Results.NotFound();     // 404
});

// 201 Created - New resource
app.MapPost("/api/users", (User user) =>
{
    // Validation
    if (string.IsNullOrEmpty(user.Name))
        return Results.BadRequest("Name is required!");  // 400
    
    if (user.Age < 0 || user.Age > 120)
        return Results.BadRequest("Invalid age!");  // 400
    
    user.Id = nextId++;
    users.Add(user);
    
    // 201 with location header and created object
    return Results.Created($"/api/users/{user.Id}", user);
});

// 200 OK or 404 Not Found
app.MapPut("/api/users/{id}", (int id, User updatedUser) =>
{
    var user = users.FirstOrDefault(u => u.Id == id);
    if (user is null) return Results.NotFound();  // 404
    
    // Validation
    if (updatedUser.Age < 0)
        return Results.BadRequest("Age cannot be negative!");  // 400
    
    user.Name = updatedUser.Name;
    user.Email = updatedUser.Email;
    user.Age = updatedUser.Age;
    
    return Results.Ok(user);  // 200 with updated data
});

// 204 No Content or 404 Not Found
app.MapDelete("/api/users/{id}", (int id) =>
{
    var user = users.FirstOrDefault(u => u.Id == id);
    if (user is null) return Results.NotFound();  // 404
    
    users.Remove(user);
    return Results.NoContent();  // 204 - success, no data
});

// Custom status code
app.MapGet("/api/admin", () =>
{
    return Results.StatusCode(403);  // 403 Forbidden
});

app.Run();
```
