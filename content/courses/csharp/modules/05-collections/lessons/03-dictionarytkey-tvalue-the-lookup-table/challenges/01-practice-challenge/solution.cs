using System.Collections.Generic;

// Create translator dictionary
Dictionary<string, string> translator = new Dictionary<string, string>();

// Add translations
translator.Add("hello", "hola");
translator.Add("goodbye", "adiós");
translator.Add("friend", "amigo");

// Look up 'hello'
Console.WriteLine("hello: " + translator["hello"]);

// Check and display 'goodbye'
if (translator.ContainsKey("goodbye"))
{
    Console.WriteLine("goodbye: " + translator["goodbye"]);
}

// Add 'water'
translator.Add("water", "agua");

// Display all translations
foreach (var pair in translator)
{
    Console.WriteLine(pair.Key + ": " + pair.Value);
}