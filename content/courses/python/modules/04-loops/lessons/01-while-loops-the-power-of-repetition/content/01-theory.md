---
type: "THEORY"
title: "Understanding the Concept"
---

Imagine you're washing dishes. You follow this simple process:

- **Check:** Are there still dirty dishes?
- **If YES:** Wash one dish, then go back to step 1
- **If NO:** You're done!

This is a **loop** - repeating an action while a condition remains true. In code:

```python
while there_are_dirty_dishes:
    wash_one_dish()
    # Loop back and check again
```

The **while loop** is Python's way of saying: "Keep doing this action over and over, as long as this condition is True."

### Why Loops Matter

Without loops, if you wanted to print "Hello" 100 times, you'd need to write:

```python
print("Hello")
print("Hello")
print("Hello")
# ... 97 more times!
```

With a loop:

```python
count = 0
while count < 100:
    print("Hello")
    count = count + 1  # This is critical!
# Done in 4 lines instead of 100!
```

### Real-World Examples

- **ATM machine**:
  - WHILE user hasn't entered correct PIN:
    - Ask for PIN again

- **Game loop**:
  - WHILE player is alive:
    - Accept player input
    - Update game state
    - Draw screen

- **Downloading files**:
  - WHILE download not complete:
    - Download next chunk
    - Update progress bar

- **Menu system**:
  - WHILE user hasn't chosen 'quit':
    - Show menu
    - Get user choice
    - Execute action

Loops are the foundation of automation - they let computers do what they do best: repeat tasks tirelessly!
