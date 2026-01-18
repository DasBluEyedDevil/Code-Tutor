---
type: "EXAMPLE"
title: "Code Example: match/case Patterns"
---

**Expected Output:**
```
=== Command System ===
Available commands: start, stop, restart, quit, help
Enter command: start
Starting the application...

=== Matching with | (OR patterns) ===
User said 'y' -> Proceeding...
User said 'no' -> Cancelled.

=== Destructuring Tuples ===
Point (3, 4) is at origin? False
Moving to (3, 4)

Point (0, 0) is at origin? True
At the origin!

=== Guards (if conditions) ===
Temperature: 75 -> Nice weather!
Temperature: 95 -> Too hot!

=== Matching Sequences (Lists) ===
Enter a command like 'go north' or 'take sword':
Command: go north
Moving north...
```

```python
# Pattern Matching with match/case (Python 3.10+)

print("=== Command System ===")
print("Available commands: start, stop, restart, quit, help")

# Interactive input
command = input("Enter command: ").lower().strip()

match command:
    case "start":
        print("Starting the application...")
    case "stop":
        print("Stopping the application...")
    case "restart":
        print("Restarting...")
    case "quit" | "exit":  # Match multiple values with |
        print("Goodbye!")
    case "help":
        print("Help: Enter a command to run.")
    case _:  # Wildcard: matches anything else
        print(f"Unknown command: {command}")

print()
print("=== Matching with | (OR patterns) ===")

def get_confirmation(response):
    match response.lower():
        case "y" | "yes" | "yeah" | "yep":
            return "Proceeding..."
        case "n" | "no" | "nope":
            return "Cancelled."
        case _:
            return "Please answer yes or no."

# Demonstration
print(f"User said 'y' -> {get_confirmation('y')}")
print(f"User said 'no' -> {get_confirmation('no')}")
print()

print("=== Destructuring Tuples ===")

def describe_point(point):
    match point:
        case (0, 0):
            return "At the origin!"
        case (0, y):  # x is 0, capture y
            return f"On the y-axis at {y}"
        case (x, 0):  # y is 0, capture x
            return f"On the x-axis at {x}"
        case (x, y):  # Capture both values
            return f"Moving to ({x}, {y})"
        case _:
            return "Not a valid point"

points = [(3, 4), (0, 0), (0, 5)]
for p in points:
    print(f"Point {p} is at origin? {p == (0, 0)}")
    print(describe_point(p))
    print()

print("=== Guards (if conditions) ===")

def describe_temperature(temp):
    match temp:
        case t if t < 32:
            return "Freezing!"
        case t if t < 50:
            return "Cold"
        case t if t < 70:
            return "Cool"
        case t if t < 85:
            return "Nice weather!"
        case _:
            return "Too hot!"

for temp in [75, 95]:
    print(f"Temperature: {temp} -> {describe_temperature(temp)}")

print()
print("=== Matching Sequences (Lists) ===")
print("Enter a command like 'go north' or 'take sword':")
# Split input into a list of words
cmd_parts = input("Command: ").lower().split()

match cmd_parts:
    case ["go", direction]:
        print(f"Moving {direction}...")
    case ["take", item]:
        print(f"Taking {item}...")
    case [single_cmd]:
        print(f"Single command: {single_cmd}")
    case _:
        print("I don't understand that structure.")
```
