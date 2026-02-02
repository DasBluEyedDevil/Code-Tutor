import pytest
import re
from typing import List


# ============================================
# STEP 1: Write tests first (TDD Red Phase)
# ============================================

class PasswordValidator:
    """Validates passwords against security requirements.
    
    Requirements:
    - Minimum length (default 8 characters)
    - At least one uppercase letter
    - At least one number
    - At least one special character
    """
    
    # Define special characters for validation
    SPECIAL_CHARS = '!@#$%^&*()_+-=[]{}|;:,.<>?'
    
    def __init__(self, min_length: int = 8):
        """Initialize validator with minimum length requirement.
        
        Args:
            min_length: Minimum password length (default 8)
        """
        self.min_length = min_length
    
    def validate(self, password: str) -> bool:
        """Check if password meets all requirements.
        
        Args:
            password: The password to validate
        
        Returns:
            True if password meets all requirements, False otherwise
        """
        # Get list of errors - if empty, password is valid
        errors = self.get_errors(password)
        return len(errors) == 0
    
    def get_errors(self, password: str) -> List[str]:
        """Get list of validation errors for a password.
        
        Args:
            password: The password to validate
        
        Returns:
            List of error messages (empty if valid)
        """
        errors = []
        
        # Check minimum length
        if len(password) < self.min_length:
            errors.append(
                f'Password must be at least {self.min_length} characters'
            )
        
        # Check for uppercase letter
        if not any(c.isupper() for c in password):
            errors.append('Password must contain at least one uppercase letter')
        
        # Check for number
        if not any(c.isdigit() for c in password):
            errors.append('Password must contain at least one number')
        
        # Check for special character
        if not any(c in self.SPECIAL_CHARS for c in password):
            errors.append('Password must contain at least one special character')
        
        return errors
    
    def get_strength(self, password: str) -> str:
        """Evaluate password strength.
        
        Args:
            password: The password to evaluate
        
        Returns:
            Strength rating: 'weak', 'medium', 'strong', or 'invalid'
        """
        if not self.validate(password):
            return 'invalid'
        
        score = 0
        
        # Length bonuses
        if len(password) >= 12:
            score += 2
        elif len(password) >= 10:
            score += 1
        
        # Character variety bonuses
        if sum(1 for c in password if c.isupper()) >= 2:
            score += 1
        if sum(1 for c in password if c.isdigit()) >= 2:
            score += 1
        if sum(1 for c in password if c in self.SPECIAL_CHARS) >= 2:
            score += 1
        
        # Determine strength
        if score >= 4:
            return 'strong'
        elif score >= 2:
            return 'medium'
        else:
            return 'weak'


# ============================================
# STEP 2: Comprehensive Tests
# ============================================

# Test fixture - provides a validator instance
@pytest.fixture
def validator():
    """Create a PasswordValidator instance for testing."""
    return PasswordValidator(min_length=8)


# Test: Password minimum length
def test_password_length(validator):
    """Test that passwords must meet minimum length."""
    # Too short - should fail
    assert validator.validate('Aa1!') == False
    assert validator.validate('Aa1!abc') == False  # 7 chars
    
    # Exactly 8 chars - should pass (if other requirements met)
    assert validator.validate('Aa1!abcd') == True
    
    # Longer - should pass
    assert validator.validate('Aa1!abcdef') == True


# Test: Uppercase letter requirement
def test_password_uppercase(validator):
    """Test that passwords must contain uppercase letter."""
    # No uppercase - should fail
    assert validator.validate('aa1!abcd') == False
    
    # With uppercase - should pass
    assert validator.validate('Aa1!abcd') == True
    
    # Multiple uppercase - should pass
    assert validator.validate('AA1!abcd') == True


# Test: Number requirement
def test_password_number(validator):
    """Test that passwords must contain a number."""
    # No number - should fail
    assert validator.validate('Aaa!abcd') == False
    
    # With number - should pass
    assert validator.validate('Aa1!abcd') == True
    
    # Multiple numbers - should pass
    assert validator.validate('Aa12!bcd') == True


# Test: Special character requirement
def test_password_special_char(validator):
    """Test that passwords must contain special character."""
    # No special char - should fail
    assert validator.validate('Aa1aabcd') == False
    
    # With special char - should pass
    assert validator.validate('Aa1!abcd') == True
    
    # Different special chars - all should pass
    for char in '@#$%^&*':
        assert validator.validate(f'Aa1{char}abcd') == True


# Test: Error messages
def test_get_errors_returns_all_issues(validator):
    """Test that get_errors returns all validation issues."""
    # Empty password - should have all errors
    errors = validator.get_errors('')
    assert len(errors) == 4
    assert any('length' in e.lower() for e in errors)
    assert any('uppercase' in e.lower() for e in errors)
    assert any('number' in e.lower() for e in errors)
    assert any('special' in e.lower() for e in errors)
    
    # Valid password - should have no errors
    errors = validator.get_errors('Aa1!abcd')
    assert errors == []


# Test: Custom minimum length
def test_custom_min_length():
    """Test validator with custom minimum length."""
    validator = PasswordValidator(min_length=12)
    
    # 8 chars now too short
    assert validator.validate('Aa1!abcd') == False
    
    # 12 chars - should pass
    assert validator.validate('Aa1!abcdefgh') == True


# Test: Parametrized tests for multiple valid passwords
@pytest.mark.parametrize('password', [
    'Aa1!abcd',
    'Password123!',
    'MyP@ssw0rd',
    'Secure#Pass1',
    'Test1234$$$',
])
def test_valid_passwords(validator, password):
    """Test that various valid passwords pass validation."""
    assert validator.validate(password) == True


# Test: Parametrized tests for invalid passwords
@pytest.mark.parametrize('password,reason', [
    ('short1!', 'too short'),
    ('alllowercase1!', 'no uppercase'),
    ('ALLUPPERCASE1!', 'has uppercase but valid actually'),
    ('NoNumbers!!', 'no numbers'),
    ('NoSpecial123', 'no special chars'),
])
def test_invalid_passwords(validator, password, reason):
    """Test that invalid passwords fail validation."""
    # Note: 'ALLUPPERCASE1!' is actually valid, so we skip that case
    if password != 'ALLUPPERCASE1!':
        assert validator.validate(password) == False


# Test: Password strength
def test_password_strength(validator):
    """Test password strength evaluation."""
    # Invalid password
    assert validator.get_strength('weak') == 'invalid'
    
    # Weak but valid
    assert validator.get_strength('Aa1!abcd') in ['weak', 'medium']
    
    # Strong password (long, multiple special chars, etc.)
    assert validator.get_strength('MyStr0ng!P@ssword123') == 'strong'


# ============================================
# Demo the validator
# ============================================

if __name__ == '__main__':
    print('Password Validator Demo')
    print('=' * 40)
    
    validator = PasswordValidator()
    
    test_passwords = [
        'weak',
        'Password',
        'password123',
        'Password123',
        'Password123!',
        'MyStr0ng!P@ssword',
    ]
    
    for pwd in test_passwords:
        is_valid = validator.validate(pwd)
        errors = validator.get_errors(pwd)
        strength = validator.get_strength(pwd)
        
        print(f"\nPassword: '{pwd}'")
        print(f"  Valid: {is_valid}")
        print(f"  Strength: {strength}")
        if errors:
            print(f"  Errors:")
            for error in errors:
                print(f"    - {error}")
    
    print('\n' + '=' * 40)
    print('Run tests with: pytest -v')