import csv

# Student Grade Manager
# This solution demonstrates CSV reading and writing with DictReader/DictWriter

def create_grades_file(records):
    """Write grade records to CSV."""
    # Step 1: Open file with newline='' for proper CSV handling
    with open('grades.csv', 'w', newline='') as file:
        # Step 2: Create DictWriter with column names
        fieldnames = ['Name', 'Subject', 'Grade', 'Score']
        writer = csv.DictWriter(file, fieldnames=fieldnames)
        
        # Step 3: Write header row
        writer.writeheader()
        
        # Step 4: Write all records
        writer.writerows(records)
    
    print(f"Created grades.csv with {len(records)} records")

def read_all_grades():
    """Read and display all grade records."""
    with open('grades.csv', 'r') as file:
        reader = csv.DictReader(file)
        
        # Print header
        print(f"{'Name':<10} {'Subject':<10} {'Grade':<6} {'Score':<6}")
        print("-" * 32)
        
        # Print each row
        for row in reader:
            print(f"{row['Name']:<10} {row['Subject']:<10} {row['Grade']:<6} {row['Score']:<6}")

def calculate_averages():
    """Calculate average score per student."""
    # Step 1: Collect all scores per student
    student_scores = {}
    
    with open('grades.csv', 'r') as file:
        reader = csv.DictReader(file)
        
        for row in reader:
            name = row['Name']
            score = int(row['Score'])
            
            if name not in student_scores:
                student_scores[name] = []
            student_scores[name].append(score)
    
    # Step 2: Calculate average for each student
    averages = {}
    for name, scores in student_scores.items():
        averages[name] = sum(scores) / len(scores)
    
    return averages

def find_a_students():
    """Find all students with grade 'A'."""
    a_students = set()  # Use set to avoid duplicates
    
    with open('grades.csv', 'r') as file:
        reader = csv.DictReader(file)
        
        for row in reader:
            if row['Grade'] == 'A':
                a_students.add(row['Name'])
    
    return list(a_students)

# Test data
grade_records = [
    {'Name': 'Alice', 'Subject': 'Math', 'Grade': 'A', 'Score': '95'},
    {'Name': 'Alice', 'Subject': 'English', 'Grade': 'B', 'Score': '85'},
    {'Name': 'Bob', 'Subject': 'Math', 'Grade': 'B', 'Score': '88'},
    {'Name': 'Bob', 'Subject': 'English', 'Grade': 'A', 'Score': '92'},
    {'Name': 'Carol', 'Subject': 'Math', 'Grade': 'A', 'Score': '97'},
    {'Name': 'Carol', 'Subject': 'English', 'Grade': 'A', 'Score': '94'},
]

# Test the functions
print("Creating grades file...")
create_grades_file(grade_records)

print("\nAll grades:")
read_all_grades()

print("\nAverage scores:")
averages = calculate_averages()
for name, avg in averages.items():
    print(f"  {name}: {avg:.1f}")

print("\nStudents with A grades:")
a_students = find_a_students()
for name in a_students:
    print(f"  - {name}")