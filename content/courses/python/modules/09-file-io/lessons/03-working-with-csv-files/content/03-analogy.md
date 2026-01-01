---
type: "ANALOGY"
title: "Syntax Breakdown: CSV Operations"
---

**Importing csv module:**
```python
import csv
```

**Writing CSV - Basic (lists):**
```python
with open('data.csv', 'w', newline='') as file:
    writer = csv.writer(file)
    
    # Write one row
    writer.writerow(['Name', 'Age', 'City'])
    
    # Write multiple rows
    rows = [
        ['Alice', 25, 'NYC'],
        ['Bob', 30, 'LA']
    ]
    writer.writerows(rows)
```

**Reading CSV - Basic (lists):**
```python
with open('data.csv', 'r') as file:
    reader = csv.reader(file)
    
    for row in reader:
        # row is a list: ['Alice', '25', 'NYC']
        print(row[0], row[1], row[2])
```

**Writing CSV - Dictionaries (recommended):**
```python
with open('data.csv', 'w', newline='') as file:
    fieldnames = ['Name', 'Age', 'City']
    writer = csv.DictWriter(file, fieldnames=fieldnames)
    
    writer.writeheader()  # Write column names
    
    writer.writerow({'Name': 'Alice', 'Age': 25, 'City': 'NYC'})
    
    # Or write multiple
    data = [
        {'Name': 'Bob', 'Age': 30, 'City': 'LA'},
        {'Name': 'Carol', 'Age': 28, 'City': 'Chicago'}
    ]
    writer.writerows(data)
```

**Reading CSV - Dictionaries (recommended):**
```python
with open('data.csv', 'r') as file:
    reader = csv.DictReader(file)
    
    for row in reader:
        # row is a dict: {'Name': 'Alice', 'Age': '25', 'City': 'NYC'}
        print(row['Name'], row['Age'], row['City'])
```

**Important: newline='' parameter**

Always use `newline=''` when opening CSV files in write mode:
```python
with open('data.csv', 'w', newline='') as file:  # newline='' required!
```

Without it, you get extra blank lines on Windows.

**Skipping header row:**
```python
with open('data.csv', 'r') as file:
    reader = csv.reader(file)
    next(reader)  # Skip first row (header)
    
    for row in reader:
        # Process data rows only
        pass
```

**Common patterns:**

**1. Read CSV into list of dictionaries:**
```python
with open('data.csv', 'r') as file:
    reader = csv.DictReader(file)
    data = list(reader)  # Convert to list

# Now you have: [{'Name': 'Alice', ...}, {...}, ...]
```

**2. Filter CSV data:**
```python
with open('data.csv', 'r') as file:
    reader = csv.DictReader(file)
    
    for row in reader:
        if int(row['Age']) >= 25:  # Filter condition
            print(row['Name'])
```

**3. Transform CSV (read → modify → write):**
```python
# Read
with open('input.csv', 'r') as infile:
    reader = csv.DictReader(infile)
    data = list(reader)

# Modify
for row in data:
    row['Age'] = int(row['Age']) + 1  # Add 1 to age

# Write
with open('output.csv', 'w', newline='') as outfile:
    writer = csv.DictWriter(outfile, fieldnames=data[0].keys())
    writer.writeheader()
    writer.writerows(data)
```

**CSV vs regular text files:**

**Regular text file:**
```python
with open('file.txt', 'w') as f:
    f.write('Hello\n')
```

**CSV file:**
```python
with open('file.csv', 'w', newline='') as f:
    writer = csv.writer(f)
    writer.writerow(['Hello', 'World'])
```

**Key differences:**
- CSV needs `import csv`
- CSV needs `newline=''` when writing
- CSV handles commas/quotes in data automatically
- DictReader/DictWriter make column access easier