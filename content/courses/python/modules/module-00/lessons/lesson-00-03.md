# Math Operations

Python is great at math! Let's learn how to perform calculations.

## Basic Operators

Python uses symbols called **operators** to perform math:

| Operator | Name | Example | Result |
|----------|------|---------|--------|
| `+` | Addition | `10 + 5` | `15` |
| `-` | Subtraction | `10 - 5` | `5` |
| `*` | Multiplication | `10 * 5` | `50` |
| `/` | Division | `10 / 5` | `2.0` |
| `**` | Exponent (Power) | `10 ** 2` | `100` |
| `//` | Floor Division | `10 // 3` | `3` |
| `%` | Modulus (Remainder) | `10 % 3` | `1` |

## Addition and Subtraction

Just like you learned in school:

```python
result = 10 + 5
print(result)  # Displays: 15

result = 10 - 5
print(result)  # Displays: 5
```

## Multiplication and Division

The `*` means multiply, and `/` means divide:

```python
result = 10 * 5
print(result)  # Displays: 50

result = 10 / 5
print(result)  # Displays: 2.0
```

Notice that division always gives you a decimal number (float), even if the answer is a whole number!

## Using Variables

You can do math with variables:

```python
width = 10
height = 5
area = width * height
print(area)  # Displays: 50
```

## Order of Operations

Python follows the same math rules you learned in school (PEMDAS):

1. **P**arentheses `()`
2. **E**xponents `**`
3. **M**ultiplication and **D**ivision `*` `/`
4. **A**ddition and **S**ubtraction `+` `-`

```python
result = 2 + 3 * 4
print(result)  # Displays: 14 (not 20!)

result = (2 + 3) * 4
print(result)  # Displays: 20
```

## Special Operators

### Floor Division (`//`)
Regular division but throws away the decimal:

```python
result = 10 / 3
print(result)  # Displays: 3.3333...

result = 10 // 3
print(result)  # Displays: 3
```

### Modulus (`%`)
Gives you the remainder after division:

```python
result = 10 % 3
print(result)  # Displays: 1
# Because 10 ÷ 3 = 3 remainder 1
```

This is useful for checking if numbers are even or odd:

```python
number = 7
remainder = number % 2
print(remainder)  # Displays: 1 (odd number)
```

## Combining Operations

You can do complex calculations:

```python
# Calculate the area of a circle
# Formula: π × radius²
radius = 5
pi = 3.14159
area = pi * (radius ** 2)
print(area)  # Displays: 78.53975
```

## Common Mistakes

❌ Forgetting order of operations:
```python
result = 2 + 3 * 4  # Result is 14, not 20
```

✓ Using parentheses to be clear:
```python
result = (2 + 3) * 4  # Result is 20
```

❌ Using commas in numbers:
```python
number = 1,000  # Wrong! This creates a tuple
```

✓ No commas needed:
```python
number = 1000  # Correct!
```

## Key Concepts

**Operator**: A symbol that performs an operation (+, -, *, /)

**Expression**: A combination of values and operators that produces a result

**PEMDAS**: The order in which operations are performed

Now try the exercise to practice what you've learned!
