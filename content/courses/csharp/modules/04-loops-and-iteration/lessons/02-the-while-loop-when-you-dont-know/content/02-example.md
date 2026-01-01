---
type: "EXAMPLE"
title: "Code Example"
---

The while loop checks its condition BEFORE each iteration. If the condition is true, the loop body executes. Something inside the loop must eventually make the condition false, or you get an infinite loop!

```csharp
// Simple while loop
int count = 1;

while (count <= 5)
{
    Console.WriteLine($"Count is: {count}");
    count++;  // CRITICAL! Must change count or loop never ends!
}

// User input validation (keep asking until valid)
bool validInput = false;
int userAge = 0;

// Simulating user input
userAge = 15; // Pretend user entered this

while (!validInput)
{
    if (userAge >= 18)
    {
        Console.WriteLine("Valid age!");
        validInput = true;
    }
    else
    {
        Console.WriteLine("You must be 18 or older.");
        validInput = true; // For this example, we stop after one check
    }
}

// Countdown with while
int countdown = 5;
while (countdown > 0)
{
    Console.WriteLine(countdown);
    countdown--;
}
Console.WriteLine("Done!");

// Real-world: Processing until a sentinel value
int[] data = { 5, 10, 15, -1 };  // -1 is sentinel (end marker)
int index = 0;

while (data[index] != -1)
{
    Console.WriteLine($"Processing: {data[index]}");
    index++;
}
```
