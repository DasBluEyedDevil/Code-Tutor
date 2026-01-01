---
type: "THEORY"
title: "Understanding the Concept"
---

Imagine you're washing dishes. You follow this simple process:

<ol style='background-color: #f0f0f0; padding: 15px; margin: 10px 0;'>- **Check:** Are there still dirty dishes?
- **If YES:** Wash one dish, then go back to step 1
- **If NO:** You're done!

This is a **loop** - repeating an action while a condition remains true. In code:

```
while there_are_dirty_dishes:
    wash_one_dish()
    # Loop back and check again

```
The **while loop** is Python's way of saying: "Keep doing this action over and over, as long as this condition is True."

### Why Loops Matter:
Without loops, if you wanted to print "Hello" 100 times, you'd need to write:

```
print("Hello")
print("Hello")
print("Hello")
# ... 97 more times!

```
With a loop:

```
count = 0
while count < 100:
    print("Hello")
    count = count + 1  # This is critical!
# Done in 4 lines instead of 100!

```
### Real-World Examples:

- **ATM machine**:
WHILE user hasn't entered correct PIN:
&nbsp;&nbsp;&nbsp;&nbsp;Ask for PIN again
- **Game loop**:
WHILE player is alive:
&nbsp;&nbsp;&nbsp;&nbsp;Accept player input
&nbsp;&nbsp;&nbsp;&nbsp;Update game state
&nbsp;&nbsp;&nbsp;&nbsp;Draw screen
- **Downloading files**:
WHILE download not complete:
&nbsp;&nbsp;&nbsp;&nbsp;Download next chunk
&nbsp;&nbsp;&nbsp;&nbsp;Update progress bar
- **Menu system**:
WHILE user hasn't chosen 'quit':
&nbsp;&nbsp;&nbsp;&nbsp;Show menu
&nbsp;&nbsp;&nbsp;&nbsp;Get user choice
&nbsp;&nbsp;&nbsp;&nbsp;Execute action

Loops are the foundation of automation - they let computers do what they do best: repeat tasks tirelessly!