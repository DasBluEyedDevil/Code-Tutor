using System.Text.RegularExpressions;

public partial class InputValidators
{
    // Credit card: 16 digits with optional dashes or spaces every 4
    [GeneratedRegex(@"^\d{4}[-\s]?\d{4}[-\s]?\d{4}[-\s]?\d{4}$")]
    public static partial Regex CreditCardRegex();
    
    // Postal code: 5 digits or 5+4 format
    [GeneratedRegex(@"^\d{5}(-\d{4})?$")]
    public static partial Regex PostalCodeRegex();
    
    // Username: 3-20 alphanumeric + underscore
    [GeneratedRegex(@"^[a-zA-Z0-9_]{3,20}$")]
    public static partial Regex UsernameRegex();
    
    // Strong password: 8+ chars, upper, lower, digit, special
    [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")]
    public static partial Regex StrongPasswordRegex();
    
    // Helper methods for cleaner API
    public static bool IsValidCreditCard(string input) => 
        CreditCardRegex().IsMatch(input);
    
    public static bool IsValidPostalCode(string input) => 
        PostalCodeRegex().IsMatch(input);
    
    public static bool IsValidUsername(string input) => 
        UsernameRegex().IsMatch(input);
    
    public static bool IsStrongPassword(string input) => 
        StrongPasswordRegex().IsMatch(input);
}

// Test the validators
Console.WriteLine("=== AOT-Ready Input Validation ===");

// Valid test data
var validCard = "1234-5678-9012-3456";
var validPostal = "12345-6789";
var validUser = "user_123";
var validPassword = "Secure@123";

// Invalid test data
var invalidCard = "1234-5678";
var invalidPostal = "1234";
var invalidUser = "ab";
var invalidPassword = "weak";

Console.WriteLine("\n--- Valid Inputs ---");
Console.WriteLine($"Credit Card '{validCard}': {InputValidators.IsValidCreditCard(validCard)}");
Console.WriteLine($"Postal Code '{validPostal}': {InputValidators.IsValidPostalCode(validPostal)}");
Console.WriteLine($"Username '{validUser}': {InputValidators.IsValidUsername(validUser)}");
Console.WriteLine($"Password '{validPassword}': {InputValidators.IsStrongPassword(validPassword)}");

Console.WriteLine("\n--- Invalid Inputs ---");
Console.WriteLine($"Credit Card '{invalidCard}': {InputValidators.IsValidCreditCard(invalidCard)}");
Console.WriteLine($"Postal Code '{invalidPostal}': {InputValidators.IsValidPostalCode(invalidPostal)}");
Console.WriteLine($"Username '{invalidUser}': {InputValidators.IsValidUsername(invalidUser)}");
Console.WriteLine($"Password '{invalidPassword}': {InputValidators.IsStrongPassword(invalidPassword)}");

Console.WriteLine("\nAll regex compiled at build time - AOT ready!");