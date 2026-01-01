---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
==================================================
PRACTICAL LOOP PROGRAMS - DEMONSTRATION
==================================================


=== PROGRAM 1: NUMBER GUESSING GAME ===

I'm thinking of a number between 1 and 100.
You have 7 attempts. Good luck!

Attempt 1/7 - Your guess: 50
📉 Too high! Try a lower number.

Attempt 2/7 - Your guess: 25
📈 Too low! Try a higher number.

Attempt 3/7 - Your guess: thirty-seven
❌ Please enter a valid number!

Attempt 3/7 - Your guess: 37
📈 Too low! Try a higher number.

Attempt 4/7 - Your guess: 44
📉 Too high! Try a lower number.

Attempt 5/7 - Your guess: 40
📈 Too low! Try a higher number.

Attempt 6/7 - Your guess: 42

🎉 Correct! You won in 6 attempts!

Press Enter to continue...

==================================================
=== PROGRAM 2: CALCULATOR MENU ===
==================================================


Calculator Menu:
1. Add
2. Subtract
3. Multiply
4. Divide
5. Power
6. Exit

Enter choice (1-6): 3
Enter first number: 7
Enter second number: 8

✅ 7.0 × 8.0 = 56.0

Calculator Menu:
1. Add
2. Subtract
3. Multiply
4. Divide
5. Power
6. Exit

Enter choice (1-6): 5
Enter first number: 2
Enter second number: 10

✅ 2.0 ^ 10.0 = 1024.0

Calculator Menu:
1. Add
2. Subtract
3. Multiply
4. Divide
5. Power
6. Exit

Enter choice (1-6): 6

👋 Goodbye!

Press Enter to continue...

==================================================
=== PROGRAM 3: GRADE ANALYZER ===
==================================================

Enter number of students: 5

Enter grade for student 1 (0-100): 85
Enter grade for student 2 (0-100): 92
Enter grade for student 3 (0-100): 78
Enter grade for student 4 (0-100): 95
Enter grade for student 5 (0-100): 88

========================================
GRADE REPORT
========================================
Total students:    5
Average score:     87.6
Highest score:     95.0
Lowest score:      78.0
Passing (>=60):    5 (100.0%)
Failing (<60):     0 (0.0%)
========================================

Grade Distribution:
Student  1:  85.0 (B) *****************
Student  2:  92.0 (A) ******************
Student  3:  78.0 (C) ***************
Student  4:  95.0 (A) *******************
Student  5:  88.0 (B) *****************

Press Enter to continue...

==================================================
=== PROGRAM 4: PATTERN STUDIO ===
==================================================


Available Patterns:
1. Rectangle
2. Right Triangle
3. Pyramid
4. Diamond
5. Multiplication Table
6. Exit

Choose pattern (1-6): 3
Enter size (1-20): 5

    *
   ***
  *****
 *******
*********


Available Patterns:
1. Rectangle
2. Right Triangle
3. Pyramid
4. Diamond
5. Multiplication Table
6. Exit

Choose pattern (1-6): 5
Enter size (1-20): 5

Multiplication Table (5x5):

       1   2   3   4   5
    --------------------
 1 |   1   2   3   4   5
 2 |   2   4   6   8  10
 3 |   3   6   9  12  15
 4 |   4   8  12  16  20
 5 |   5  10  15  20  25


Available Patterns:
1. Rectangle
2. Right Triangle
3. Pyramid
4. Diamond
5. Multiplication Table
6. Exit

Choose pattern (1-6): 6

Exiting Pattern Studio. Goodbye!

==================================================
All programs complete! Great work!
==================================================
```

```python
# Mini-Project: Practical Loop Programs
# Four complete programs demonstrating loop mastery

import random  # For number guessing game

print("=" * 50)
print("PRACTICAL LOOP PROGRAMS - DEMONSTRATION")
print("=" * 50)
print()

# ========================================
# PROGRAM 1: NUMBER GUESSING GAME
# ========================================

print("\n=== PROGRAM 1: NUMBER GUESSING GAME ===")
print()

