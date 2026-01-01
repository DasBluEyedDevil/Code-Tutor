using System.Collections.Generic;

// Create inventory
List<string> inventory = new List<string>();

// Add items
inventory.Add("Sword");
inventory.Add("Shield");
inventory.Add("Potion");

// Display count
Console.WriteLine("Items in inventory: " + inventory.Count);

// Check for potion
if (inventory.Contains("Potion"))
{
    Console.WriteLine("Potion ready!");
}

// Remove potion
inventory.Remove("Potion");

// Add gold coin
inventory.Add("Gold Coin");

// Display all items
for (int i = 0; i < inventory.Count; i++)
{
    Console.WriteLine("Item: " + inventory[i]);
}