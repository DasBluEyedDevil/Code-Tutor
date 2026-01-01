# TODO: Create custom exception classes
class TooShortError(Exception):
    """Password is too short."""
    pass

# TODO: Create NoDigitError class

# TODO: Create NoUppercaseError class

def validate_password(password):
    """Validate password and raise exceptions on failure."""
    
    # TODO: Check length (< 8 chars)
    # Raise TooShortError with helpful message
    
    # TODO: Check for at least one digit
    # Use: any(char.isdigit() for char in password)
    # Raise NoDigitError if no digits found
    
    # TODO: Check for at least one uppercase letter
    # Use: any(char.isupper() for char in password)
    # Raise NoUppercaseError if no uppercase found
    
    # If all checks pass
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
            print(f"✓ '{pwd}' is valid")
    except (TooShortError, NoDigitError, NoUppercaseError) as e:
        print(f"✗ '{pwd}': {e}")