---
type: "THEORY"
title: "Syntax Breakdown"
---

### for Loop Anatomy

```python
for variable in sequence:
    # Code to repeat (loop body)
    # Indented (4 spaces)
    statement1
    statement2
```

#### Breaking It Down:

- **The keyword:** `for` (lowercase)
- **The loop variable:** Name you choose (e.g., `number`, `letter`, `item`)
  ```python
  for number in range(5):  # 'number' will hold each value
  for letter in "Hello":   # 'letter' will hold each character
  ```
- **The keyword `in`:** Required!
- **The sequence:** What to iterate over
  - `range()` for numbers
  - String for characters
  - List for items (Module 5!)
- **The colon (:):** Required!
- **Indented body:** Code that runs for each item

### Understanding range()

The `range()` function generates a sequence of numbers. It has three forms:

#### 1. range(stop) - One Argument

```python
range(5)  # Generates: 0, 1, 2, 3, 4
# Starts at 0, stops BEFORE 5
```

| Code | Generates |
| :--- | :--- |
| `range(3)` | 0, 1, 2 |
| `range(10)` | 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 |
| `range(1)` | 0 |

#### 2. range(start, stop) - Two Arguments

```python
range(1, 6)  # Generates: 1, 2, 3, 4, 5
# Starts at 1, stops BEFORE 6
```

| Code | Generates |
| :--- | :--- |
| `range(1, 5)` | 1, 2, 3, 4 |
| `range(5, 10)` | 5, 6, 7, 8, 9 |
| `range(0, 3)` | 0, 1, 2 |

**Critical:** The stop value is EXCLUDED! `range(1, 6)` stops *before* 6.

#### 3. range(start, stop, step) - Three Arguments

```python
range(0, 10, 2)  # Generates: 0, 2, 4, 6, 8
# Starts at 0, stops before 10, increment by 2
```

| Code | Generates | Description |
| :--- | :--- | :--- |
| `range(0, 10, 2)` | 0, 2, 4, 6, 8 | Even numbers |
| `range(1, 10, 2)` | 1, 3, 5, 7, 9 | Odd numbers |
| `range(0, 20, 5)` | 0, 5, 10, 15 | Count by 5s |
| `range(10, 0, -1)` | 10, 9, 8, ..., 1 | Countdown |
| `range(5, 0, -1)` | 5, 4, 3, 2, 1 | Countdown from 5 |

### Iterating Over Strings

```python
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

| Use `for` when... | Use `while` when... |
| :--- | :--- |
| You know the sequence to iterate | You don't know how many iterations |
| Counting a specific range | Looping until a condition changes |
| Processing each item in a collection | Input validation (retry until valid) |
| Simpler, less error-prone | Condition-based repetition |

#### Examples:

```python
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

### Common Patterns

#### 1. Counting Loop

```python
for i in range(1, 11):  # 1 through 10
    print(f"Iteration {i}")
```

#### 2. Accumulator (Sum/Product)

```python
total = 0
for num in range(1, 6):
    total = total + num  # Sum: 1+2+3+4+5
print(total)  # 15
```

#### 3. Building Strings

```python
result = ""
for i in range(5):
    result = result + "*"
print(result)  # *****
```

#### 4. Character Counter

```python
vowels = 0
for letter in "Hello World":
    if letter.lower() in "aeiou":
        vowels = vowels + 1
print(vowels)  # 3
```

### Common Mistakes

- **Off-by-one with range()**:
  ```python
  # WRONG: Trying to count 1 to 5
  for i in range(1, 5):  # Only goes to 4!
      print(i)  # 1, 2, 3, 4 (missing 5)

  # CORRECT:
  for i in range(1, 6):  # Stop BEFORE 6
      print(i)  # 1, 2, 3, 4, 5
  ```

- **Modifying loop variable (doesn't work!)**:
  ```python
  # DOESN'T DO WHAT YOU THINK:
  for i in range(5):
      i = i + 10  # This doesn't affect the loop!
      print(i)  # Prints: 10, 11, 12, 13, 14
  # Next iteration, i gets reset to next value from range
  ```

- **Forgetting colon**:
  ```python
  # WRONG:
  for i in range(5)  # Missing colon!
      print(i)

  # CORRECT:
  for i in range(5):  # Has colon
      print(i)
  ```

- **Using wrong indentation**:
  ```python
  # WRONG:
  for i in range(3):
  print(i)  # Not indented! Won't loop

  # CORRECT:
  for i in range(3):
      print(i)  # Indented, part of loop
  ```
