---
type: "EXAMPLE"
title: "Code Example"
---

C# 13 extends params to work with any collection type, not just arrays. This gives you flexible calling patterns.

```csharp
// C# 13: params works with IEnumerable<T>, Span<T>, and more!
void PrintAll(params IEnumerable<string> items)
{
    foreach (var item in items)
        Console.WriteLine(item);
}

// Three ways to call the same method:
PrintAll("apple", "banana", "cherry");     // Inline items
PrintAll(["one", "two", "three"]);         // Collection expression

var myList = new List<string> { "red", "green", "blue" };
PrintAll(myList);                           // Existing collection

// Works with Span<T> for performance!
void ProcessFast(params ReadOnlySpan<int> numbers)
{
    var sum = 0;
    foreach (var n in numbers)
        sum += n;
    Console.WriteLine($"Sum: {sum}");
}

ProcessFast(1, 2, 3, 4, 5);  // No allocation!
```
