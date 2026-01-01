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

// Print calendar grid
int dayOfWeek = 1;

// Print leading spaces

// Print day numbers

// New line after each week