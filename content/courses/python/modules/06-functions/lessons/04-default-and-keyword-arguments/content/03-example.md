---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
=== Default Arguments ===
Hello, Alice!
Hi, Bob!
Hey there, Charlie!

=== Multiple Defaults ===
[INFO] Application started
[WARNING] Low memory
[ERROR] Connection failed
[DEBUG] Variable x = 42

=== Keyword Arguments ===
Order #1: burger with fries, Drink: cola
Order #2: pizza with salad, Drink: water
Order #3: tacos with chips, Drink: lemonade

=== Mixing Positional and Keyword ===
Sending email...
  To: alice@example.com
  Subject: Hello!
  Body: Just saying hi...
  CC: None
  Priority: normal

Sending email...
  To: bob@example.com
  Subject: Urgent!
  Body: Please respond ASAP...
  CC: manager@example.com
  Priority: high
```

```python
# Default and Keyword Arguments

print("=== Default Arguments ===")

# 'greeting' has a default value
def greet(name, greeting="Hello"):
    print(f"{greeting}, {name}!")

greet("Alice")              # Uses default greeting
greet("Bob", "Hi")          # Overrides default
greet("Charlie", "Hey there")  # Different greeting

print("\n=== Multiple Defaults ===")

def log_message(message, level="INFO", prefix=""):
    print(f"[{level}] {prefix}{message}")

log_message("Application started")               # Default level
log_message("Low memory", "WARNING")            # Custom level
log_message("Connection failed", "ERROR")       # Another level
log_message("Variable x = 42", "DEBUG")         # Debug message

print("\n=== Keyword Arguments ===")

def create_order(main_dish, side="fries", drink="water"):
    print(f"Order: {main_dish} with {side}, Drink: {drink}")

# Using positional arguments
print("Order #1: ", end="")
create_order("burger", "fries", "cola")

# Using keyword arguments - order doesn't matter!
print("Order #2: ", end="")
create_order(drink="water", main_dish="pizza", side="salad")

# Mix: positional first, then keyword
print("Order #3: ", end="")
create_order("tacos", side="chips", drink="lemonade")

print("\n=== Mixing Positional and Keyword ===")

def send_email(to, subject, body, cc=None, priority="normal"):
    print("Sending email...")
    print(f"  To: {to}")
    print(f"  Subject: {subject}")
    print(f"  Body: {body}")
    print(f"  CC: {cc}")
    print(f"  Priority: {priority}")

# Simple call - only required arguments
send_email("alice@example.com", "Hello!", "Just saying hi...")

print()  # Blank line

# Call with some keyword arguments
send_email(
    "bob@example.com",
    "Urgent!",
    "Please respond ASAP...",
    cc="manager@example.com",
    priority="high"
)
```
