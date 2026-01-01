---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the C# 13 implicit index access using the ^ operator in object initializers.

```csharp
// C# 13: Implicit 'from end' index (^) in object initializers

// The ^ operator counts from the end:
// ^1 = last element, ^2 = second-to-last, etc.

// Example: Creating a countdown timer display
public class TimerDisplay
{
    public int[] Digits { get; set; } = new int[10];
}

// Before C# 13: You couldn't use ^ in object initializers!
// Now you CAN use the 'from end' index operator:
var countdown = new TimerDisplay
{
    Digits =
    {
        [^1] = 0,   // Last position = 0
        [^2] = 1,   // Second-to-last = 1
        [^3] = 2,   // Third-to-last = 2
        [^4] = 3,
        [^5] = 4,
        [^6] = 5,
        [^7] = 6,
        [^8] = 7,
        [^9] = 8,
        [^10] = 9   // First position = 9
    }
};

// The array now contains: [9, 8, 7, 6, 5, 4, 3, 2, 1, 0]
Console.WriteLine("Countdown: " + string.Join(", ", countdown.Digits));

// Another example: Buffer with values at both ends
public class Buffer
{
    public byte[] Data { get; } = new byte[256];
}

var buffer = new Buffer
{
    Data =
    {
        [0] = 0xAA,    // First byte (header)
        [1] = 0xBB,    // Second byte
        [^1] = 0xFF,   // Last byte (footer)
        [^2] = 0xFE    // Second-to-last
    }
};

Console.WriteLine($"First: 0x{buffer.Data[0]:X2}");   // 0xAA
Console.WriteLine($"Last: 0x{buffer.Data[^1]:X2}");   // 0xFF

// The ^ operator works with the Index type
Index lastIndex = ^1;
Console.WriteLine($"Using Index: {buffer.Data[lastIndex]:X2}");
```
