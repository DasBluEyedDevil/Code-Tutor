---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
=== Defining and Calling Functions ===
Hello, welcome to Python!
I hope you're having a great day!

Let's call the greeting again:
Hello, welcome to Python!
I hope you're having a great day!

=== A Function for Drawing ===
********************
* Python Functions *
********************

=== Multiple Functions Working Together ===
--- Starting the program ---
Step 1: Loading data...
Step 2: Processing data...
Step 3: Saving results...
--- Program complete! ---
```

```python
# Let's create our first functions!

print("=== Defining and Calling Functions ===")

# Define a simple greeting function
def greet():
    print("Hello, welcome to Python!")
    print("I hope you're having a great day!")

# Call the function
greet()

print("\nLet's call the greeting again:")
greet()  # Reuse it!

print("\n=== A Function for Drawing ===")

# A function that draws a decorative box
def draw_box():
    print("*" * 20)
    print("* Python Functions *")
    print("*" * 20)

draw_box()

print("\n=== Multiple Functions Working Together ===")

# Functions can be called from other functions!
def start_program():
    print("--- Starting the program ---")

def load_data():
    print("Step 1: Loading data...")

def process_data():
    print("Step 2: Processing data...")

def save_results():
    print("Step 3: Saving results...")

def end_program():
    print("--- Program complete! ---")

# Now run them in order
start_program()
load_data()
process_data()
save_results()
end_program()
```
