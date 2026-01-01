import csv

def create_grades_file(records):
    """Write grade records to CSV.
    
    Args:
        records: List of dictionaries with keys: Name, Subject, Grade, Score
    """
    # TODO: Open grades.csv in write mode with newline=''
    # TODO: Create DictWriter with fieldnames
    # TODO: Write header
    # TODO: Write all records
    pass

def read_all_grades():
    """Read and display all grade records."""
    # TODO: Open grades.csv in read mode
    # TODO: Create DictReader
    # TODO: Read and print each row
    pass

def calculate_averages():
    """Calculate average score per student.
    
    Returns:
        dict: {student_name: average_score}
    """
    # TODO: Read CSV with DictReader
    # TODO: Group scores by student name
    # TODO: Calculate average for each student
    # TODO: Return dictionary
    pass

def find_a_students():
    """Find all students with grade 'A'.
    
    Returns:
        list: List of student names with grade A
    """
    # TODO: Read CSV with DictReader
    # TODO: Filter rows where Grade == 'A'
    # TODO: Return list of names
    pass

# Test data
grade_records = [
    {'Name': 'Alice', 'Subject': 'Math', 'Grade': 'A', 'Score': '95'},
    {'Name': 'Alice', 'Subject': 'English', 'Grade': 'B', 'Score': '85'},
    {'Name': 'Bob', 'Subject': 'Math', 'Grade': 'B', 'Score': '88'},
    {'Name': 'Bob', 'Subject': 'English', 'Grade': 'A', 'Score': '92'},
    {'Name': 'Carol', 'Subject': 'Math', 'Grade': 'A', 'Score': '97'},
    {'Name': 'Carol', 'Subject': 'English', 'Grade': 'A', 'Score': '94'},
]

# Test your functions
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