---
type: "EXAMPLE"
title: "Code Example: Groups and Practical Patterns"
---

**Capturing groups:**
```python
pattern = r"(\d{3})-(\d{3})-(\d{4})"
match = re.search(pattern, "555-123-4567")
match.group(0)  # Full match: "555-123-4567"
match.group(1)  # First group: "555"
match.groups()  # All groups: ('555', '123', '4567')
```

**Named groups:**
```python
pattern = r"(?P<area>\d{3})-(?P<num>\d{3})"
match.group('area')  # By name
match.groupdict()    # Dict of named groups
```

**Substitution with groups:**
```python
# Swap first and last name
re.sub(r'(\w+) (\w+)', r'\2, \1', "John Doe")
# Result: "Doe, John"
```

**Flags:**
```python
re.IGNORECASE  # Case-insensitive
re.MULTILINE   # ^ and $ match line boundaries
re.DOTALL      # . matches newlines too
```

```python
import re

print("=== Capturing Groups ===")

# Groups with ()
pattern = r"(\d{3})-(\d{3})-(\d{4})"
phone = "555-123-4567"
match = re.search(pattern, phone)

if match:
    print(f"Full match: {match.group(0)}")
    print(f"Area code: {match.group(1)}")
    print(f"Exchange: {match.group(2)}")
    print(f"Number: {match.group(3)}")
    print(f"All groups: {match.groups()}")

# Named groups
pattern = r"(?P<area>\d{3})-(?P<exchange>\d{3})-(?P<number>\d{4})"
match = re.search(pattern, phone)

if match:
    print(f"\nNamed groups:")
    print(f"Area: {match.group('area')}")
    print(f"Exchange: {match.group('exchange')}")
    print(f"Number: {match.group('number')}")
    print(f"Dict: {match.groupdict()}")

print("\n=== Email Validation ===")

email_pattern = r'^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$'

emails = [
    "user@example.com",
    "test.user@domain.co.uk",
    "invalid@",
    "@invalid.com",
    "no-at-sign.com"
]

for email in emails:
    valid = bool(re.match(email_pattern, email))
    print(f"{email:25} {'✓ Valid' if valid else '✗ Invalid'}")

print("\n=== URL Extraction ===")

text = """
Check out https://www.example.com and http://test.org
Also visit ftp://files.example.com for downloads.
"""

url_pattern = r'https?://[\w.-]+\.[a-zA-Z]{2,}'
urls = re.findall(url_pattern, text)
print(f"Found URLs: {urls}")

print("\n=== Phone Number Formatting ===")

def format_phone(phone):
    """Extract and format phone number"""
    # Remove all non-digits
    digits = re.sub(r'\D', '', phone)
    
    # Format as (XXX) XXX-XXXX
    if len(digits) == 10:
        return f"({digits[:3]}) {digits[3:6]}-{digits[6:]}"
    return phone

phones = [
    "5551234567",
    "555-123-4567",
    "(555) 123-4567",
    "555.123.4567"
]

for phone in phones:
    formatted = format_phone(phone)
    print(f"{phone:20} → {formatted}")

print("\n=== Date Extraction ===")

text = "Meeting on 2024-01-15, deadline 03/20/2024, event: 12-25-2024"

# Multiple date formats
date_patterns = [
    r'\d{4}-\d{2}-\d{2}',  # YYYY-MM-DD
    r'\d{2}/\d{2}/\d{4}',  # MM/DD/YYYY
    r'\d{2}-\d{2}-\d{4}',  # MM-DD-YYYY
]

all_dates = []
for pattern in date_patterns:
    dates = re.findall(pattern, text)
    all_dates.extend(dates)

print(f"Dates found: {all_dates}")

print("\n=== Text Substitution ===")

# Replace multiple spaces with single space
text = "Too    many     spaces"
cleaned = re.sub(r'\s+', ' ', text)
print(f"Original: '{text}'")
print(f"Cleaned:  '{cleaned}'")

# Remove HTML tags
html = "<p>Hello <b>World</b>!</p>"
plain = re.sub(r'<[^>]+>', '', html)
print(f"\nHTML: {html}")
print(f"Plain: {plain}")

# Censor words
text = "This is damn bad stuff"
censored = re.sub(r'\b(damn|bad)\b', '***', text, flags=re.IGNORECASE)
print(f"\nOriginal: {text}")
print(f"Censored: {censored}")

print("\n=== Split with Regex ===")

# Split on multiple delimiters
text = "apple;banana,cherry:date|elderberry"
fruits = re.split(r'[;,:|
]+', text)
print(f"Fruits: {fruits}")

# Split but keep delimiter
text = "Question? Answer! Statement."
parts = re.split(r'([.!?])', text)
print(f"With punctuation: {parts}")

print("\n=== Password Validation ===")

def validate_password(password):
    """Validate password strength"""
    checks = {
        'length': len(password) >= 8,
        'uppercase': bool(re.search(r'[A-Z]', password)),
        'lowercase': bool(re.search(r'[a-z]', password)),
        'digit': bool(re.search(r'\d', password)),
        'special': bool(re.search(r'[!@#$%^&*()]', password))
    }
    
    return all(checks.values()), checks

passwords = [
    "weak",
    "StrongPass123!",
    "NoDigits!",
    "ALLUPPER123!"
]

for pwd in passwords:
    valid, checks = validate_password(pwd)
    status = "✓ Valid" if valid else "✗ Invalid"
    print(f"{pwd:20} {status}")
    if not valid:
        failed = [k for k, v in checks.items() if not v]
        print(f"{'':20} Missing: {', '.join(failed)}")
```
