# Enhanced Calculator - SOLUTION
# A complete, functional calculator program

# Title banner
print("===================================")
print("   SUPER CALCULATOR PRO 3000")
print("===================================")
print()

# Get user's name for personalization
name = input("What's your name? ")

print(f"\nHi {name}! Let's do some math!")
print()

# Display menu
print("Choose an operation:")
print("1. Addition (+)")
print("2. Subtraction (-)")
print("3. Multiplication (*)")
print("4. Division (/)")
print("5. Floor Division (//)")
print("6. Modulo (%)")
print("7. Exponentiation (**)")
print()

# Get user's choice
choice = int(input("Enter your choice (1-7): "))

# Get two numbers
num1 = float(input("Enter first number: "))
num2 = float(input("Enter second number: "))

print()

# Perform calculation
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

# Farewell message
print(f"\nThanks for calculating, {name}! See you next time!")