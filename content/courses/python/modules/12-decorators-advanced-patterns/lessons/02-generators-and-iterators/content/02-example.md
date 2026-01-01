---
type: "EXAMPLE"
title: "Code Example: Generators with yield"
---

**yield keyword:**
- Pauses function execution
- Returns value to caller
- Remembers state
- Resumes on next iteration

**Generator features:**
- `next(gen)`: Get next value
- `gen.send(value)`: Send value to generator
- `gen.close()`: Stop generator
- One-time use (exhausted after iteration)

**Memory comparison:**
```python
list(range(1000000))      # 8MB+ memory
(x for x in range(1000000))  # ~128 bytes
```

```python
# Regular function returns list (all at once)
def count_up_to_list(n):
    """Returns list of numbers - uses memory"""
    result = []
    for i in range(1, n + 1):
        result.append(i)
    return result

# Generator function uses yield (one at a time)
def count_up_to_generator(n):
    """Yields numbers one by one - memory efficient"""
    for i in range(1, n + 1):
        print(f"  Generating {i}")
        yield i

print("=== List vs Generator ===")
print("\nList (all at once):")
list_result = count_up_to_list(5)
print(f"Type: {type(list_result)}")
print(f"Values: {list_result}")
print(f"Can iterate again: {list(list_result)}")

print("\nGenerator (one at a time):")
gen_result = count_up_to_generator(5)
print(f"Type: {type(gen_result)}")
print("Iterating through generator:")
for num in gen_result:
    print(f"    Got: {num}")

print("\nTrying to iterate again (generator exhausted):")
print(f"List: {list(gen_result)}")

print("\n=== Practical Example: Reading Large File ===")

def read_file_list(filename):
    """Reads entire file into memory"""
    with open(filename) as f:
        return f.readlines()  # All lines at once

def read_file_generator(filename):
    """Yields lines one at a time"""
    with open(filename) as f:
        for line in f:
            yield line.strip()

# Create test file
with open('test.txt', 'w') as f:
    for i in range(5):
        f.write(f"Line {i + 1}\n")

print("\nUsing generator to read file:")
for line in read_file_generator('test.txt'):
    print(f"  {line}")

print("\n=== Generator with State ===")

def fibonacci_generator(limit):
    """Generate Fibonacci sequence"""
    a, b = 0, 1
    count = 0
    while count < limit:
        yield a
        a, b = b, a + b
        count += 1

print("Fibonacci numbers:")
for num in fibonacci_generator(10):
    print(num, end=" ")
print()

print("\n=== Infinite Generator ===")

def infinite_counter(start=0):
    """Infinite sequence - only possible with generators!"""
    while True:
        yield start
        start += 1

print("First 10 from infinite counter:")
counter = infinite_counter(100)
for _ in range(10):
    print(next(counter), end=" ")
print()

print("\n=== Generator with Send ===")

def echo_generator():
    """Generator that can receive values"""
    while True:
        received = yield
        if received:
            print(f"  Received: {received}")
            yield f"Echo: {received}"

echo = echo_generator()
next(echo)  # Prime the generator
response = echo.send("Hello")
print(f"  {response}")
next(echo)
response = echo.send("World")
print(f"  {response}")

import os
os.remove('test.txt')
```
