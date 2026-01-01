---
type: "EXAMPLE"
title: "Code Example: Personal Utility Library"
---

**Expected Output:**
```
=== String Utilities ===
Original: '  Hello World  '
Cleaned: 'Hello World'
Slug: 'hello-world-from-python'
Truncated: 'Hello Wor...'
Is valid email? True
Is valid email? False

=== Math Utilities ===
75 out of 200 = 37.5%
Average: 85.0
Clamped 150 to range [0, 100]: 100
Clamped -10 to range [0, 100]: 0

=== Formatting Utilities ===
Formatted price: $1,234.56
Formatted large number: 1,234,567
Formatted phone: (555) 123-4567

=== List Utilities ===
Chunks of 3: [[1, 2, 3], [4, 5, 6], [7, 8]]
Flattened: [1, 2, 3, 4, 5, 6]
Unique (order preserved): [1, 2, 3, 4, 5]
```

```python
# Personal Utility Library
# A collection of reusable functions

print("=== String Utilities ===")

def clean_string(text):
    """Remove extra whitespace and clean up a string."""
    return " ".join(text.split())

def slugify(text):
    """Convert text to URL-friendly slug."""
    text = text.lower().strip()
    text = text.replace(" ", "-")
    # Remove non-alphanumeric characters (except hyphens)
    return "".join(c for c in text if c.isalnum() or c == "-")

def truncate(text, max_length=50, suffix="..."):
    """Truncate text to max_length, adding suffix if truncated."""
    if len(text) <= max_length:
        return text
    return text[:max_length - len(suffix)] + suffix

def is_valid_email(email):
    """Basic email validation (simplified)."""
    return "@" in email and "." in email.split("@")[-1]

test_string = "  Hello World  "
print(f"Original: '{test_string}'")
print(f"Cleaned: '{clean_string(test_string)}'")
print(f"Slug: '{slugify('Hello World from Python!')}'")
print(f"Truncated: '{truncate('Hello World', 12)}'")
print(f"Is valid email? {is_valid_email('user@example.com')}")
print(f"Is valid email? {is_valid_email('invalid-email')}")

print("\n=== Math Utilities ===")

def calculate_percentage(part, whole, decimal_places=2):
    """Calculate what percentage 'part' is of 'whole'."""
    if whole == 0:
        return 0.0
    return round((part / whole) * 100, decimal_places)

def average(numbers):
    """Calculate the average of a list of numbers."""
    if not numbers:
        return 0.0
    return sum(numbers) / len(numbers)

def clamp(value, min_val, max_val):
    """Clamp a value between min and max."""
    return max(min_val, min(value, max_val))

print(f"75 out of 200 = {calculate_percentage(75, 200)}%")
print(f"Average: {average([80, 85, 90, 85])}")
print(f"Clamped 150 to range [0, 100]: {clamp(150, 0, 100)}")
print(f"Clamped -10 to range [0, 100]: {clamp(-10, 0, 100)}")

print("\n=== Formatting Utilities ===")

def format_price(amount, currency="$"):
    """Format a number as currency."""
    return f"{currency}{amount:,.2f}"

def format_number(number):
    """Format a number with thousand separators."""
    return f"{number:,}"

def format_phone(phone):
    """Format a 10-digit phone number."""
    digits = "".join(c for c in phone if c.isdigit())
    if len(digits) == 10:
        return f"({digits[:3]}) {digits[3:6]}-{digits[6:]}"
    return phone  # Return original if not 10 digits

print(f"Formatted price: {format_price(1234.56)}")
print(f"Formatted large number: {format_number(1234567)}")
print(f"Formatted phone: {format_phone('5551234567')}")

print("\n=== List Utilities ===")

def chunk_list(lst, size):
    """Split a list into chunks of given size."""
    return [lst[i:i + size] for i in range(0, len(lst), size)]

def flatten(nested_list):
    """Flatten a list of lists into a single list."""
    return [item for sublist in nested_list for item in sublist]

def unique_preserve_order(lst):
    """Remove duplicates while preserving order."""
    seen = set()
    result = []
    for item in lst:
        if item not in seen:
            seen.add(item)
            result.append(item)
    return result

print(f"Chunks of 3: {chunk_list([1, 2, 3, 4, 5, 6, 7, 8], 3)}")
print(f"Flattened: {flatten([[1, 2], [3, 4], [5, 6]])}")
print(f"Unique (order preserved): {unique_preserve_order([1, 2, 2, 3, 1, 4, 5, 3])}")
```
