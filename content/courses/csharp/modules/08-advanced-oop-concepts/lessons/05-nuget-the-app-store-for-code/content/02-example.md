---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// ========== NUGET CLI COMMANDS ==========
// Run these in terminal (in your project folder):

// Install a package (latest version)
// dotnet add package Humanizer

// Install specific version
// dotnet add package Newtonsoft.Json --version 13.0.3

// List installed packages
// dotnet list package

// Check for outdated packages
// dotnet list package --outdated

// Remove a package
// dotnet remove package Humanizer

// Restore all packages (after cloning)
// dotnet restore

// ========== USING PACKAGES IN CODE ==========
using System;
using System.Text.Json;  // Built-in JSON library (modern .NET)
using Humanizer;  // Third-party: makes things human-readable

// JSON SERIALIZATION with System.Text.Json (BUILT-IN - no install!)
class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}

Person person = new Person 
{ 
    Name = "Alice", 
    Age = 30, 
    Email = "alice@example.com" 
};

// Convert object to JSON string
var options = new JsonSerializerOptions 
{ 
    WriteIndented = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase  // JSON uses camelCase
};
string json = JsonSerializer.Serialize(person, options);
Console.WriteLine("JSON:\n" + json);

// Convert JSON string back to object
string jsonData = "{\"name\":\"Bob\",\"age\":25}";
Person restored = JsonSerializer.Deserialize<Person>(jsonData, options)!;
Console.WriteLine("Name: " + restored.Name);

// HUMANIZER library (requires: dotnet add package Humanizer)
DateTime past = DateTime.Now.AddDays(-5);
Console.WriteLine(past.Humanize());  // "5 days ago"
Console.WriteLine("NoOfDonuts".Humanize());  // "No of donuts"
```
