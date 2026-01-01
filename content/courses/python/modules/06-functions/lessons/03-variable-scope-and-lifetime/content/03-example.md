---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
=== Local Variables ===
Inside function: Hello from inside!
Error accessing local outside: name 'message' is not defined

=== Global Variables ===
Global score: 100
Inside function, reading global: 100
After function, global unchanged: 100

=== Modifying Global Variables ===
Score before: 0
Score after increment: 1
Score after add_points: 11

=== Same Name, Different Scopes ===
Global x: 10
Inside function, local x: 5
Back outside, global x still: 10

=== Variable Lifetime ===
Call 1: counter = 1
Call 2: counter = 1
Call 3: counter = 1
```

```python
# Understanding Variable Scope and Lifetime

print("=== Local Variables ===")

def create_message():
    # This variable only exists inside this function
    message = "Hello from inside!"
    print(f"Inside function: {message}")

create_message()

# Try to access 'message' outside the function
try:
    print(message)
except NameError as e:
    print(f"Error accessing local outside: {e}")

print("\n=== Global Variables ===")

# Global variable - accessible everywhere
score = 100

def show_score():
    # We can READ global variables without any special keyword
    print(f"Inside function, reading global: {score}")

print(f"Global score: {score}")
show_score()
print(f"After function, global unchanged: {score}")

print("\n=== Modifying Global Variables ===")

points = 0  # Global variable

def increment_points():
    global points  # Tell Python we want to modify the global
    points += 1

def add_points(amount):
    global points
    points += amount

print(f"Score before: {points}")
increment_points()
print(f"Score after increment: {points}")
add_points(10)
print(f"Score after add_points: {points}")

print("\n=== Same Name, Different Scopes ===")

x = 10  # Global x

def use_local_x():
    x = 5  # Local x (different variable!)
    print(f"Inside function, local x: {x}")

print(f"Global x: {x}")
use_local_x()
print(f"Back outside, global x still: {x}")

print("\n=== Variable Lifetime ===")

def count_calls():
    # This counter is created NEW each time the function runs
    counter = 0
    counter += 1
    print(f"Call {counter}: counter = {counter}")

# Each call creates a fresh 'counter' variable
count_calls()  # counter = 1
count_calls()  # counter = 1 (not 2!)
count_calls()  # counter = 1 (not 3!)
```
