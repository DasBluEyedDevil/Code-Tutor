---
type: "THEORY"
title: "Syntax Breakdown"
---

### The break Statement:
```
for item in sequence:
    if condition:
        break  # Exit loop immediately
    # Rest of loop body
# Code here runs after break

```
**What break does:**

- Immediately exits the innermost loop
- Skips any remaining iterations
- Continues with code after the loop

#### Visual Flow:
```
for num in range(1, 6):
    if num == 3:
        break  # ← Exit here
    print(num)
print("Done")

# Output:
# 1
# 2
# Done
# (Never prints 3, 4, 5 - loop exited early)

```
### The continue Statement:
```
for item in sequence:
    if condition:
        continue  # Skip rest, go to next iteration
    # This code is skipped when continue runs
    process(item)

```
**What continue does:**

- Skips remaining code in current iteration
- Jumps to the next iteration
- Loop continues normally

#### Visual Flow:
```
for num in range(1, 6):
    if num == 3:
        continue  # ← Skip when num is 3
    print(num)

# Output:
# 1
# 2
# 4  (skipped 3!)
# 5
# (Loop continued, just skipped one iteration)

```
### The pass Statement:
```
for item in sequence:
    if condition:
        pass  # Do nothing (placeholder)
    else:
        process(item)

```
**What pass does:**

- Literally nothing - it's a placeholder
- Used when syntax requires a statement but you have nothing to do
- Common in early development ("TODO: implement this later")

#### Example Uses:
```
# Placeholder for future code:
for item in data:
    if item.needs_special_handling():
        pass  # TODO: Add special handling
    else:
        process(item)

# Empty function (syntax requires a body):
def coming_soon():
    pass  # Will implement later

# Empty if block:
if condition:
    pass  # Nothing to do for this case
else:
    do_something()

```
### Loop else Clause (Advanced):
```
for item in sequence:
    if found_what_we_need:
        break
else:
    # Runs ONLY if loop completed without break
    print("Didn't find it")

```
**How it works:**

- else block runs if loop finishes normally (no break)
- else block is skipped if break was executed
- Useful for search operations

#### Example - Searching:
```
students = ["Alice", "Bob", "Charlie"]
search_for = "David"

for student in students:
    if student == search_for:
        print(f"Found {search_for}!")
        break
else:
    # Only runs if we never broke
    print(f"{search_for} not in class")

# Output: David not in class

```
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Scenario</th><th>break executed?</th><th>else runs?</th></tr><tr><td>Found "Alice"</td><td>Yes</td><td>No</td></tr><tr><td>Found "David" (not in list)</td><td>No</td><td>Yes</td></tr></table>### break vs continue vs return:
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Statement</th><th>Exits</th><th>Effect</th></tr><tr><td>`break`</td><td>Loop</td><td>Exit loop, continue after it</td></tr><tr><td>`continue`</td><td>Current iteration</td><td>Skip to next iteration</td></tr><tr><td>`return`</td><td>Function</td><td>Exit function entirely (Module 6)</td></tr><tr><td>`pass`</td><td>Nothing</td><td>Do nothing, continue normally</td></tr></table>### Common Patterns:
#### 1. Early Exit (break)
```
# Search and stop when found
for item in large_list:
    if item == target:
        print("Found it!")
        break  # No need to keep searching

```
#### 2. Skip Invalid Data (continue)
```
# Process only valid entries
for entry in data:
    if not entry.is_valid():
        continue  # Skip invalid entries
    process(entry)  # Only valid ones reach here

```
#### 3. User-Controlled Loop (break)
```
# Infinite loop until user quits
while True:
    action = input("Command: ")
    if action == "quit":
        break
    handle(action)

```
#### 4. Flag Alternative (else)
```
# Without else (old way):
found = False
for item in items:
    if item == target:
        found = True
        break
if not found:
    print("Not found")

# With else (cleaner):
for item in items:
    if item == target:
        break
else:
    print("Not found")

```
### Common Mistakes:

<li>**Using break/continue outside loops**:```
# ERROR:
if condition:
    break  # SyntaxError! break only works in loops

```
</li><li>**Forgetting break in infinite loops**:```
# INFINITE LOOP:
while True:
    print("Running forever!")
    # Forgot break statement!

# CORRECT:
while True:
    if should_stop:
        break

```
</li><li>**Confusing break with return**:```
def search(items):
    for item in items:
        if item == target:
            break  # Exits loop, but stays in function
    print("After loop")  # This still runs
    
    # Use return to exit function:
    for item in items:
        if item == target:
            return item  # Exits function entirely

```
</li>