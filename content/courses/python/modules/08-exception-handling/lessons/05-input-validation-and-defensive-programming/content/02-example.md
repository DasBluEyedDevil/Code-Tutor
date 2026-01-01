---
type: "EXAMPLE"
title: "Code Example: Input Validation Patterns"
---

The code demonstrates:
1. **Step-by-step validation** (check empty, type, range)
2. **EAFP vs. LBYL** approaches (Python prefers EAFP)
3. **Comprehensive validation** with helpful error messages
4. **Type checking** with isinstance()
5. **Range validation** to ensure values are reasonable
6. **Defensive programming** that assumes inputs are wrong until proven right

```python
# Example 1: Basic input validation
print("=== Basic Input Validation ===")

def get_age_from_user(user_input):
    """Validate and convert age input."""
    
    # Step 1: Check if input is empty
    if not user_input or user_input.strip() == "":
        raise ValueError("Age cannot be empty")
    
    # Step 2: Try to convert to integer
    try:
        age = int(user_input.strip())
    except ValueError:
        raise ValueError(f"Age must be a number, got '{user_input}'")
    
    # Step 3: Validate range
    if age < 0:
        raise ValueError(f"Age cannot be negative (got {age})")
    if age > 120:
        raise ValueError(f"Age seems unrealistic (got {age})")
    
    return age

# Test cases
test_inputs = ["25", "  30  ", "", "abc", "-5", "200"]

for test_input in test_inputs:
    print(f"\nInput: '{test_input}'")
    try:
        age = get_age_from_user(test_input)
        print(f"  ✓ Valid age: {age}")
    except ValueError as e:
        print(f"  ✗ Invalid: {e}")

# Example 2: EAFP vs LBYL
print("\n\n=== EAFP vs LBYL Comparison ===")

my_dict = {"name": "Alice", "age": 25}

# LBYL (Look Before You Leap)
print("\nLBYL approach:")
if "email" in my_dict:
    email = my_dict["email"]
    print(f"Email: {email}")
else:
    print("No email found")
    email = "no-email@example.com"

print(f"Result: {email}")

# EAFP (Easier to Ask Forgiveness than Permission) - More Pythonic!
print("\nEAFP approach:")
try:
    email = my_dict["email"]
    print(f"Email: {email}")
except KeyError:
    print("No email found")
    email = "no-email@example.com"

print(f"Result: {email}")

# Or even better - using .get() for simple cases
print("\nBest approach (for dicts):")
email = my_dict.get("email", "no-email@example.com")
print(f"Result: {email}")

# Example 3: Comprehensive validation function
print("\n\n=== Comprehensive Email Validation ===")

def validate_email(email):
    """Validate email with multiple checks."""
    
    # Check 1: Not empty
    if not email or not email.strip():
        return False, "Email cannot be empty"
    
    email = email.strip()
    
    # Check 2: Contains @
    if "@" not in email:
        return False, "Email must contain @"
    
    # Check 3: Has content before and after @
    parts = email.split("@")
    if len(parts) != 2:
        return False, "Email must have exactly one @"
    
    local, domain = parts
    
    if not local:
        return False, "Email must have content before @"
    if not domain:
        return False, "Email must have content after @"
    
    # Check 4: Domain has a dot
    if "." not in domain:
        return False, "Domain must contain a dot (e.g., gmail.com)"
    
    # Check 5: Domain parts are not empty
    domain_parts = domain.split(".")
    if any(part == "" for part in domain_parts):
        return False, "Domain parts cannot be empty"
    
    # All checks passed!
    return True, "Valid email"

# Test email validation
test_emails = [
    "alice@example.com",
    "bob@company.co.uk",
    "",
    "no-at-sign.com",
    "@example.com",
    "user@",
    "user@@example.com",
    "user@nodot",
    "user@example.",
]

for email in test_emails:
    is_valid, message = validate_email(email)
    status = "✓" if is_valid else "✗"
    print(f"{status} '{email}': {message}")

# Example 4: Defensive programming in practice
print("\n\n=== Defensive Programming Example ===")

def calculate_discount(price, discount_percent=0, coupon_code=None):
    """Calculate price with discount - defensive version."""
    
    # Validate price
    if not isinstance(price, (int, float)):
        raise TypeError(f"Price must be a number, got {type(price).__name__}")
    if price < 0:
        raise ValueError(f"Price cannot be negative (got {price})")
    
    # Validate discount percent
    if not isinstance(discount_percent, (int, float)):
        raise TypeError(f"Discount must be a number, got {type(discount_percent).__name__}")
    if not 0 <= discount_percent <= 100:
        raise ValueError(f"Discount must be 0-100% (got {discount_percent})")
    
    # Validate coupon code (optional)
    if coupon_code is not None and not isinstance(coupon_code, str):
        raise TypeError("Coupon code must be a string")
    
    # Apply discount
    discount_amount = price * (discount_percent / 100)
    discounted_price = price - discount_amount
    
    # Apply coupon if provided
    if coupon_code:
        coupon_code = coupon_code.strip().upper()
        if coupon_code == "SAVE10":
            discounted_price *= 0.9  # Extra 10% off
            print(f"  Coupon '{coupon_code}' applied: -10%")
    
    return round(discounted_price, 2)

# Test defensive function
print("\nTest 1: Valid inputs")
try:
    result = calculate_discount(100, 20, "SAVE10")
    print(f"  Final price: ${result}\n")
except (TypeError, ValueError) as e:
    print(f"  Error: {e}\n")

print("Test 2: Invalid price type")
try:
    result = calculate_discount("100", 20)
    print(f"  Final price: ${result}\n")
except (TypeError, ValueError) as e:
    print(f"  Error: {e}\n")

print("Test 3: Invalid discount range")
try:
    result = calculate_discount(100, 150)
    print(f"  Final price: ${result}\n")
except (TypeError, ValueError) as e:
    print(f"  Error: {e}\n")

print("Test 4: Invalid coupon type")
try:
    result = calculate_discount(100, 20, 12345)
    print(f"  Final price: ${result}\n")
except (TypeError, ValueError) as e:
    print(f"  Error: {e}\n")
```
