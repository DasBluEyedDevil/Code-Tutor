// Create temperature array
double[] temperatures = new double[7];

// Fill with values
temperatures[0] = 72.5;
temperatures[1] = 75.0;
temperatures[2] = 68.3;
temperatures[3] = 71.2;
temperatures[4] = 74.8;
temperatures[5] = 70.1;
temperatures[6] = 69.5;

// Display each temperature with a loop
for (int i = 0; i < temperatures.Length; i++)
{
    Console.WriteLine("Day " + i + ": " + temperatures[i] + "°F");
}

// Calculate average
double sum = 0;
for (int i = 0; i < temperatures.Length; i++)
{
    sum += temperatures[i];
}
double average = sum / temperatures.Length;
Console.WriteLine("Average: " + average + "°F");