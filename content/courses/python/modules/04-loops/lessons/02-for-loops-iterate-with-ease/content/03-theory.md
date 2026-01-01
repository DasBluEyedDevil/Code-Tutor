---
type: "THEORY"
title: "Syntax Breakdown"
---

### for Loop Anatomy:
```
for variable in sequence:
    # Code to repeat (loop body)
    # Indented (4 spaces)
    statement1
    statement2

```
#### Breaking It Down:

- **The keyword:** `for` (lowercase)
<li>**The loop variable:** Name you choose (e.g., `number`, `letter`, `item`)```
for number in range(5):  # 'number' will hold each value
for letter in "Hello":   # 'letter' will hold each character

```
</li>- **The keyword `in`:** Required!
- **The sequence:** What to iterate over
<li>`range()` for numbers
- String for characters
- List for items (Module 5!)

</li>- **The colon (:):** Required!
- **Indented body:** Code that runs for each item

### Understanding range():
The `range()` function generates a sequence of numbers. It has three forms:

#### 1. range(stop) - One Argument
```
range(5)  # Generates: 0, 1, 2, 3, 4
# Starts at 0, stops BEFORE 5

```
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Code</th><th>Generates</th></tr><tr><td>`range(3)`</td><td>0, 1, 2</td></tr><tr><td>`range(10)`</td><td>0, 1, 2, 3, 4, 5, 6, 7, 8, 9</td></tr><tr><td>`range(1)`</td><td>0</td></tr></table>#### 2. range(start, stop) - Two Arguments
```
range(1, 6)  # Generates: 1, 2, 3, 4, 5
# Starts at 1, stops BEFORE 6

```
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Code</th><th>Generates</th></tr><tr><td>`range(1, 5)`</td><td>1, 2, 3, 4</td></tr><tr><td>`range(5, 10)`</td><td>5, 6, 7, 8, 9</td></tr><tr><td>`range(0, 3)`</td><td>0, 1, 2</td></tr></table>**Critical:** The stop value is EXCLUDED! `range(1, 6)` stops <em>before</em> 6.

#### 3. range(start, stop, step) - Three Arguments
```
range(0, 10, 2)  # Generates: 0, 2, 4, 6, 8
# Starts at 0, stops before 10, increment by 2

```
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Code</th><th>Generates</th><th>Description</th></tr><tr><td>`range(0, 10, 2)`</td><td>0, 2, 4, 6, 8</td><td>Even numbers</td></tr><tr><td>`range(1, 10, 2)`</td><td>1, 3, 5, 7, 9</td><td>Odd numbers</td></tr><tr><td>`range(0, 20, 5)`</td><td>0, 5, 10, 15</td><td>Count by 5s</td></tr><tr><td>`range(10, 0, -1)`</td><td>10, 9, 8, ..., 1</td><td>Countdown</td></tr><tr><td>`range(5, 0, -1)`</td><td>5, 4, 3, 2, 1</td><td>Countdown from 5</td></tr></table>### Iterating Over Strings:
```
for character in "Hello":
    print(character)

# Outputs:
# H
# e
# l
# l
# o

```
Python treats strings as sequences of characters, so you can loop through each letter!

### for vs while: When to Use Each
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Use `for` when...</th><th>Use `while` when...</th></tr><tr><td>You know the sequence to iterate</td><td>You don't know how many iterations</td></tr><tr><td>Counting a specific range</td><td>Looping until a condition changes</td></tr><tr><td>Processing each item in a collection</td><td>Input validation (retry until valid)</td></tr><tr><td>Simpler, less error-prone</td><td>Condition-based repetition</td></tr></table>#### Examples:
```
# for is better here (known range):
for i in range(10):
    print(i)

# while is better here (unknown iterations):
password = ""
while password != "secret":
    password = input("Enter password: ")

# for is better here (iterate string):
for letter in "Python":
    print(letter)

# while is better here (game loop):
game_running = True
while game_running:
    # Game logic
    if player_quits:
        game_running = False

```
### Common Patterns:
#### 1. Counting Loop
```
for i in range(1, 11):  # 1 through 10
    print(f"Iteration {i}")

```
#### 2. Accumulator (Sum/Product)
```
total = 0
for num in range(1, 6):
    total = total + num  # Sum: 1+2+3+4+5
print(total)  # 15

```
#### 3. Building Strings
```
result = ""
for i in range(5):
    result = result + "*"
print(result)  # *****

```
#### 4. Character Counter
```
vowels = 0
for letter in "Hello World":
    if letter.lower() in "aeiou":
        vowels = vowels + 1
print(vowels)  # 3

```
### Common Mistakes:

<li>**Off-by-one with range()**:```
# WRONG: Trying to count 1 to 5
for i in range(1, 5):  # Only goes to 4!
    print(i)  # 1, 2, 3, 4 (missing 5)

# CORRECT:
for i in range(1, 6):  # Stop BEFORE 6
    print(i)  # 1, 2, 3, 4, 5

```
</li><li>**Modifying loop variable (doesn't work!)**:```
# DOESN'T DO WHAT YOU THINK:
for i in range(5):
    i = i + 10  # This doesn't affect the loop!
    print(i)  # Prints: 10, 11, 12, 13, 14
# Next iteration, i gets reset to next value from range

```
</li><li>**Forgetting colon**:```
# WRONG:
for i in range(5)  # Missing colon!
    print(i)

# CORRECT:
for i in range(5):  # Has colon
    print(i)

```
</li><li>**Using wrong indentation**:```
# WRONG:
for i in range(3):
print(i)  # Not indented! Won't loop

# CORRECT:
for i in range(3):
    print(i)  # Indented, part of loop

```
</li>