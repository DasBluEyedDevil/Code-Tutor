---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
=== Creating Dictionaries ===
{'name': 'Alice', 'age': 30, 'city': 'NYC'}
{'x': 10, 'y': 20}

=== Accessing Values ===
Name: Alice
Age: 30
Email with get(): None
Email with default: No email provided

=== Modifying Dictionaries ===
Original: {'name': 'Bob', 'age': 25}
After adding email: {'name': 'Bob', 'age': 25, 'email': 'bob@example.com'}
After updating age: {'name': 'Bob', 'age': 26, 'email': 'bob@example.com'}
After deleting email: {'name': 'Bob', 'age': 26}

=== Checking Keys ===
Has 'name'? True
Has 'phone'? False
Number of items: 2

=== Practical Example: Product Inventory ===
Apple costs: $1.50
Banana costs: $0.75
Mango costs: Not in stock
```

```python
# Dictionary Basics - Key-Value Pairs

print("=== Creating Dictionaries ===")

# Using curly braces
person = {"name": "Alice", "age": 30, "city": "NYC"}
print(person)

# Using dict() with keyword arguments
coordinates = dict(x=10, y=20)
print(coordinates)

print("\n=== Accessing Values ===")

person = {"name": "Alice", "age": 30, "city": "NYC"}

# Using square brackets
print(f"Name: {person['name']}")
print(f"Age: {person['age']}")

# Using get() - safer for potentially missing keys
print(f"Email with get(): {person.get('email')}")
print(f"Email with default: {person.get('email', 'No email provided')}")

print("\n=== Modifying Dictionaries ===")

user = {"name": "Bob", "age": 25}
print(f"Original: {user}")

# Add a new key-value pair
user["email"] = "bob@example.com"
print(f"After adding email: {user}")

# Update an existing value
user["age"] = 26
print(f"After updating age: {user}")

# Delete a key-value pair
del user["email"]
print(f"After deleting email: {user}")

print("\n=== Checking Keys ===")

person = {"name": "Alice", "age": 30}

# Check if a key exists
print(f"Has 'name'? {'name' in person}")
print(f"Has 'phone'? {'phone' in person}")

# Get number of key-value pairs
print(f"Number of items: {len(person)}")

print("\n=== Practical Example: Product Inventory ===")

product_prices = {
    "apple": 1.50,
    "banana": 0.75,
    "orange": 2.00,
    "grape": 3.50
}

# Look up prices
print(f"Apple costs: ${product_prices['apple']:.2f}")
print(f"Banana costs: ${product_prices.get('banana'):.2f}")
print(f"Mango costs: {product_prices.get('mango', 'Not in stock')}")
```
