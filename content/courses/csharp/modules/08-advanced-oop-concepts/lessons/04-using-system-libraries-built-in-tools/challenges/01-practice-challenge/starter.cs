using System;
using System.IO;
using System.Text;

string filePath = "journal.txt";

// Build journal entry with StringBuilder
StringBuilder entry = new StringBuilder();

// Add current date/time

// Ask for mood and journal text
Console.WriteLine("How are you feeling?");
string mood = Console.ReadLine();

Console.WriteLine("What's on your mind?");
string thoughts = Console.ReadLine();

// Build entry

// Append to file

// Read all entries

// Calculate days until New Year