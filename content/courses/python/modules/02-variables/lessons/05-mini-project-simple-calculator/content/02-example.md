---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
Welcome to Simple Calculator!

Choose an operation:
1. Addition (+)
2. Subtraction (-)
3. Multiplication (*)
4. Division (/)
5. Floor Division (//)
6. Modulo (%)
7. Exponentiation (**)

Enter your choice (1-7): 1
Enter first number: 15.5
Enter second number: 4.5

Result: 15.5 + 4.5 = 20.0

Thank you for using Simple Calculator!
```

```python
# Simple Calculator - Complete Example
# Demonstrates all Module 2 concepts in one program

print("Welcome to Simple Calculator!")
print()

# Display menu (using print statements)
print("Choose an operation:")
print("1. Addition (+)")
print("2. Subtraction (-)")
print("3. Multiplication (*)")
print("4. Division (/)")
print("5. Floor Division (//)")
print("6. Modulo (%)")
print("7. Exponentiation (**)")
print()

# Get user's choice (string input → int conversion)
choice = int(input("Enter your choice (1-7): "))

# Get numbers from user (string input → float conversion)
num1 = float(input("Enter first number: "))
num2 = float(input("Enter second number: "))

print()  # Blank line for readability

# Perform calculation based on choice
if choice == 1:
    result = num1 + num2
    print(f"Result: {num1} + {num2} = {result}")
elif choice == 2:
    result = num1 - num2
    print(f"Result: {num1} - {num2} = {result}")
elif choice == 3:
    result = num1 * num2
    print(f"Result: {num1} * {num2} = {result}")
elif choice == 4:
    result = num1 / num2
    print(f"Result: {num1} / {num2} = {result}")
elif choice == 5:
    result = num1 // num2
    print(f"Result: {num1} // {num2} = {result}")
elif choice == 6:
    result = num1 % num2
    print(f"Result: {num1} % {num2} = {result}")
elif choice == 7:
    result = num1 ** num2
    print(f"Result: {num1} ** {num2} = {result}")
else:
    print("Invalid choice! Please choose 1-7.")

print("\nThank you for using Simple Calculator!")
```
