using System;
using System.IO;
using System.Text;

string filePath = "journal.txt";

StringBuilder entry = new StringBuilder();
entry.AppendLine("=== Journal Entry ===");
entry.AppendLine("Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

Console.WriteLine("How are you feeling?");
string mood = Console.ReadLine();
entry.AppendLine("Mood: " + mood);

Console.WriteLine("What's on your mind?");
string thoughts = Console.ReadLine();
entry.AppendLine("Entry: " + thoughts);
entry.AppendLine("");

File.AppendAllText(filePath, entry.ToString());
Console.WriteLine("Journal entry saved!");

if (File.Exists(filePath))
{
    Console.WriteLine("\nAll entries:");
    string allEntries = File.ReadAllText(filePath);
    Console.WriteLine(allEntries);
}

DateTime newYear = new DateTime(DateTime.Now.Year + 1, 1, 1);
TimeSpan timeUntil = newYear - DateTime.Now;
Console.WriteLine("Days until New Year: " + (int)timeUntil.TotalDays);