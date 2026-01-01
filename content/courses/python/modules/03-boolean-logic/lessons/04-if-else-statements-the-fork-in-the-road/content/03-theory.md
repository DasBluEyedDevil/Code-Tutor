---
type: "THEORY"
title: "Syntax Breakdown"
---

### if-else Statement Anatomy:
```
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

<li>**The if block** (same as before):```
if condition:  # Check if True
    code       # Runs if True

```
</li><li>**The else keyword**:```
else:  # Note: no condition! No parentheses!

```

- Must be at the same indentation level as its `if`
- Has a colon (:) just like if
- **No condition** - else means "everything else"

</li><li>**The else block**:```
else:
    code  # Runs if condition is False

```

- Indented just like the if block (4 spaces)
- Can have multiple statements

</li>
### How Python Reads if-else:

- Evaluate the condition (True or False?)
- If **True** → Execute if block, **skip else block**
- If **False** → **Skip if block**, execute else block
- Continue with code after the if-else structure

#### Visual Flow:
```
age = 15

if age >= 18:
    print("Adult")   ← Skipped (False)
else:
    print("Minor")   ← Executed (because if was False)

print("Done")       ← Always runs

# Output:
# Minor
# Done

```
### Key Differences: if vs if-else vs Multiple if:
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Pattern</th><th>Behavior</th><th>When to Use</th></tr><tr><td>**if only**<pre>if condition:
    code</pre></td><td>Code runs if True, nothing happens if False</td><td>When False case needs no action</td></tr><tr><td>**if-else**<pre>if condition:
    code1
else:
    code2</pre></td><td>Exactly ONE block always runs</td><td>Binary decisions (two mutually exclusive options)</td></tr><tr><td>**Multiple if**<pre>if cond1:
    code1
if cond2:
    code2</pre></td><td>Each check is independent; multiple blocks can run</td><td>Multiple independent conditions</td></tr></table>#### Example Comparison:
```
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
```
if user_input == "yes":
    proceed()
else:
    cancel()

```
#### 2. Pass/Fail
```
if score >= 60:
    print("PASS")
else:
    print("FAIL")

```
#### 3. Even/Odd
```
if number % 2 == 0:
    print("Even")
else:
    print("Odd")

```
#### 4. Toggle/Switch
```
if is_on:
    turn_off()
else:
    turn_on()

```
#### 5. Validation
```
if len(password) >= 8:
    accept_password()
else:
    reject_password()

```
### Common Mistakes:

<li>**Putting a condition on else**:```
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
</li><li>**Wrong indentation**:```
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
</li><li>**Missing colon on else**:```
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
</li><li>**Using when you need elif**:```
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
</li>