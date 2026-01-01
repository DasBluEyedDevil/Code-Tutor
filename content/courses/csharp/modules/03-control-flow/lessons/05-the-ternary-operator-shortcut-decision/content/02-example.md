---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// Regular if-else (verbose)
int age = 20;
string status;

if (age >= 18)
{
    status = "Adult";
}
else
{
    status = "Minor";
}
Console.WriteLine(status);

// Ternary operator (concise!)
int age2 = 20;
string status2 = (age2 >= 18) ? "Adult" : "Minor";
Console.WriteLine(status2);

// Inline usage
int score = 85;
Console.WriteLine("Result: " + (score >= 60 ? "Pass" : "Fail"));

// Multiple ternaries (careful - can get confusing!)
int grade = 85;
string letter = (grade >= 90) ? "A" : 
                (grade >= 80) ? "B" : 
                (grade >= 70) ? "C" : "F";
Console.WriteLine("Grade: " + letter);
```
