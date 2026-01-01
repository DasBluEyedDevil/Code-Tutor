// Variables
int health = 75;
int energy = 30;

// Use ternary operators to create status strings
string healthStatus = (health > 50) ? "Healthy" : "Injured";
string energyStatus = (energy > 50) ? "Energized" : "Tired";

// Display the statuses
Console.WriteLine("Health: " + healthStatus);
Console.WriteLine("Energy: " + energyStatus);