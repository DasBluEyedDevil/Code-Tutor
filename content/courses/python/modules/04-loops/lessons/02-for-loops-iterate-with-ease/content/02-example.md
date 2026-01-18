---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
=== Counting Range ===
Enter start: 1
Enter stop: 6
1
2
3
4
5

=== Multiplication Table ===
Enter number: 5
5 x 1 = 5
5 x 2 = 10
...
5 x 10 = 50

=== String Iteration ===
Enter a word: Python
P
y
t
h
o
n
```

```python
# for Loops: Iterating Over Sequences

# Example 1: Basic Range (Interactive)
print("=== Counting Range ===")
start = int(input("Enter start: "))
stop = int(input("Enter stop: "))

# Remember: range stops BEFORE the stop value!
for number in range(start, stop):
    print(number)

print()

# Example 2: Multiplication Table
print("=== Multiplication Table ===")
num = int(input("Enter number: "))

for i in range(1, 11):
    result = num * i
    print(f"{num} x {i} = {result}")

print()

# Example 3: Iterating Over a String
print("=== String Iteration ===")
word = input("Enter a word: ")

for letter in word:
    print(letter)

print()

# Example 4: Nested Loops (Pattern)
print("=== Pattern Builder ===")
rows = int(input("How many rows of stars? "))

for i in range(1, rows + 1):
    # Print i stars
    for j in range(i):
        print("*", end="")
    print()  # Newline after each row
```
