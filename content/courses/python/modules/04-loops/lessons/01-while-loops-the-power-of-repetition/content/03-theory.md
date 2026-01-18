---
type: "THEORY"
title: "Syntax Breakdown"
---

### while Loop Anatomy

```python
while condition:
    # Code to repeat (loop body)
    # Must be indented!
    statement1
    statement2
    # Update variable(s) in condition!
```

#### Breaking It Down:

- **The keyword:** `while` (lowercase)
- **The condition:** Any Boolean expression
  ```python
  while count < 10:     # Comparison
  while is_running:     # Boolean variable
  while password != correct:  # Inequality check
  ```
- **The colon (:):** Required! Signals start of loop body
- **Indented body:** Code that repeats (4 spaces)
- **Variable update:** MUST change something in the condition!

### How Python Reads a while Loop

1. **Check condition:** Is it True?
2. **If True:** Execute indented code
3. **Loop back:** Return to step 1 (check again)
4. **If False:** Skip loop body, continue after loop

#### Visual Flow:

```python
count = 1             # Initialize

while count <= 3:     # <- Check: 1 <= 3? YES
    print(count)      #   Execute: prints 1
    count = count + 1 #   Update: count becomes 2
                      # <- Loop back, check: 2 <= 3? YES
    print(count)      #   Execute: prints 2
    count = count + 1 #   Update: count becomes 3
                      # <- Loop back, check: 3 <= 3? YES
    print(count)      #   Execute: prints 3
    count = count + 1 #   Update: count becomes 4
                      # <- Loop back, check: 4 <= 3? NO
                      # Exit loop

print("Done")         # Continue here
```

### The Three Essential Parts of Most Loops

1. **Initialization:** Set starting values BEFORE the loop
   ```python
   count = 1  # Initialize counter
   ```

2. **Condition:** Test that determines if loop continues
   ```python
   while count <= 5:  # Condition
   ```

3. **Update:** Change variable(s) to eventually make condition False
   ```python
   count = count + 1  # Update (progress toward exit)
   ```

**If you forget the update, you get an INFINITE LOOP!**

### Common Loop Patterns

#### 1. Counter Loop (Count N times)

```python
count = 1
while count <= 10:     # Repeat 10 times
    print(f"Iteration {count}")
    count = count + 1  # Increment
```

#### 2. Sentinel Loop (Until Specific Input)

```python
answer = ""
while answer != "quit":  # Keep going until "quit"
    answer = input("Enter command (or 'quit'): ")
    # Process answer
```

#### 3. Flag Loop (Boolean Control)

```python
keep_going = True
while keep_going:
    # Do stuff
    choice = input("Continue? (y/n): ")
    if choice == "n":
        keep_going = False  # Exit loop
```

#### 4. Validation Loop (Repeat Until Valid)

```python
age = -1
while age < 0:  # Repeat while invalid
    age = int(input("Enter age: "))
    if age < 0:
        print("Invalid!")
```

#### 5. Accumulator Loop (Build Up a Result)

```python
total = 0
count = 1
while count <= 5:
    total = total + count  # Accumulate sum
    count = count + 1
# total now equals 1+2+3+4+5 = 15
```

### Infinite Loops (Common Mistake!)

```python
# INFINITE LOOP - DON'T DO THIS!
count = 1
while count <= 5:
    print(count)
    # OOPS! Forgot to update count
    # count is always 1, so 1 <= 5 is always True
    # Loop runs forever!

# FIX:
count = 1
while count <= 5:
    print(count)
    count = count + 1  # Now it will eventually become 6 and exit
```

**Signs you have an infinite loop:**

- Program never stops running
- Same output repeats endlessly
- Computer fan gets loud (CPU working hard!)

**How to stop:** Press Ctrl+C in the terminal

### Common Mistakes

- **Missing colon**:
  ```python
  # WRONG:
  while count < 5  # Missing colon!
      print(count)

  # CORRECT:
  while count < 5:  # Has colon
      print(count)
  ```

- **Forgetting to update loop variable**:
  ```python
  # INFINITE LOOP:
  count = 0
  while count < 10:
      print(count)  # Prints 0 forever!
      # Forgot: count = count + 1
  ```

- **Wrong indentation**:
  ```python
  # WRONG:
  while count < 5:
  print(count)  # Not indented! Won't loop

  # CORRECT:
  while count < 5:
      print(count)  # Indented, part of loop
  ```

- **Initializing inside the loop**:
  ```python
  # WRONG:
  while count < 5:
      count = 0  # Resets to 0 every iteration! Infinite loop!
      print(count)
      count = count + 1

  # CORRECT:
  count = 0  # Initialize BEFORE loop
  while count < 5:
      print(count)
      count = count + 1
  ```
