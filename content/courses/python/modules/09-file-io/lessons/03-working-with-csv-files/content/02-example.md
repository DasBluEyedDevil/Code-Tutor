---
type: "EXAMPLE"
title: "Code Example: Reading and Writing CSV Files"
---

Key concepts:
1. **csv.writer()** - Write lists to CSV (row by row)
2. **csv.reader()** - Read CSV into lists
3. **csv.DictWriter()** - Write dictionaries to CSV (keys = column names)
4. **csv.DictReader()** - Read CSV into dictionaries (column names = keys)
5. **newline=''** - Required parameter when opening CSV files in write mode
6. **writeheader()** - DictWriter method to write column headers
7. **next(reader)** - Skip header row when processing data

DictReader/DictWriter are usually easier because you can access columns by name!

```python
import csv

# Example 1: Writing a CSV file
print("=== Writing CSV File ===")

# Data to write (list of lists)
students = [
    ["Name", "Age", "Grade", "City"],  # Header row
    ["Alice", 20, "A", "NYC"],
    ["Bob", 22, "B", "LA"],
    ["Carol", 21, "A", "Chicago"],
    ["David", 23, "C", "Boston"]
]

with open("students.csv", "w", newline='') as file:
    writer = csv.writer(file)
    
    # Write all rows
    writer.writerows(students)
    # Or write one at a time: writer.writerow(row)

print("✓ Created students.csv\n")

# Example 2: Reading a CSV file
print("=== Reading CSV File ===")

with open("students.csv", "r") as file:
    reader = csv.reader(file)
    
    print("File contents:")
    for row in reader:
        print(row)  # Each row is a list

print("")

# Example 3: Reading with headers
print("=== Reading with Header Processing ===")

with open("students.csv", "r") as file:
    reader = csv.reader(file)
    
    # Get header row first
    headers = next(reader)
    print(f"Headers: {headers}\n")
    
    # Process data rows
    print("Students:")
    for row in reader:
        name, age, grade, city = row
        print(f"  {name} (age {age}): Grade {grade} from {city}")

print("")

# Example 4: Using DictReader (rows as dictionaries)
print("=== DictReader - Rows as Dictionaries ===")

with open("students.csv", "r") as file:
    reader = csv.DictReader(file)
    
    for row in reader:
        # Each row is a dictionary!
        print(f"{row['Name']}: {row['Grade']} grade, {row['Age']} years old")

print("")

# Example 5: Writing with DictWriter
print("=== DictWriter - Writing Dictionaries ===")

products = [
    {"Product": "Laptop", "Price": 999.99, "Stock": 15},
    {"Product": "Mouse", "Price": 29.99, "Stock": 50},
    {"Product": "Keyboard", "Price": 79.99, "Stock": 30}
]

with open("products.csv", "w", newline='') as file:
    fieldnames = ["Product", "Price", "Stock"]
    writer = csv.DictWriter(file, fieldnames=fieldnames)
    
    # Write header
    writer.writeheader()
    
    # Write data rows
    writer.writerows(products)

print("✓ Created products.csv\n")

# Read it back
print("Product catalog:")
with open("products.csv", "r") as file:
    reader = csv.DictReader(file)
    for row in reader:
        print(f"  {row['Product']}: ${row['Price']} ({row['Stock']} in stock)")

print("")

# Example 6: Filtering and processing CSV data
print("=== Filtering CSV Data ===")

print("Students with grade 'A':")
with open("students.csv", "r") as file:
    reader = csv.DictReader(file)
    
    for row in reader:
        if row['Grade'] == 'A':
            print(f"  - {row['Name']} from {row['City']}")

print("")

# Example 7: Handling special characters
print("=== Handling Special Characters ===")

special_data = [
    ["Name", "Company", "Salary"],
    ["Smith, John", "Tech Corp", "75,000"],  # Commas in data!
    ['Jane "JJ" Doe', "StartUp Inc", "80,000"],  # Quotes in data!
]

with open("special.csv", "w", newline='') as file:
    writer = csv.writer(file)
    writer.writerows(special_data)

print("✓ Written data with commas and quotes")

# Read it back - csv module handles it correctly
with open("special.csv", "r") as file:
    reader = csv.reader(file)
    next(reader)  # Skip header
    
    print("\nData read correctly:")
    for row in reader:
        print(f"  Name: {row[0]}")
        print(f"  Company: {row[1]}")
        print(f"  Salary: {row[2]}\n")

print("✓ CSV module handled special characters correctly!")
```
