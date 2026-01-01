# Password Validator with Custom Exceptions
# This solution demonstrates creating and raising custom exceptions

# Step 1: Define custom exception classes
class TooShortError(Exception):
    """Password is too short."""
    pass

class NoDigitError(Exception):
    """Password has no digits."""
    pass

class NoUppercaseError(Exception):
    """Password has no uppercase letters."""
    pass

def validate_password(password):
    """Validate password and raise exceptions on failure."""
    
    # Step 2: Check length (must be at least 8 characters)
    if len(password) < 8:
        raise TooShortError(f"Password must be at least 8 characters (got {len(password)})")
    
    # Step 3: Check for at least one digit
    if not any(char.isdigit() for char in password):
        raise NoDigitError("Password must contain at least one digit")
    
    # Step 4: Check for at least one uppercase letter
    if not any(char.isupper() for char in password):
        raise NoUppercaseError("Password must contain at least one uppercase letter")
    
    # All checks passed!
    return True

# Test cases
test_passwords = [
    "SecurePass123",  # Valid
    "short",          # Too short
    "nouppercase123", # No uppercase
    "NODIGITS",       # No digit
]

for pwd in test_passwords:
    try:
        if validate_password(pwd):
            print(f"Valid: '{pwd}'")
    except (TooShortError, NoDigitError, NoUppercaseError) as e:
        print(f"Invalid '{pwd}': {e}")