---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Printing Instead of Returning Values**
```python
# WRONG - Function prints but doesn't return
def calculate_tax(amount, rate=0.1):
    tax = amount * rate
    print(f"Tax: ${tax}")  # Prints, returns None!

result = calculate_tax(100)  # result is None!
total = 100 + result  # TypeError: can't add None!

# CORRECT - Return the value, let caller decide what to do
def calculate_tax(amount, rate=0.1):
    return amount * rate

result = calculate_tax(100)
print(f"Tax: ${result}")  # Caller prints if needed
total = 100 + result  # Works!
```

**2. Not Handling Edge Cases**
```python
# WRONG - Crashes on edge cases
def average(numbers):
    return sum(numbers) / len(numbers)  # ZeroDivisionError if empty!

def get_first(items):
    return items[0]  # IndexError if empty!

# CORRECT - Handle edge cases gracefully
def average(numbers):
    if not numbers:
        return 0.0  # Or raise ValueError with helpful message
    return sum(numbers) / len(numbers)

def get_first(items, default=None):
    return items[0] if items else default
```

**3. Modifying Input Arguments Unexpectedly**
```python
# WRONG - Modifies the original list!
def add_item(items, item):
    items.append(item)  # Changes caller's list!
    return items

my_list = [1, 2, 3]
result = add_item(my_list, 4)
print(my_list)  # [1, 2, 3, 4] - Original changed!

# CORRECT - Create a new list
def add_item(items, item):
    return items + [item]  # Returns new list

# OR be explicit that it modifies in-place
def add_item_inplace(items, item):
    """Modifies items in-place and returns None."""
    items.append(item)
    # No return - convention for in-place operations
```

**4. Functions That Do Too Much**
```python
# WRONG - Does too many things
def process_user(name, email, save_to_db=True, send_email=True):
    user = create_user(name, email)
    if save_to_db:
        database.save(user)
    if send_email:
        send_welcome_email(user)
    return user

# CORRECT - Single responsibility
def create_user(name, email):
    return {"name": name, "email": email}

def save_user(user):
    database.save(user)

def send_welcome_email(user):
    # Send email logic
    pass

# Caller composes as needed
user = create_user("Alice", "alice@example.com")
save_user(user)
send_welcome_email(user)
```

**5. Unclear Function Names and Missing Docstrings**
```python
# WRONG - What does this do?
def proc(d, k):
    return d.get(k, 0) + 1

def calc(x, y, z):
    return x * y / z if z else 0

# CORRECT - Clear names and docstrings
def get_count_plus_one(data, key):
    """Get value for key from dict, defaulting to 0, then add 1."""
    return data.get(key, 0) + 1

def calculate_rate(quantity, price, time_period):
    """Calculate rate as (quantity * price) / time_period.
    
    Returns 0 if time_period is zero to avoid division error.
    """
    if time_period == 0:
        return 0
    return quantity * price / time_period
```