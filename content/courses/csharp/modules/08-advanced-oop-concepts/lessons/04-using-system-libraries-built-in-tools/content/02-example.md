---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

// COLLECTIONS (System.Collections.Generic)
List<string> names = new List<string>();
names.Add("Alice");
names.Add("Bob");

Dictionary<string, int> ages = new Dictionary<string, int>();
ages["Alice"] = 30;
ages["Bob"] = 25;

// FILE I/O (System.IO)
string path = "test.txt";
File.WriteAllText(path, "Hello from C#!");
string content = File.ReadAllText(path);
Console.WriteLine(content);

if (File.Exists(path))
{
    Console.WriteLine("File exists!");
}

// STRING MANIPULATION (System.Text)
StringBuilder sb = new StringBuilder();
sb.Append("Hello");
sb.Append(" ");
sb.Append("World");
string result = sb.ToString();
Console.WriteLine(result);

// DATE/TIME (System)
DateTime now = DateTime.Now;
Console.WriteLine("Current time: " + now);

DateTime birthday = new DateTime(1990, 5, 15);
TimeSpan age = now - birthday;
Console.WriteLine("Days old: " + age.TotalDays);

// MATH (System)
double squareRoot = Math.Sqrt(16);
double power = Math.Pow(2, 8);  // 2^8
int max = Math.Max(10, 20);
Console.WriteLine("Max: " + max);
```
