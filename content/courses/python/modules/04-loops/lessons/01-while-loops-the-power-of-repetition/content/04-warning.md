---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Infinite Loops - Forgetting to Update**
```python
# DANGER - This runs forever!
count = 0
while count < 10:
    print(count)  # count never changes!
    # Missing: count = count + 1

# CORRECT
count = 0
while count < 10:
    print(count)
    count = count + 1  # Now it will eventually exit
```
**Press Ctrl+C to stop an infinite loop!**

**2. Off-By-One Errors**
```python
# Prints 1-4 (not 1-5!)
count = 1
while count < 5:  # < means "less than", not "less than or equal"
    print(count)
    count += 1

# To include 5, use <=
while count <= 5:  # Now prints 1-5
```

**3. Initializing Inside the Loop**
```python
# WRONG - Resets count every iteration!
while count < 5:
    count = 0  # Oops! Always 0, infinite loop!
    print(count)
    count += 1

# CORRECT - Initialize BEFORE the loop
count = 0
while count < 5:
    print(count)
    count += 1
```

**4. Wrong Comparison Operator**
```python
# WRONG - Loop never executes (10 is not less than 5)
count = 10
while count < 5:
    print(count)  # Never runs!

# Check your initial value matches your condition logic
```

**5. Input Without int() Conversion**
```python
# WRONG - Comparing string to number!
guess = input("Enter number: ")  # Returns string
while guess != 10:  # "10" != 10 is always True!

# CORRECT
guess = int(input("Enter number: "))  # Convert to int
while guess != 10:
```