# Generate random number between 1-100
secret_number = random.randint(1, 100)
attempts = 0
max_attempts = 7

print(f"I'm thinking of a number between 1 and 100.")
print(f"You have {max_attempts} attempts. Good luck!\n")

while attempts < max_attempts:
    # Get user input
    guess_input = input(f"Attempt {attempts + 1}/{max_attempts} - Your guess: ")
    
    # Validate input (is it a number?)
    try:
        guess = int(guess_input)
    except ValueError:
        print("❌ Please enter a valid number!\n")
        continue  # Skip to next iteration, don't count as attempt
    
    # Validate range
    if guess < 1 or guess > 100:
        print("❌ Number must be between 1 and 100!\n")
        continue
    
    # Count this as a valid attempt
    attempts = attempts + 1
    
    # Check guess
    if guess == secret_number:
        print(f"\n🎉 Correct! You won in {attempts} attempts!")
        break  # Exit loop - game won
    elif guess < secret_number:
        print("📈 Too low! Try a higher number.\n")
    else:
        print("📉 Too high! Try a lower number.\n")
else:
    # Loop finished without break - player lost
    print(f"\n😞 Game over! The number was {secret_number}.")

print()
input("Press Enter to continue...")

# ========================================
# PROGRAM 2: CALCULATOR MENU
# ========================================

print("\n" + "=" * 50)
print("=== PROGRAM 2: CALCULATOR MENU ===")
print("=" * 50)
print()

while True:  # Infinite loop - runs until break
    # Display menu
    print("\nCalculator Menu:")
    print("1. Add")
    print("2. Subtract")
    print("3. Multiply")
    print("4. Divide")
    print("5. Power")
    print("6. Exit")
    print()
    
    choice = input("Enter choice (1-6): ")
    
    # Exit condition
    if choice == "6":
        print("\n👋 Goodbye!")
        break  # Exit the while True loop
    
    # Validate choice
    if choice not in ["1", "2", "3", "4", "5"]:
        print("❌ Invalid choice! Please enter 1-6.")
        continue  # Skip rest, show menu again
    
    # Get numbers
    try:
        num1 = float(input("Enter first number: "))
        num2 = float(input("Enter second number: "))
    except ValueError:
        print("❌ Invalid input! Please enter numbers.")
        continue
    
    # Perform calculation
    if choice == "1":
        result = num1 + num2
        operation = "+"
    elif choice == "2":
        result = num1 - num2
        operation = "-"
    elif choice == "3":
        result = num1 * num2
        operation = "×"
    elif choice == "4":
        if num2 == 0:
            print("❌ Cannot divide by zero!")
            continue
        result = num1 / num2
        operation = "÷"
    elif choice == "5":
        result = num1 ** num2
        operation = "^"
    
    # Display result
    print(f"\n✅ {num1} {operation} {num2} = {result}")

print()
input("Press Enter to continue...")

# ========================================
# PROGRAM 3: GRADE ANALYZER
# ========================================

print("\n" + "=" * 50)
print("=== PROGRAM 3: GRADE ANALYZER ===")
print("=" * 50)
print()

# Get number of students
while True:
    try:
        num_students = int(input("Enter number of students: "))
        if num_students > 0:
            break
        else:
            print("Please enter a positive number!")
    except ValueError:
        print("Please enter a valid number!")

print()

# Initialize accumulators
total_score = 0
highest_score = 0  # Will update on first grade
lowest_score = 100  # Will update on first grade
passing_count = 0
failing_count = 0
grades_list = []  # Store all grades for later

# Collect grades
for student_num in range(1, num_students + 1):
    while True:
        try:
            grade = float(input(f"Enter grade for student {student_num} (0-100): "))
            
            # Validate range
            if 0 <= grade <= 100:
                break
            else:
                print("Grade must be between 0 and 100!")
        except ValueError:
            print("Please enter a valid number!")
    
    # Process this grade
    grades_list.append(grade)  # Store it
    total_score = total_score + grade  # Add to sum
    
    # Update highest/lowest
    if grade > highest_score:
        highest_score = grade
    if grade < lowest_score:
        lowest_score = grade
    
    # Count passing/failing
    if grade >= 60:
        passing_count = passing_count + 1
    else:
        failing_count = failing_count + 1

