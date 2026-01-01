---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
=== Functions with Parameters ===
Hello, Alice!
Hello, Bob!
Hello, Charlie!

=== Functions with Return Values ===
5 + 3 = 8
10 - 4 = 6
7 * 6 = 42

=== Multiple Parameters ===
Full name: Alice Smith
Email: alice@company.com

=== Using Return Values ===
Area of 5x3 rectangle: 15
Total area: 37
Circle area (radius 5): 78.54

=== Return vs. Print ===
This function prints: Hello!
print_greeting returned: None
add_numbers returned: 8
```

```python
# Functions with Parameters and Return Values

print("=== Functions with Parameters ===")

# A function with ONE parameter
def greet(name):
    print(f"Hello, {name}!")

greet("Alice")
greet("Bob")
greet("Charlie")

print("\n=== Functions with Return Values ===")

# Functions that RETURN values instead of printing
def add(a, b):
    return a + b

def subtract(a, b):
    return a - b

def multiply(a, b):
    return a * b

# Using the returned values
sum_result = add(5, 3)
diff_result = subtract(10, 4)
prod_result = multiply(7, 6)

print(f"5 + 3 = {sum_result}")
print(f"10 - 4 = {diff_result}")
print(f"7 * 6 = {prod_result}")

print("\n=== Multiple Parameters ===")

def create_full_name(first, last):
    return f"{first} {last}"

def create_email(username, domain):
    return f"{username}@{domain}"

name = create_full_name("Alice", "Smith")
email = create_email("alice", "company.com")

print(f"Full name: {name}")
print(f"Email: {email}")

print("\n=== Using Return Values ===")

def calculate_area(width, height):
    return width * height

def calculate_circle_area(radius):
    pi = 3.14159
    return pi * radius * radius

# Store the result
area = calculate_area(5, 3)
print(f"Area of 5x3 rectangle: {area}")

# Use return value directly in expression
total = calculate_area(5, 3) + calculate_area(2, 11)
print(f"Total area: {total}")

# Use return value in f-string
print(f"Circle area (radius 5): {calculate_circle_area(5):.2f}")

print("\n=== Return vs. Print ===")

# This function PRINTS but returns None
def print_greeting():
    print("This function prints: Hello!")
    # No return statement = returns None

# This function RETURNS a value
def add_numbers(x, y):
    return x + y

result1 = print_greeting()  # Prints "Hello!"
result2 = add_numbers(5, 3)

print(f"print_greeting returned: {result1}")
print(f"add_numbers returned: {result2}")
```
