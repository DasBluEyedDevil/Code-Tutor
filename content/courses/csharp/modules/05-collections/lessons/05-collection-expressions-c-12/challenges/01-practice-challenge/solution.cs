// Create rock songs array
string[] rockSongs = ["Bohemian Rhapsody", "Stairway to Heaven", "Back in Black"];

// Create pop songs array
string[] popSongs = ["Billie Jean", "Like a Prayer", "Shake It Off"];

// Combine all songs with spread and add bonus
string[] allSongs = [..rockSongs, ..popSongs, "Bonus Track"];

// Create empty favorites list
List<string> favorites = [];

// Display all songs
Console.WriteLine("All Songs:");
foreach (string song in allSongs)
{
    Console.WriteLine("  - " + song);
}

Console.WriteLine($"Total: {allSongs.Length} songs");
Console.WriteLine($"Favorites: {favorites.Count} songs");