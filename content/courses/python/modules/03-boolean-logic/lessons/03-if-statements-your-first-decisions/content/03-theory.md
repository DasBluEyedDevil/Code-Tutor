---
type: "THEORY"
title: "Syntax Breakdown"
---

### if Statement Anatomy:
```
if condition:
    statement1
    statement2
    statement3

```
#### Breaking It Down:

<li>**The keyword**: `if` (lowercase!)```
if  # Correct
IF  # ERROR! Python is case-sensitive
If  # ERROR! Must be lowercase

```
</li><li>**The condition**: Any Boolean expression```
if age >= 18:           # Comparison
if is_valid:            # Boolean variable
if score > 90 and passed_exam:  # Logical operators
if temperature > 32:    # Any expression that's True/False

```
</li><li>**The colon (:)**: REQUIRED! Tells Python "here comes the code block"```
if age >= 18:  # Correct - has colon
if age >= 18   # ERROR! Missing colon

```
</li><li>**Indentation**: The code inside the if block MUST be indented```
# Correct - 4 spaces (or 1 tab)
if temperature > 100:
    print("Boiling")  # Indented
    print("Hot!")     # Indented

# ERROR - not indented
if temperature > 100:
print("Boiling")  # IndentationError!

```
</li>
### How Python Reads an if Statement:

- Evaluate the condition (is it True or False?)
- If True → Execute all indented code
- If False → Skip all indented code
- Continue with non-indented code after the block

#### Visual Flow:
```
if temperature > 100:
    print("A")  ← Runs only if True
    print("B")  ← Runs only if True
print("C")      ← Always runs (not indented!)

# If temperature = 105 (True):
# Output: A, B, C

# If temperature = 75 (False):
# Output: C

```
### Indentation Rules (CRITICAL!):
Python uses **indentation** to group code, not curly braces {} like other languages:

```
# Standard: 4 spaces per indentation level
if condition:
    statement1     # 4 spaces
    statement2     # 4 spaces
next_statement     # 0 spaces (not part of if)

```
**Common indentation mistakes:**

```
# WRONG - mixing tabs and spaces (big no-no!)
if x > 10:
    print("A")    # Tab
    print("B")    # 4 spaces  ← Can cause hard-to-see errors!

# WRONG - inconsistent spacing
if x > 10:
  print("A")      # 2 spaces
    print("B")    # 4 spaces  ← IndentationError!

# CORRECT - consistent 4 spaces
if x > 10:
    print("A")    # 4 spaces
    print("B")    # 4 spaces

```
**Best practice:** Configure your editor to insert 4 spaces when you press Tab.

### Multiple if Statements:
You can have multiple separate if statements - each is independent:

```
score = 85

if score >= 90:
    print("A grade")  # Not executed (85 < 90)

if score >= 80:
    print("B grade")  # Executed! (85 >= 80)

if score >= 70:
    print("C grade")  # Also executed! (85 >= 70)

# Output:
# B grade
# C grade

```
**Important:** These are separate checks! Both the 80 and 70 checks run because they're independent if statements. Later you'll learn `elif` to make them mutually exclusive.

### Common Mistakes and Solutions:

<li>**Missing colon**:```
# WRONG:
if age >= 18
    print("Adult")
# SyntaxError: invalid syntax

# CORRECT:
if age >= 18:
    print("Adult")

```
</li><li>**Using = instead of ==**:```
# WRONG:
if age = 18:  # This tries to ASSIGN, not compare!
    print("Adult")
# SyntaxError: invalid syntax

# CORRECT:
if age == 18:  # Comparison
    print("Adult")

```
</li><li>**Forgetting indentation**:```
# WRONG:
if score > 90:
print("Great!")  # IndentationError!

# CORRECT:
if score > 90:
    print("Great!")

```
</li><li>**Indenting code that shouldn't be**:```
if temperature > 100:
    print("Boiling")   # Runs only if True
    print("Done")      # Also only if True!

# Probably wanted:
if temperature > 100:
    print("Boiling")   # Runs only if True
print("Done")          # Always runs

```
</li>