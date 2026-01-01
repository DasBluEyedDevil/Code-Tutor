using Xunit;
using System.Collections.Generic;
using System.Linq;

public class ValidationResult
{
    public bool IsValid { get; set; }
    public List<string> ErrorMessages { get; set; } = new();
}

public class PasswordValidator
{
    public ValidationResult Validate(string password)
    {
        var result = new ValidationResult { IsValid = true };
        
        // Requirement 1: Minimum length
        if (password.Length < 8)
        {
            result.IsValid = false;
            result.ErrorMessages.Add("Password must be at least 8 characters");
        }
        
        // Requirement 2: Uppercase letter
        if (!password.Any(char.IsUpper))
        {
            result.IsValid = false;
            result.ErrorMessages.Add("Password must contain at least one uppercase letter");
        }
        
        // Requirement 3: Digit
        if (!password.Any(char.IsDigit))
        {
            result.IsValid = false;
            result.ErrorMessages.Add("Password must contain at least one digit");
        }
        
        // Requirement 4: Special character
        var specialChars = "!@#$%^&*";
        if (!password.Any(c => specialChars.Contains(c)))
        {
            result.IsValid = false;
            result.ErrorMessages.Add("Password must contain at least one special character (!@#$%^&*)");
        }
        
        return result;
    }
}

public class PasswordValidatorTests
{
    private readonly PasswordValidator _validator = new();
    
    [Fact]
    public void Validate_PasswordTooShort_ReturnsInvalid()
    {
        var result = _validator.Validate("Ab1!");
        
        Assert.False(result.IsValid);
        Assert.Contains(result.ErrorMessages, m => m.Contains("8 characters"));
    }
    
    [Fact]
    public void Validate_NoUppercase_ReturnsInvalid()
    {
        var result = _validator.Validate("lowercase1!");
        
        Assert.False(result.IsValid);
        Assert.Contains(result.ErrorMessages, m => m.Contains("uppercase"));
    }
    
    [Fact]
    public void Validate_NoDigit_ReturnsInvalid()
    {
        var result = _validator.Validate("NoDigits!!");
        
        Assert.False(result.IsValid);
        Assert.Contains(result.ErrorMessages, m => m.Contains("digit"));
    }
    
    [Fact]
    public void Validate_NoSpecialChar_ReturnsInvalid()
    {
        var result = _validator.Validate("NoSpecial1A");
        
        Assert.False(result.IsValid);
        Assert.Contains(result.ErrorMessages, m => m.Contains("special"));
    }
    
    [Fact]
    public void Validate_ValidPassword_ReturnsValid()
    {
        var result = _validator.Validate("SecureP@ss1");
        
        Assert.True(result.IsValid);
        Assert.Empty(result.ErrorMessages);
    }
    
    [Fact]
    public void Validate_MultipleErrors_ReturnsAllErrors()
    {
        var result = _validator.Validate("bad");
        
        Assert.False(result.IsValid);
        Assert.True(result.ErrorMessages.Count >= 3);
    }
}

Console.WriteLine("TDD Password Validator Complete!");
Console.WriteLine("Tests cover: length, uppercase, digit, special char");