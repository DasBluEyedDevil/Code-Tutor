using Xunit;
using System.Collections.Generic;

public class ValidationResult
{
    public bool IsValid { get; set; }
    public List<string> ErrorMessages { get; set; } = new();
}

public class PasswordValidator
{
    public ValidationResult Validate(string password)
    {
        // TODO: Implement using TDD
        // Start with just length check, then add others
        return new ValidationResult { IsValid = false };
    }
}

public class PasswordValidatorTests
{
    private readonly PasswordValidator _validator = new();
    
    // STEP 1: Test for minimum length (RED first!)
    [Fact]
    public void Validate_PasswordTooShort_ReturnsInvalid()
    {
        var result = _validator.Validate("Ab1!");
        
        Assert.False(result.IsValid);
        Assert.Contains(result.ErrorMessages, 
            m => m.Contains("8 characters"));
    }
    
    [Fact]
    public void Validate_ValidPassword_ReturnsValid()
    {
        var result = _validator.Validate("SecureP@ss1");
        
        Assert.True(result.IsValid);
        Assert.Empty(result.ErrorMessages);
    }
    
    // TODO: Add tests for uppercase, digit, special character
}