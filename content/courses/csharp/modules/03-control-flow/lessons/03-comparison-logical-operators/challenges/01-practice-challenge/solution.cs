// Set up variables
int age = 16;
bool hasParent = true;
bool hasTicket = true;

// Write your if-else chain with logical operators
if (age >= 18 && hasTicket)
{
    Console.WriteLine("Welcome! Enjoy the movie.");
}
else if (age < 18 && hasParent && hasTicket)
{
    Console.WriteLine("Welcome with your parent!");
}
else if (!hasTicket)
{
    Console.WriteLine("Please purchase a ticket first.");
}
else
{
    Console.WriteLine("Sorry, you need a parent to attend.");
}