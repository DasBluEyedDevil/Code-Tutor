---
type: "EXAMPLE"
title: "Code Example: Basic Regular Expressions"
---

**Key regex functions:**

**1. re.match(pattern, string):**
- Matches at START of string
- Returns Match object or None
```python
re.match(r"Hello", "Hello World")  # Matches
re.match(r"World", "Hello World")  # Doesn't match
```

**2. re.search(pattern, string):**
- Searches ANYWHERE in string
- Returns first match
```python
re.search(r"World", "Hello World")  # Matches
```

**3. re.findall(pattern, string):**
- Returns list of ALL matches
```python
re.findall(r"\d+", "10 cats, 20 dogs")  # ['10', '20']
```

**4. re.sub(pattern, replacement, string):**
- Replace matches
```python
re.sub(r"\d+", "X", "10 cats")  # "X cats"
```

**Raw strings (r""):**
- Use r"" for regex patterns
- Prevents backslash escaping issues
- r"\d" not "\\d"

```python
import re

print("=== Basic Matching ===")

# re.match() - Match at start of string
text = "Hello World"
if re.match(r"Hello", text):
    print(f"'{text}' starts with 'Hello'")

if not re.match(r"World", text):
    print(f"'{text}' does NOT start with 'World'")

# re.search() - Find anywhere in string
if re.search(r"World", text):
    print(f"'{text}' contains 'World'")

# re.findall() - Find all occurrences
text = "The price is $10, $20, and $30"
prices = re.findall(r"\$\d+", text)
print(f"Found prices: {prices}")

print("\n=== Character Classes ===")

# \d - digits
text = "My phone: 555-1234"
digits = re.findall(r"\d", text)
print(f"All digits: {''.join(digits)}")

# \d+ - one or more digits
numbers = re.findall(r"\d+", text)
print(f"Number groups: {numbers}")

# \w - word characters
text = "Hello, World! 123"
words = re.findall(r"\w+", text)
print(f"Words: {words}")

# \s - whitespace
text = "Split   on    spaces"
parts = re.split(r"\s+", text)
print(f"Split parts: {parts}")

print("\n=== Quantifiers ===")

# * - zero or more
pattern = r"a*b"
print(f"'b' matches: {bool(re.match(pattern, 'b'))}")
print(f"'ab' matches: {bool(re.match(pattern, 'ab'))}")
print(f"'aaab' matches: {bool(re.match(pattern, 'aaab'))}")

# + - one or more
pattern = r"a+b"
print(f"\n'b' matches a+b: {bool(re.match(pattern, 'b'))}")
print(f"'ab' matches a+b: {bool(re.match(pattern, 'ab'))}")

# ? - zero or one (optional)
pattern = r"colou?r"  # Matches color or colour
print(f"\n'color' matches: {bool(re.match(pattern, 'color'))}")
print(f"'colour' matches: {bool(re.match(pattern, 'colour'))}")

# {n} - exactly n times
pattern = r"\d{3}"  # Exactly 3 digits
print(f"\n'123' matches \\d{{3}}: {bool(re.match(pattern, '123'))}")
print(f"'12' matches \\d{{3}}: {bool(re.match(pattern, '12'))}")

# {n,m} - between n and m times
pattern = r"\d{2,4}"  # 2 to 4 digits
print(f"\n'12' matches \\d{{2,4}}: {bool(re.match(pattern, '12'))}")
print(f"'123' matches \\d{{2,4}}: {bool(re.match(pattern, '123'))}")

print("\n=== Anchors and Boundaries ===")

# ^ - start of string
pattern = r"^Hello"
print(f"'Hello World' starts with Hello: {bool(re.match(pattern, 'Hello World'))}")
print(f"'Say Hello' starts with Hello: {bool(re.match(pattern, 'Say Hello'))}")

# $ - end of string
pattern = r"World$"
print(f"\n'Hello World' ends with World: {bool(re.search(pattern, 'Hello World'))}")
print(f"'World Hello' ends with World: {bool(re.search(pattern, 'World Hello'))}")

# Combining ^ and $ - full string match
pattern = r"^\d{3}$"  # Exactly 3 digits, nothing else
print(f"\n'123' is exactly 3 digits: {bool(re.match(pattern, '123'))}")
print(f"'1234' is exactly 3 digits: {bool(re.match(pattern, '1234'))}")

print("\n=== Character Classes ===")

# [abc] - any of these characters
pattern = r"[aeiou]"
vowels = re.findall(pattern, "hello world")
print(f"Vowels in 'hello world': {vowels}")

# [a-z] - range
pattern = r"[a-z]+"
words = re.findall(pattern, "Hello123World456")
print(f"Lowercase words: {words}")

# [^abc] - NOT these characters
pattern = r"[^aeiou]+"
consonants = re.findall(pattern, "hello world")
print(f"Consonant groups: {consonants}")
```
