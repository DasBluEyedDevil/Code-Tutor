---
type: "EXAMPLE"
title: "Code Example: Common Exception Types in Action"
---

Each exception type represents a specific category of error. By catching specific exceptions, you can provide tailored error messages and recovery strategies. The function demonstrates how a single try block can have multiple except blocks to handle different error scenarios.

```python
# Demonstrating common Python exception types
print("=== Common Exception Types ===")

# 1. ValueError - Wrong value, right type
print("\n1. ValueError Example:")
try:
    number = int("not_a_number")  # String to int, but invalid format
except ValueError as e:
    print(f"ValueError caught: {e}")
    print("Cause: Trying to convert invalid string to integer\n")

# 2. TypeError - Wrong type entirely
print("2. TypeError Example:")
try:
    result = "hello" + 5  # Can't add string and integer
except TypeError as e:
    print(f"TypeError caught: {e}")
    print("Cause: Incompatible types in operation\n")

# 3. IndexError - List index out of range
print("3. IndexError Example:")
try:
    my_list = [1, 2, 3]
    item = my_list[10]  # Only indices 0-2 exist
except IndexError as e:
    print(f"IndexError caught: {e}")
    print("Cause: Accessing index that doesn't exist\n")

# 4. KeyError - Dictionary key doesn't exist
print("4. KeyError Example:")
try:
    person = {"name": "Alice", "age": 25}
    email = person["email"]  # Key 'email' doesn't exist
except KeyError as e:
    print(f"KeyError caught: {e}")
    print("Cause: Accessing dictionary key that doesn't exist\n")

# 5. ZeroDivisionError - Division by zero
print("5. ZeroDivisionError Example:")
try:
    result = 10 / 0
except ZeroDivisionError as e:
    print(f"ZeroDivisionError caught: {e}")
    print("Cause: Attempting to divide by zero\n")

# 6. FileNotFoundError - File doesn't exist
print("6. FileNotFoundError Example:")
try:
    with open("nonexistent_file.txt", "r") as f:
        content = f.read()
except FileNotFoundError as e:
    print(f"FileNotFoundError caught: {e}")
    print("Cause: Trying to open a file that doesn't exist\n")

# 7. AttributeError - Object doesn't have that attribute
print("7. AttributeError Example:")
try:
    my_list = [1, 2, 3]
    my_list.append_all([4, 5])  # Method doesn't exist
except AttributeError as e:
    print(f"AttributeError caught: {e}")
    print("Cause: Calling a method/attribute that doesn't exist\n")

# Handling MULTIPLE exception types
print("=== Handling Multiple Exceptions ===")

def process_user_input(user_input, index):
    """Process input with multiple exception handling."""
    numbers = [10, 20, 30, 40, 50]
    
    try:
        # Multiple things can go wrong here!
        num = int(user_input)  # ValueError if input not a number
        result = numbers[num]  # IndexError if num out of range
        division = result / index  # ZeroDivisionError if index is 0
        return division
        
    except ValueError:
        print(f"Error: '{user_input}' is not a valid number")
        return None
        
    except IndexError:
        print(f"Error: Index {num} out of range (0-{len(numbers)-1})")
        return None
        
    except ZeroDivisionError:
        print("Error: Cannot divide by zero")
        return None

print("\nTest 1: Valid input")
result1 = process_user_input("2", 5)
print(f"Result: {result1}\n")

print("Test 2: Invalid number format")
result2 = process_user_input("abc", 5)
print(f"Result: {result2}\n")

print("Test 3: Index out of range")
result3 = process_user_input("10", 5)
print(f"Result: {result3}\n")

print("Test 4: Division by zero")
result4 = process_user_input("2", 0)
print(f"Result: {result4}")
```
