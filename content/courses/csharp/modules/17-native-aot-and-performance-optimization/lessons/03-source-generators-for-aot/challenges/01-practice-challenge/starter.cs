using System.Text.RegularExpressions;

// TODO: Create partial class InputValidators with generated regex methods
// Patterns needed:
// - CreditCard: 16 digits, optional separators (1234-5678-9012-3456)
// - PostalCode: 5 digits or 5+4 (12345 or 12345-6789)
// - Username: 3-20 chars, alphanumeric + underscore
// - StrongPassword: 8+ chars, upper, lower, digit, special

public partial class InputValidators
{
    // TODO: [GeneratedRegex(...)] for CreditCard
    
    // TODO: [GeneratedRegex(...)] for PostalCode
    
    // TODO: [GeneratedRegex(...)] for Username
    
    // TODO: [GeneratedRegex(...)] for StrongPassword
}

// Test the validators
Console.WriteLine("=== AOT-Ready Input Validation ===");

// Test data
var testCreditCard = "1234-5678-9012-3456";
var testPostalCode = "12345-6789";
var testUsername = "user_123";
var testPassword = "Secure@123";

// TODO: Run validations and print results

Console.WriteLine("\nAll regex compiled at build time - AOT ready!");