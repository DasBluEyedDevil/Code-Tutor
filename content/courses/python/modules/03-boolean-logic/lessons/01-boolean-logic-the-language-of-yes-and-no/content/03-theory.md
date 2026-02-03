---
type: "THEORY"
title: "Syntax Breakdown"
---

### Understanding Boolean Values:
#### 1. True and False - The Only Boolean Values
```python
is_valid = True   # Capitalized!
is_error = False  # Capitalized!
```

**Critical:** Boolean values MUST be capitalized in Python:

- ✅ `True` - Correct
- ❌ `true` - Error! Python won't recognize it
- ✅ `False` - Correct
- ❌ `false` - Error! Python won't recognize it

Python is case-sensitive, so capitalization matters!

#### 2. The Six Comparison Operators
These operators *ask questions* and *return* True or False:

| Operator | Meaning | Example | Result |
| :--- | :--- | :--- | :--- |
| `==` | Equal to | `5 == 5` | True |
| `!=` | Not equal to | `5 != 3` | True |
| `>` | Greater than | `10 > 5` | True |
| `<` | Less than | `3 < 10` | True |
| `>=` | Greater than or equal | `5 >= 5` | True |
| `<=` | Less than or equal | `3 <= 5` | True |

#### 3. Common Beginner Confusion: == vs =
```python
# WRONG: Using = (assignment) when you mean == (comparison)
age = 18      # Assignment: "Make age equal to 18"
if age = 18:  # ERROR! This is trying to assign, not compare

# CORRECT: Using == (comparison)
age = 18      # Assignment: "Make age equal to 18"
if age == 18: # Comparison: "Is age equal to 18?" (Returns True/False)
```

**Memory trick:**

- `=` (one equal sign) → **Make it equal** (assignment)
- `==` (two equal signs) → **Check if equal** (comparison)

#### 4. Storing Boolean Results in Variables
You can store the result of a comparison in a variable:

```python
is_adult = age >= 18          # Stores True or False
has_permission = score > 80   # Stores True or False
is_valid_username = len(name) >= 3  # Stores True or False
```

This makes your code more readable!

#### 5. Comparing Strings
Comparison operators work with strings too:

```python
name = "Alice"

print(name == "Alice")   # True (exact match, case-sensitive!)
print(name == "alice")   # False (different case)
print(name != "Bob")     # True (not equal)

# Alphabetical comparison
print("apple" < "banana")  # True ('a' comes before 'b')
print("Zoo" < "apple")     # True (capital letters come before lowercase!)
```

**Important:** String comparison is case-sensitive! "Hello" != "hello"

#### 6. Truthy and Falsy Values (Advanced Preview)
In Python, almost any value can be treated as True or False in a Boolean context:

```python
# Falsy values (treated as False):
0              # Zero is False
0.0            # Zero float is False
""             # Empty string is False
None           # None is False

# Truthy values (treated as True):
1, -5, 100     # Any non-zero number is True
"hello", "0"   # Any non-empty string is True (even "0"!)
```

**Note:** You'll use this more in later modules. For now, focus on True, False, and comparison operators.
