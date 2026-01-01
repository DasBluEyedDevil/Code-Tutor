# Student Grade Manager - Starter Code
# Complete the missing parts

print("=== Student Grade Manager ===")
print()

# Storage for grades
grades = []

# Main program loop
while True:
    # Display menu
    print("\nMain Menu:")
    print("1. Enter grades")
    print("2. View statistics")
    print("3. View distribution")
    print("4. Find high/low")
    print("5. Exit")
    print()
    
    choice = input("Choice: ")
    
    # Option 1: Enter grades
    if choice == "1":
        # Get number of students
        while True:
            try:
                num_students = int(input("\nHow many students? "))
                if num_students > 0:
                    break
                else:
                    print("Please enter a positive number!")
            except ValueError:
                print("Please enter a valid number!")
        
        print()
        
        # Clear previous grades
        grades = []
        
        # Collect grades
        for student_num in range(1, num_students + 1):
            while True:
                try:
                    grade = float(input(f"Grade for student {student_num} (0-100): "))
                    
                    # YOUR CODE: Validate grade is 0-100
                    if :  # Check range
                        break
                    else:
                        print("Grade must be 0-100!")
                except ValueError:
                    print("Please enter a valid number!")
            
            # YOUR CODE: Add grade to list
            grades.  # Append to list
        
        print(f"\n✅ {len(grades)} grades entered!")
    
    # Option 2: Statistics
    elif choice == "2":
        if len(grades) == 0:
            print("\n❌ No grades entered yet!")
            continue
        
        print("\n=== Statistics ===")
        
        # YOUR CODE: Calculate statistics
        total = 0
        passing_count = 0
        failing_count = 0
        
        for grade in grades:
            total = total +   # Add to total
            
            if :  # Passing threshold
                passing_count = passing_count + 1
            else:
                failing_count = failing_count + 1
        
        average =   # Calculate average
        
        # Display results
        print(f"Total students: {len(grades)}")
        print(f"Average: {average:.1f}")
        print(f"Passing (>=60): {passing_count} ({passing_count/len(grades)*100:.1f}%)")
        print(f"Failing (<60): {failing_count} ({failing_count/len(grades)*100:.1f}%)")
    
    # Option 3: Distribution
    elif choice == "3":
        if len(grades) == 0:
            print("\n❌ No grades entered yet!")
            continue
        
        print("\n=== Grade Distribution ===")
        
        # YOUR CODE: Count each letter grade
        a_count = 0
        b_count = 0
        c_count = 0
        d_count = 0
        f_count = 0
        
        for grade in grades:
            if grade >= 90:
                a_count = a_count + 1
            elif :  # B range
                b_count = b_count + 1
            elif grade >= 70:
                c_count = c_count + 1
            elif grade >= 60:
                d_count = d_count + 1
            else:
                f_count = f_count + 1
        
        # Display with bars
        print(f"A (90+):  {a_count:2} {'*' * a_count}")
        print(f"B (80+):  {b_count:2} {'*' * b_count}")
        print(f"C (70+):  {c_count:2} {'*' * c_count}")
        print(f"D (60+):  {d_count:2} {'*' * d_count}")
        print(f"F (<60):  {f_count:2} {'*' * f_count}")
    
    # Option 4: High/Low
    elif choice == "4":
        if len(grades) == 0:
            print("\n❌ No grades entered yet!")
            continue
        
        print("\n=== High/Low ===")
        
        # YOUR CODE: Find highest and lowest
        highest = grades[0]
        lowest = grades[0]
        highest_student = 1
        lowest_student = 1
        
        for i, grade in enumerate(grades, start=1):
            if grade > highest:
                highest = 
                highest_student = 
            if :  # Check if lower
                lowest = grade
                lowest_student = i
        
        print(f"Highest: {highest} (Student {highest_student})")
        print(f"Lowest: {lowest} (Student {lowest_student})")
    
    # Option 5: Exit
    elif choice == "5":
        print("\nGoodbye!")
        break
    
    # Invalid choice
    else:
        print("\n❌ Invalid choice! Please enter 1-5.")