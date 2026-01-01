// Required field (non-nullable)
string username = "CSharpDev2024";

// Optional fields (nullable)
string? bio = null;
string? website = "https://example.com";

// Display username (always safe)
Console.WriteLine("Username: " + username);

// Display bio with fallback
string displayBio = bio ?? "No bio provided";
Console.WriteLine("Bio: " + displayBio);

// Display website length safely
int? websiteLength = website?.Length;
Console.WriteLine("Website length: " + (websiteLength?.ToString() ?? "No website"));

// Alternative: Check with if
if (website != null)
{
    Console.WriteLine("Website: " + website);
}
else
{
    Console.WriteLine("No website configured");
}