// Calendar Printer

Console.WriteLine("Enter month name:");
string month = Console.ReadLine();

Console.WriteLine("Enter number of days:");
int daysInMonth = int.Parse(Console.ReadLine());

Console.WriteLine("What day does month start? (1=Mon, 7=Sun):");
int startDay = int.Parse(Console.ReadLine());

// Print header
Console.WriteLine($"\n     {month} 2025");
Console.WriteLine("Mon Tue Wed Thu Fri Sat Sun");

// Print leading spaces for offset
for (int i = 1; i < startDay; i++)
{
    Console.Write("    ");  // 4 spaces for empty day
}

int currentDayOfWeek = startDay;

// Print each day of month
for (int day = 1; day <= daysInMonth; day++)
{
    Console.Write($"{day,4}");  // Right-aligned in 4 spaces
    
    // Check if end of week (Sunday = 7)
    if (currentDayOfWeek == 7)
    {
        Console.WriteLine();  // New line for next week
        currentDayOfWeek = 1;  // Reset to Monday
    }
    else
    {
        currentDayOfWeek++;
    }
}

Console.WriteLine();  // Final newline