# Calculate statistics
average_score = total_score / num_students

# Display report
print("\n" + "=" * 40)
print("GRADE REPORT")
print("=" * 40)
print(f"Total students:    {num_students}")
print(f"Average score:     {average_score:.1f}")
print(f"Highest score:     {highest_score}")
print(f"Lowest score:      {lowest_score}")
print(f"Passing (>=60):    {passing_count} ({passing_count/num_students*100:.1f}%)")
print(f"Failing (<60):     {failing_count} ({failing_count/num_students*100:.1f}%)")
print("=" * 40)

# Display grade distribution
print("\nGrade Distribution:")
for i, grade in enumerate(grades_list, start=1):
    # Determine letter grade
    if grade >= 90:
        letter = "A"
    elif grade >= 80:
        letter = "B"
    elif grade >= 70:
        letter = "C"
    elif grade >= 60:
        letter = "D"
    else:
        letter = "F"
    
    # Create bar chart
    bar_length = int(grade / 5)  # Each * = 5 points
    bar = "*" * bar_length
    
    print(f"Student {i:2}: {grade:5.1f} ({letter}) {bar}")

print()
input("Press Enter to continue...")

# ========================================
# PROGRAM 4: PATTERN STUDIO
# ========================================

print("\n" + "=" * 50)
print("=== PROGRAM 4: PATTERN STUDIO ===")
print("=" * 50)
print()

while True:
    # Display menu
    print("\nAvailable Patterns:")
    print("1. Rectangle")
    print("2. Right Triangle")
    print("3. Pyramid")
    print("4. Diamond")
    print("5. Multiplication Table")
    print("6. Exit")
    print()
    
    choice = input("Choose pattern (1-6): ")
    
    if choice == "6":
        print("\nExiting Pattern Studio. Goodbye!")
        break
    
    if choice not in ["1", "2", "3", "4", "5"]:
        print("❌ Invalid choice!")
        continue
    
    # Get size
    try:
        size = int(input("Enter size (1-20): "))
        if size < 1 or size > 20:
            print("Size must be between 1 and 20!")
            continue
    except ValueError:
        print("Please enter a valid number!")
        continue
    
    print()  # Blank line before pattern
    
    # Generate chosen pattern
    if choice == "1":
        # Rectangle
        width = size
        height = max(3, size // 2)  # Make it rectangular
        
        for row in range(height):
            for col in range(width):
                print("*", end=" ")
            print()
    
    elif choice == "2":
        # Right Triangle
        for row in range(1, size + 1):
            for col in range(row):
                print("*", end=" ")
            print()
    
    elif choice == "3":
        # Pyramid
        for row in range(1, size + 1):
            # Spaces
            for space in range(size - row):
                print(" ", end="")
            # Stars
            for star in range(2 * row - 1):
                print("*", end="")
            print()
    
    elif choice == "4":
        # Diamond (pyramid + inverted pyramid)
        # Top half
        for row in range(1, size + 1):
            for space in range(size - row):
                print(" ", end="")
            for star in range(2 * row - 1):
                print("*", end="")
            print()
        
        # Bottom half
        for row in range(size - 1, 0, -1):
            for space in range(size - row):
                print(" ", end="")
            for star in range(2 * row - 1):
                print("*", end="")
            print()
    
    elif choice == "5":
        # Multiplication Table
        print(f"Multiplication Table ({size}x{size}):\n")
        
        # Header row
        print("    ", end="")
        for i in range(1, size + 1):
            print(f"{i:4}", end="")
        print()
        print("    " + "-" * (4 * size))
        
        # Table rows
        for row in range(1, size + 1):
            print(f"{row:2} |", end="")
            for col in range(1, size + 1):
                product = row * col
                print(f"{product:4}", end="")
            print()
    
    print()  # Blank line after pattern

print("\n" + "=" * 50)
print("All programs complete! Great work!")
print("=" * 50)
```
