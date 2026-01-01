// Number analyzer
for (int i = 1; i <= 20; i++)
{
    // Skip even numbers
    if (i % 2 == 0)
    {
        continue;
    }
    
    // Break at 15
    if (i == 15)
    {
        break;
    }
    
    // Display the number
    Console.WriteLine(i);
}