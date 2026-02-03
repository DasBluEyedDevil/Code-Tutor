// C# 12 fallback: Uses ^ operator for reading (available since C# 8)
// but avoids ^ in object initializers (C# 13 feature)

// Define the Countdown class
public class Countdown
{
    public int[] Numbers { get; set; } = new int[5];
}

// Create instance and set values using standard indexing (C# 12 compatible)
var timer = new Countdown();

// Set values using standard indexes
timer.Numbers[0] = 5;  // First position
timer.Numbers[1] = 4;
timer.Numbers[2] = 3;
timer.Numbers[3] = 2;
timer.Numbers[4] = 1;  // Last position

// Display all numbers
Console.WriteLine("Countdown sequence:");
foreach (int num in timer.Numbers)
{
    Console.Write(num + " ");
}
Console.WriteLine();

// Display last number using ^1 (reading with ^ works since C# 8)
Console.WriteLine($"Last number is: {timer.Numbers[^1]}");
