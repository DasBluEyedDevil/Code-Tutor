---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// WITHOUT exception handling - program crashes!
string input = "abc";
int number = int.Parse(input);  // CRASH! Can't convert "abc" to number
Console.WriteLine("This never runs");

// WITH exception handling - graceful recovery
try
{
    string userInput = "abc";
    int num = int.Parse(userInput);  // This fails...
    Console.WriteLine("Success: " + num);  // Never reached
}
catch (FormatException ex)
{
    Console.WriteLine("Error: That's not a valid number!");
    Console.WriteLine("Please enter digits only.");
}
Console.WriteLine("Program continues running!");

// Multiple catch blocks for different errors
try
{
    int[] numbers = { 1, 2, 3 };
    Console.WriteLine(numbers[10]);  // Index out of range!
}
catch (IndexOutOfRangeException ex)
{
    Console.WriteLine("Error: Array index too large!");
}
catch (Exception ex)  // Generic catch for any other error
{
    Console.WriteLine("Something went wrong: " + ex.Message);
}

// EXCEPTION FILTERS with 'when' clause (C# 6+)
// The catch block ONLY runs if the condition is true!
try
{
    throw new HttpRequestException("Not Found", null, System.Net.HttpStatusCode.NotFound);
}
catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
{
    Console.WriteLine("Page not found (404)");  // Specific handling
}
catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
{
    Console.WriteLine("Access denied (401)");  // Different handling
}
catch (HttpRequestException ex)
{
    Console.WriteLine("HTTP error: " + ex.Message);  // Fallback
}

// 'when' filters preserve the original stack trace!
// The filter is evaluated BEFORE the stack unwinds
```
