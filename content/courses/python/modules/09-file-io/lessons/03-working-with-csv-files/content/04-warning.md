---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Not Using newline='' When Opening CSV**
```python
# WRONG - Can cause blank rows on Windows
with open("data.csv", "w") as f:
    writer = csv.writer(f)
    writer.writerow(["a", "b"])  # Extra blank lines!

# CORRECT - Use newline='' for CSV files
with open("data.csv", "w", newline='') as f:
    writer = csv.writer(f)
    writer.writerow(["a", "b"])
```

**2. Forgetting to Skip Header Row When Reading**
```python
# WRONG - Header treated as data
with open("data.csv", newline='') as f:
    reader = csv.reader(f)
    for row in reader:
        process(row)  # First row is headers!

# CORRECT - Skip header or use DictReader
with open("data.csv", newline='') as f:
    reader = csv.reader(f)
    next(reader)  # Skip header
    for row in reader:
        process(row)
```

**3. Assuming All Rows Have Same Number of Columns**
```python
# WRONG - Crashes on malformed CSV
with open("data.csv", newline='') as f:
    reader = csv.reader(f)
    for row in reader:
        a, b, c = row  # ValueError if row has wrong count!

# CORRECT - Validate row length
with open("data.csv", newline='') as f:
    reader = csv.reader(f)
    for row in reader:
        if len(row) != 3:
            continue  # Skip malformed rows
        a, b, c = row
```

**4. Not Handling Quoted Fields with Commas**
```python
# WRONG - Manual splitting breaks on commas in quotes
line = 'John,"123 Main St, Apt 4",NYC'
parts = line.split(',')  # ['John', '"123 Main St', ' Apt 4"', 'NYC']

# CORRECT - Use csv module for proper parsing
import csv
from io import StringIO
reader = csv.reader(StringIO(line))
parts = next(reader)  # ['John', '123 Main St, Apt 4', 'NYC']
```

**5. Writing Lists Without Converting to Strings**
```python
# WRONG - Writes repr of list, not proper CSV
data = [[1, 2], [3, 4]]
with open("data.csv", "w") as f:
    for row in data:
        f.write(str(row))  # Writes "[1, 2][3, 4]"!

# CORRECT - Use csv.writer for proper formatting
with open("data.csv", "w", newline='') as f:
    writer = csv.writer(f)
    writer.writerows(data)  # Writes "1,2\n3,4"
```