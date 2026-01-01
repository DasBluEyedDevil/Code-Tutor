---
type: "THEORY"
title: "Syntax Breakdown"
---

### Breaking Down the Calculator:
#### 1. The Menu Display
```
print("Choose an operation:")
print("1. Addition (+)")
print("2. Subtraction (-)")
# ... etc

```
Simple print statements to show options. Nothing fancy - just clear communication with the user.

#### 2. Getting User Input
```
choice = int(input("Enter your choice (1-7): "))
num1 = float(input("Enter first number: "))
num2 = float(input("Enter second number: "))

```
**Key decisions here:**

- `choice` is converted to `int` because menu options are whole numbers (1, 2, 3...)
- `num1` and `num2` are converted to `float` to handle both whole numbers and decimals (15, 15.5, etc.)

#### 3. The if-elif-else Chain
```
if choice == 1:
    result = num1 + num2
    print(f"Result: {num1} + {num2} = {result}")
elif choice == 2:
    result = num1 - num2
    print(f"Result: {num1} - {num2} = {result}")
# ... more elif statements
else:
    print("Invalid choice! Please choose 1-7.")

```
**What's happening:**

- `if choice == 1:` checks if the user entered 1
- `elif` means "else if" - checks the next condition only if previous ones were false
- `else:` catches everything that doesn't match (like entering 10 or 99)

**Note:** We're introducing `if-elif-else` here as a preview! You'll learn these "conditional statements" in detail in Module 3. For now, just understand that they let us choose different actions based on the user's choice.

#### 4. F-Strings for Output
```
print(f"Result: {num1} + {num2} = {result}")

```
Using f-strings to create a nicely formatted output showing the calculation and result.

#### 5. Blank Lines for Readability
```
print()  # Blank line
print("\n...")  # \n creates a new line

```
Small touches like this make your program easier to read. Professional programmers care about user experience!

### Module 2 Concepts in Action:
<table border="1" cellpadding="5"><tr><th>Concept</th><th>How We Used It</th></tr><tr><td>**Variables**</td><td>Stored choice, num1, num2, result</td></tr><tr><td>**Data Types**</td><td>Used int for choice, float for numbers, str in print()</td></tr><tr><td>**Type Conversion**</td><td>Converted input() strings to int and float</td></tr><tr><td>**Operators**</td><td>All 7 arithmetic operators (+, -, *, /, //, %, **)</td></tr><tr><td>**F-Strings**</td><td>Formatted output to show calculations clearly</td></tr></table>