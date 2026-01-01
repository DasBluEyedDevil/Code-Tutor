# User Registration Validator
# This solution demonstrates comprehensive input validation

def validate_registration(username, email, age, password):
    """Validate user registration data."""
    
    # Step 1: Validate username
    if not username.strip():
        raise ValueError("Username cannot be empty")
    if not 3 <= len(username) <= 20:
        raise ValueError(f"Username must be 3-20 characters (got {len(username)})")
    if not username.isalnum():
        raise ValueError("Username must contain only letters and numbers")
    
    # Step 2: Validate email
    if not email.strip():
        raise ValueError("Email cannot be empty")
    if '@' not in email:
        raise ValueError("Email must contain @")
    # Check for . after @
    at_index = email.index('@')
    if '.' not in email[at_index:]:
        raise ValueError("Email must contain . after @")
    
    # Step 3: Validate age
    if not isinstance(age, int):
        raise TypeError(f"Age must be an integer, got {type(age).__name__}")
    if not 13 <= age <= 120:
        raise ValueError(f"Age must be between 13-120 (got {age})")
    
    # Step 4: Validate password
    if not password:
        raise ValueError("Password cannot be empty")
    if len(password) < 8:
        raise ValueError(f"Password must be at least 8 characters (got {len(password)})")
    
    return True

# Test cases
test_cases = [
    ("alice123", "alice@example.com", 25, "SecurePass123"),  # Valid
    ("", "alice@example.com", 25, "SecurePass123"),  # Empty username
    ("ab", "alice@example.com", 25, "SecurePass123"),  # Short username
    ("alice123", "invalid-email", 25, "SecurePass123"),  # Invalid email
    ("alice123", "alice@example.com", 10, "SecurePass123"),  # Too young
    ("alice123", "alice@example.com", 25, "short"),  # Short password
]

for i, (user, email, age, pwd) in enumerate(test_cases, 1):
    print(f"Test {i}: user='{user}', email='{email}', age={age}")
    try:
        if validate_registration(user, email, age, pwd):
            print("  Registration valid")
    except (ValueError, TypeError) as e:
        print(f"  Error: {e}")