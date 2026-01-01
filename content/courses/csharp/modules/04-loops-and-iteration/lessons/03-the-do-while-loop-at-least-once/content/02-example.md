---
type: "EXAMPLE"
title: "Code Example"
---

The do-while loop executes its body FIRST, then checks the condition. This guarantees the code runs at least once, even if the condition is initially false. Notice the required semicolon after the while condition!

```csharp
// Regular while - might not run at all
int x = 10;
while (x < 5)  // False from the start
{
    Console.WriteLine("This never prints!");
}

// Do-while - runs at least once!
int y = 10;
do
{
    Console.WriteLine("This prints once, even though y is not < 5!");
} while (y < 5);  // Condition checked AFTER

// Practical example: Menu system
int choice;
do
{
    Console.WriteLine("\n=== MENU ===");
    Console.WriteLine("1. Play Game");
    Console.WriteLine("2. Settings");
    Console.WriteLine("3. Quit");
    Console.Write("Enter choice: ");
    
    // Simulating user input (in real code: int.Parse(Console.ReadLine()))
    choice = 3; // Pretend user chose quit
    
    switch (choice)
    {
        case 1:
            Console.WriteLine("Starting game...");
            break;
        case 2:
            Console.WriteLine("Opening settings...");
            break;
        case 3:
            Console.WriteLine("Goodbye!");
            break;
        default:
            Console.WriteLine("Invalid choice!");
            break;
    }
    
} while (choice != 3);

Console.WriteLine("Thanks for playing!");

// Input validation pattern - keep asking until valid
int age;
bool isValid;
do
{
    Console.Write("Enter your age (1-120): ");
    age = 25;  // Simulating input
    isValid = age >= 1 && age <= 120;
    
    if (!isValid)
    {
        Console.WriteLine("Invalid age! Try again.");
    }
} while (!isValid);

Console.WriteLine($"Your age is {age}");
```
