# Variables and Data Types

Now that you can display text, let's learn how to store and work with information.

## What is a Variable?

Think of a variable as a labeled box where you can store information. You can:
- Put something in the box
- Look at what's inside
- Change what's inside
- Use what's inside for calculations

## Creating Variables in Python

Creating a variable is simple - you just give it a name and a value:

```python
name = "Alice"
age = 25
height = 5.6
```

The `=` sign doesn't mean "equals" like in math - it means "assign this value to this variable."

## Data Types

Computers store different kinds of information in different ways:

### Strings (Text)
Text must be in quotes:
```python
name = "Alice"
city = "New York"
message = "Hello, World!"
```

### Integers (Whole Numbers)
Numbers without decimals:
```python
age = 25
year = 2025
score = 100
```

### Floats (Decimal Numbers)
Numbers with decimal points:
```python
height = 5.6
price = 19.99
temperature = 98.6
```

### Booleans (True/False)
Used for yes/no, on/off situations:
```python
is_student = True
has_license = False
```

## Naming Variables

Good variable names:
- Describe what they store: `age` not `a`
- Use lowercase with underscores: `first_name` not `FirstName`
- Can't start with numbers: `name1` is ok, `1name` is not
- Can't use spaces: `first_name` not `first name`

## Using Variables

Once you create a variable, you can use it:

```python
name = "Bob"
age = 30

print(name)  # Displays: Bob
print(age)   # Displays: 30
```

You can also combine them:

```python
name = "Bob"
age = 30
print(f"{name} is {age} years old")
# Displays: Bob is 30 years old
```

The `f` before the string makes it an "f-string" - it lets you put variables inside the text using curly braces `{}`.

## Key Concepts

**Variable**: A container for storing data values

**String**: Text data enclosed in quotes

**Integer**: Whole numbers without decimals

**Float**: Numbers with decimal points

**Boolean**: True or False values

**Assignment**: Using = to put a value in a variable

## Common Mistakes

❌ Forgetting quotes around text:
```python
name = Alice  # Wrong!
```

✓ Including quotes:
```python
name = "Alice"  # Correct!
```

❌ Using spaces in variable names:
```python
first name = "Bob"  # Wrong!
```

✓ Using underscores:
```python
first_name = "Bob"  # Correct!
```

Now it's your turn to practice!
