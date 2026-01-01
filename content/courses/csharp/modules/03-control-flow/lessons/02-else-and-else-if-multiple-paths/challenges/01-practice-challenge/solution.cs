// Create level variable
int level = 2;

// Write your if-else if-else chain here
if (level == 1)
{
    Console.WriteLine("Easy mode selected");
}
else if (level == 2)
{
    Console.WriteLine("Normal mode selected");
}
else if (level == 3)
{
    Console.WriteLine("Hard mode selected");
}
else
{
    Console.WriteLine("Invalid level! Defaulting to Normal.");
}