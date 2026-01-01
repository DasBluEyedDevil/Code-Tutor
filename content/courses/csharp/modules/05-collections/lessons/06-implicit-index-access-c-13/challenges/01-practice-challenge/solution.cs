// Define the Countdown class
public class Countdown
{
    public int[] Numbers { get; set; } = new int[5];
}

// Create instance using ^ operator in initializer
var timer = new Countdown
{
    Numbers =
    {
        [^5] = 5,  // First position
        [^4] = 4,
        [^3] = 3,
        [^2] = 2,
        [^1] = 1   // Last position
    }
};

// Display all numbers
Console.WriteLine("Countdown sequence:");
foreach (int num in timer.Numbers)
{
    Console.Write(num + " ");
}
Console.WriteLine();

// Display last number using ^1
Console.WriteLine($"Last number is: {timer.Numbers[^1]}");