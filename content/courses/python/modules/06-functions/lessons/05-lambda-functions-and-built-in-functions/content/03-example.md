---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
=== Lambda Basics ===
Double 5: 10
Square 4: 16
Add 3 + 7: 10

=== Lambdas with sorted() ===
Original: ['banana', 'apple', 'cherry', 'date']
Sorted by length: ['date', 'apple', 'banana', 'cherry']
Sorted by last letter: ['banana', 'apple', 'date', 'cherry']

=== Built-in Number Functions ===
abs(-10) = 10
round(3.14159, 2) = 3.14
min(5, 2, 8, 1) = 1
max(5, 2, 8, 1) = 8
sum([1, 2, 3, 4, 5]) = 15

=== map() with Lambda ===
Original: [1, 2, 3, 4, 5]
Doubled: [2, 4, 6, 8, 10]
Squared: [1, 4, 9, 16, 25]

=== filter() with Lambda ===
Original: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
Even numbers: [2, 4, 6, 8, 10]
Greater than 5: [6, 7, 8, 9, 10]

=== Combining map and filter ===
Original: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
Squares of even numbers: [4, 16, 36, 64, 100]
```

```python
# Lambda Functions and Built-in Functions

print("=== Lambda Basics ===")

# Simple lambdas
double = lambda x: x * 2
square = lambda x: x ** 2
add = lambda a, b: a + b

print(f"Double 5: {double(5)}")
print(f"Square 4: {square(4)}")
print(f"Add 3 + 7: {add(3, 7)}")

print("\n=== Lambdas with sorted() ===")

words = ["banana", "apple", "cherry", "date"]
print(f"Original: {words}")

# Sort by length using lambda
by_length = sorted(words, key=lambda word: len(word))
print(f"Sorted by length: {by_length}")

# Sort by last letter
by_last_letter = sorted(words, key=lambda word: word[-1])
print(f"Sorted by last letter: {by_last_letter}")

print("\n=== Built-in Number Functions ===")

print(f"abs(-10) = {abs(-10)}")
print(f"round(3.14159, 2) = {round(3.14159, 2)}")
print(f"min(5, 2, 8, 1) = {min(5, 2, 8, 1)}")
print(f"max(5, 2, 8, 1) = {max(5, 2, 8, 1)}")
print(f"sum([1, 2, 3, 4, 5]) = {sum([1, 2, 3, 4, 5])}")

print("\n=== map() with Lambda ===")

numbers = [1, 2, 3, 4, 5]
print(f"Original: {numbers}")

# Double each number
doubled = list(map(lambda x: x * 2, numbers))
print(f"Doubled: {doubled}")

# Square each number
squared = list(map(lambda x: x ** 2, numbers))
print(f"Squared: {squared}")

print("\n=== filter() with Lambda ===")

numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
print(f"Original: {numbers}")

# Keep only even numbers
evens = list(filter(lambda x: x % 2 == 0, numbers))
print(f"Even numbers: {evens}")

# Keep numbers greater than 5
greater_than_5 = list(filter(lambda x: x > 5, numbers))
print(f"Greater than 5: {greater_than_5}")

print("\n=== Combining map and filter ===")

numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
print(f"Original: {numbers}")

# Get squares of even numbers only
evens = filter(lambda x: x % 2 == 0, numbers)
squares_of_evens = list(map(lambda x: x ** 2, evens))
print(f"Squares of even numbers: {squares_of_evens}")
```
