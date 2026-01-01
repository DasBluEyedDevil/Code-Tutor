// Starting stats
int level = 1;
int experience = 0;
int coins = 100;

Console.WriteLine("Starting - Level: " + level + ", XP: " + experience + ", Coins: " + coins);

// Gain 50 experience
experience += 50;
Console.WriteLine("Gained XP! - Level: " + level + ", XP: " + experience + ", Coins: " + coins);

// Level up
level++;
Console.WriteLine("Level up! - Level: " + level + ", XP: " + experience + ", Coins: " + coins);

// Double coins
coins *= 2;
Console.WriteLine("Coins doubled! - Level: " + level + ", XP: " + experience + ", Coins: " + coins);

// Spend 25 coins
coins -= 25;
Console.WriteLine("Spent coins - Level: " + level + ", XP: " + experience + ", Coins: " + coins);