---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Trying to Modify Global Variables Without the `global` Keyword**
```python
# WRONG - Creates a NEW local variable instead of modifying global
counter = 0

def increment():
    counter = counter + 1  # UnboundLocalError!

# CORRECT - Use the global keyword
counter = 0

def increment():
    global counter
    counter = counter + 1  # Now modifies the global
```

**2. Assuming Local Variables Persist Between Function Calls**
```python
# WRONG - Expecting the counter to accumulate
def count_calls():
    counter = 0  # Created fresh EVERY time!
    counter += 1
    return counter

print(count_calls())  # 1
print(count_calls())  # 1 (not 2!)

# CORRECT - Use global or return values to persist state
call_count = 0

def count_calls():
    global call_count
    call_count += 1
    return call_count
```

**3. Shadowing Built-in Functions with Variable Names**
```python
# WRONG - Overwriting built-in functions
list = [1, 2, 3]  # Now 'list()' function is gone!
print = "Hello"   # Now 'print()' function is gone!

new_list = list("abc")  # TypeError: 'list' object is not callable

# CORRECT - Use descriptive names that don't shadow built-ins
my_list = [1, 2, 3]
message = "Hello"
```

**4. Confusing Local and Global Variables with Same Name**
```python
# WRONG - Confusion about which variable is being used
x = 10

def confusing():
    print(x)  # UnboundLocalError! Python sees assignment below
    x = 5     # This makes x local to the entire function

# CORRECT - Be explicit about your intentions
x = 10

def clear_local():
    local_x = 5  # Different name = no confusion
    print(local_x)

def clear_global():
    global x
    print(x)  # Reads global
    x = 5     # Modifies global
```

**5. Mutable Default Arguments (Advanced Scope Issue)**
```python
# WRONG - Mutable default is shared across all calls!
def add_item(item, items=[]):
    items.append(item)
    return items

print(add_item("a"))  # ['a']
print(add_item("b"))  # ['a', 'b'] - Unexpected!

# CORRECT - Use None as default, create new list inside
def add_item(item, items=None):
    if items is None:
        items = []
    items.append(item)
    return items
```