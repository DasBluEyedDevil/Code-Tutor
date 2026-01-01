def validate_registration(username, email, age, password):
    """Validate user registration data."""
    
    # TODO: Validate username
    # - Not empty (after stripping)
    # - 3-20 characters
    # - Only letters and numbers (use .isalnum())
    
    # TODO: Validate email
    # - Not empty
    # - Contains @
    # - Contains . after @
    
    # TODO: Validate age
    # - Is an integer (isinstance)
    # - Between 13-120
    
    # TODO: Validate password
    # - Not empty
    # - At least 8 characters
    
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
    print(f"\nTest {i}: user='{user}', email='{email}', age={age}")
    try:
        if validate_registration(user, email, age, pwd):
            print("  ✓ Registration valid")
    except (ValueError, TypeError) as e:
        print(f"  ✗ {e}")