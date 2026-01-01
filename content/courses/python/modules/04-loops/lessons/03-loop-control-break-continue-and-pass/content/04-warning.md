---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Using break/continue Outside a Loop**
```python
# WRONG - break only works inside loops!
if condition:
    break  # SyntaxError: 'break' outside loop

# break/continue MUST be inside for or while:
for item in items:
    if condition:
        break  # This is valid
```

**2. Forgetting break in Infinite Loops**
```python
# WRONG - Runs forever!
while True:
    print("Help, I can't stop!")
    # Missing break!

# CORRECT - Always have an exit condition
while True:
    user_input = input("Continue? (y/n): ")
    if user_input == 'n':
        break  # Exit point
```

**3. Confusing break vs return**
```python
def find_item(items, target):
    for item in items:
        if item == target:
            break  # Only exits loop!
    print("This still runs!")  # After loop
    return None

# If you want to exit the function entirely:
def find_item(items, target):
    for item in items:
        if item == target:
            return item  # Exits function immediately!
    return None  # Only if not found
```

**4. Misunderstanding Loop else**
```python
# WRONG thinking: "else runs if the loop body doesn't run"
# CORRECT: else runs if loop completes WITHOUT break

for i in range(3):
    print(i)
else:
    print("Loop finished")  # This RUNS! (no break)

for i in range(3):
    if i == 1:
        break
else:
    print("Loop finished")  # This does NOT run! (break)
```

**5. continue Skips More Than Expected**
```python
# WRONG - continue skips ALL remaining code!
for num in range(5):
    if num == 2:
        continue
        print("This never prints for 2")
    print(num)  # Also skipped when num == 2

# The continue skips EVERYTHING below it in that iteration
```