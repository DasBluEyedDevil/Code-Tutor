---
type: "THEORY"
title: "Syntax Breakdown"
---

### if-else Statement Anatomy:
```python
if condition:
    # Code block 1 (runs if True)
    statement1
    statement2
else:
    # Code block 2 (runs if False)
    statement3
    statement4
```

#### Breaking It Down:

- **The if block** (same as before):
  ```python
  if condition:  # Check if True
      code       # Runs if True
  ```

- **The else keyword**:
  ```python
  else:  # Note: no condition! No parentheses!
  ```
  - Must be at the same indentation level as its `if`
  - Has a colon (:) just like if
  - **No condition** - else means "everything else"

- **The else block**:
  ```python
  else:
      code  # Runs if condition is False
  ```
  - Indented just like the if block (4 spaces)
  - Can have multiple statements

### How Python Reads if-else:

1. Evaluate the condition (True or False?)
2. If **True** → Execute if block, **skip else block**
3. If **False** → **Skip if block**, execute else block
4. Continue with code after the if-else structure

#### Visual Flow:
```python
age = 15

if age >= 18:
    print("Adult")   # Skipped (False)
else:
    print("Minor")   # Executed (because if was False)

print("Done")       # Always runs

# Output:
# Minor
# Done
```

### Key Differences: if vs if-else vs Multiple if:

| Pattern | Behavior | When to Use |
| :--- | :--- | :--- |
| **if only**<br>`if condition:`<br>`    code` | Code runs if True, nothing happens if False | When False case needs no action |
| **if-else**<br>`if condition:`<br>`    code1`<br>`else:`<br>`    code2` | Exactly ONE block always runs | Binary decisions (two mutually exclusive options) |
| **Multiple if**<br>`if cond1:`<br>`    code1`<br>`if cond2:`<br>`    code2` | Each check is independent; multiple blocks can run | Multiple independent conditions |

#### Example Comparison:   
```python
# Scenario: Checking score for grade
score = 95

# WRONG for this purpose - both could print!
if score >= 90:
    print("A grade")
if score < 90:
    print("Not an A")
# Inefficient: Checks both conditions even though only one can be true

# CORRECT - mutually exclusive
if score >= 90:
    print("A grade")
else:
    print("Not an A")
# Better: Only one check needed, one guaranteed output
```

### Common Patterns:
#### 1. Binary Decision (Yes/No)
```python
if user_input == "yes":
    proceed()
else:
    cancel()
```
#### 2. Pass/Fail
```python
if score >= 60:
    print("PASS")
else:
    print("FAIL")
```
#### 3. Even/Odd
```python
if number % 2 == 0:
    print("Even")
else:
    print("Odd")
```
#### 4. Toggle/Switch
```python
if is_on:
    turn_off()
else:
    turn_on()
```
#### 5. Validation
```python
if len(password) >= 8:
    accept_password()
else:
    reject_password()
```

### Common Mistakes:

- **Putting a condition on else**:
  ```python
  # WRONG:
  if age >= 18:
      print("Adult")
  else age < 18:  # SyntaxError! No condition on else!
      print("Minor")

  # CORRECT:
  if age >= 18:
      print("Adult")
  else:  # No condition needed - else means "all other cases"
      print("Minor")
  ```

- **Wrong indentation**:
  ```python
  # WRONG:
  if age >= 18:
      print("Adult")
    else:  # IndentationError! else must align with if
      print("Minor")

  # CORRECT:
  if age >= 18:
      print("Adult")
  else:  # Same indentation level as if
      print("Minor")
  ```

- **Missing colon on else**:
  ```python
  # WRONG:
  if age >= 18:
      print("Adult")
  else  # SyntaxError! Missing colon
      print("Minor")

  # CORRECT:
  if age >= 18:
      print("Adult")
  else:  # Colon required!
      print("Minor")
  ```

- **Using when you need elif**:
  ```python
  # PROBLEM: Want to check multiple ranges
  if score >= 90:
      print("A")
  else:
      print("Not an A")  # Too broad! What about B, C, D?

  # BETTER: Use elif (next lesson!)
  if score >= 90:
      print("A")
  elif score >= 80:
      print("B")
  else:
      print("C or below")
  ```
