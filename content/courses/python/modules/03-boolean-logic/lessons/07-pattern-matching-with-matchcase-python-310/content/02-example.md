---
type: "EXAMPLE"
title: "Code Example: match/case Patterns"
---

**Expected Output:**
```
=== Basic Pattern Matching ===
Command: start
Starting the application...

Command: quit
Goodbye!

Command: dance
Unknown command: dance

=== Matching with | (OR patterns) ===
User said 'y' -> Proceeding...
User said 'no' -> Cancelled.

=== Destructuring Tuples ===
Point (3, 4) is at origin? False
Moving to (3, 4)

Point (0, 0) is at origin? True
At the origin!

Point (0, 5) is on axis? y-axis at 5

=== Guards (if conditions) ===
Temperature: 75 -> Nice weather!
Temperature: 95 -> Too hot!
Temperature: 30 -> Freezing!

=== Matching Sequences ===
[1] -> Single element: 1
[1, 2] -> Two elements: 1 and 2
[1, 2, 3, 4, 5] -> First: 1, Rest: [2, 3, 4, 5]

=== Matching Dictionaries ===
{'type': 'error', 'code': 404} -> Error 404!
{'type': 'success', 'data': 'hello'} -> Success: hello
{'type': 'warning'} -> Warning (no details)
```

```python
# Pattern Matching with match/case (Python 3.10+)

print("=== Basic Pattern Matching ===")

def handle_command(command):
    match command:
        case "start":
            return "Starting the application..."
        case "stop":
            return "Stopping the application..."
        case "restart":
            return "Restarting..."
        case "quit" | "exit":  # Match multiple values with |
            return "Goodbye!"
        case _:  # Wildcard: matches anything else
            return f"Unknown command: {command}"

for cmd in ["start", "quit", "dance"]:
    print(f"Command: {cmd}")
    print(handle_command(cmd))
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

for temp in [75, 95, 30]:
    print(f"Temperature: {temp} -> {describe_temperature(temp)}")
print()

print("=== Matching Sequences ===")

def describe_list(items):
    match items:
        case []:
            return "Empty list"
        case [single]:
            return f"Single element: {single}"
        case [first, second]:
            return f"Two elements: {first} and {second}"
        case [first, *rest]:  # * captures remaining items
            return f"First: {first}, Rest: {rest}"

test_lists = [[1], [1, 2], [1, 2, 3, 4, 5]]
for lst in test_lists:
    print(f"{lst} -> {describe_list(lst)}")
print()

print("=== Matching Dictionaries ===")

def handle_response(response):
    match response:
        case {"type": "error", "code": code}:
            return f"Error {code}!"
        case {"type": "success", "data": data}:
            return f"Success: {data}"
        case {"type": "warning"}:
            return "Warning (no details)"
        case _:
            return "Unknown response format"

responses = [
    {"type": "error", "code": 404},
    {"type": "success", "data": "hello"},
    {"type": "warning"}
]

for resp in responses:
    print(f"{resp} -> {handle_response(resp)}")
```
