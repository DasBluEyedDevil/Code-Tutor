# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Python Backend Development (python)
- **Module:** File I/O
- **Lesson:** Working with CSV Files (ID: 09_03)
- **Difficulty:** intermediate
- **Estimated Time:** 30 minutes

## Current Lesson Content

{
    "id":  "09_03",
    "contentSections":  [
                            {
                                "type":  "ANALOGY",
                                "title":  "The Concept: Spreadsheets as Text Files",
                                "content":  "**CSV = Comma-Separated Values** - A simple way to store spreadsheet/table data in plain text.\n\n**Real-world analogy: A Table Written in Text**\n\nImagine you have a spreadsheet:\n```\nName       | Age | City\n-----------|-----|----------\nAlice      | 25  | NYC\nBob        | 30  | LA\nCarol      | 28  | Chicago\n```\n\nIn CSV format, this becomes:\n```\nName,Age,City\nAlice,25,NYC\nBob,30,LA\nCarol,28,Chicago\n```\n\nEach row = one line, values separated by commas.\n\n**Why CSV is everywhere:**\n- Excel can open it\n- Google Sheets can open it\n- Databases can import/export it\n- Every programming language can read it\n- Simple, universal format\n\n**Real-world uses:**\n1. **Export data from apps** - Download your bank transactions, contacts, sales data\n2. **Data exchange** - Share data between different programs\n3. **Bulk uploads** - Import 1000 products into your store\n4. **Data analysis** - Load data into Python for processing\n\n**Python\u0027s csv module** handles:\n- Reading CSV files (parsing commas correctly)\n- Writing CSV files (formatting data correctly)\n- Handling special cases (commas in data, quotes, etc.)\n\n**Why use csv module instead of split(\u0027,\u0027):**\n\nBasic split breaks with:\n```\n\"Smith, John\",25,NYC  # Name has a comma!\n\"Product \"\"Best\"\" 2000\",19.99  # Quotes in name!\n```\n\nThe csv module handles these edge cases automatically.\n\n**Common operations:**\n- Read CSV → Process rows → Display/analyze\n- Create list of dicts → Write to CSV → Share file\n- Read CSV → Modify data → Write new CSV\n- Filter CSV data (select certain rows/columns)"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Code Example: Reading and Writing CSV Files",
                                "content":  "Key concepts:\n1. **csv.writer()** - Write lists to CSV (row by row)\n2. **csv.reader()** - Read CSV into lists\n3. **csv.DictWriter()** - Write dictionaries to CSV (keys = column names)\n4. **csv.DictReader()** - Read CSV into dictionaries (column names = keys)\n5. **newline=\u0027\u0027** - Required parameter when opening CSV files in write mode\n6. **writeheader()** - DictWriter method to write column headers\n7. **next(reader)** - Skip header row when processing data\n\nDictReader/DictWriter are usually easier because you can access columns by name!",
                                "code":  "import csv\n\n# Example 1: Writing a CSV file\nprint(\"=== Writing CSV File ===\")\n\n# Data to write (list of lists)\nstudents = [\n    [\"Name\", \"Age\", \"Grade\", \"City\"],  # Header row\n    [\"Alice\", 20, \"A\", \"NYC\"],\n    [\"Bob\", 22, \"B\", \"LA\"],\n    [\"Carol\", 21, \"A\", \"Chicago\"],\n    [\"David\", 23, \"C\", \"Boston\"]\n]\n\nwith open(\"students.csv\", \"w\", newline=\u0027\u0027) as file:\n    writer = csv.writer(file)\n    \n    # Write all rows\n    writer.writerows(students)\n    # Or write one at a time: writer.writerow(row)\n\nprint(\"✓ Created students.csv\\n\")\n\n# Example 2: Reading a CSV file\nprint(\"=== Reading CSV File ===\")\n\nwith open(\"students.csv\", \"r\") as file:\n    reader = csv.reader(file)\n    \n    print(\"File contents:\")\n    for row in reader:\n        print(row)  # Each row is a list\n\nprint(\"\")\n\n# Example 3: Reading with headers\nprint(\"=== Reading with Header Processing ===\")\n\nwith open(\"students.csv\", \"r\") as file:\n    reader = csv.reader(file)\n    \n    # Get header row first\n    headers = next(reader)\n    print(f\"Headers: {headers}\\n\")\n    \n    # Process data rows\n    print(\"Students:\")\n    for row in reader:\n        name, age, grade, city = row\n        print(f\"  {name} (age {age}): Grade {grade} from {city}\")\n\nprint(\"\")\n\n# Example 4: Using DictReader (rows as dictionaries)\nprint(\"=== DictReader - Rows as Dictionaries ===\")\n\nwith open(\"students.csv\", \"r\") as file:\n    reader = csv.DictReader(file)\n    \n    for row in reader:\n        # Each row is a dictionary!\n        print(f\"{row[\u0027Name\u0027]}: {row[\u0027Grade\u0027]} grade, {row[\u0027Age\u0027]} years old\")\n\nprint(\"\")\n\n# Example 5: Writing with DictWriter\nprint(\"=== DictWriter - Writing Dictionaries ===\")\n\nproducts = [\n    {\"Product\": \"Laptop\", \"Price\": 999.99, \"Stock\": 15},\n    {\"Product\": \"Mouse\", \"Price\": 29.99, \"Stock\": 50},\n    {\"Product\": \"Keyboard\", \"Price\": 79.99, \"Stock\": 30}\n]\n\nwith open(\"products.csv\", \"w\", newline=\u0027\u0027) as file:\n    fieldnames = [\"Product\", \"Price\", \"Stock\"]\n    writer = csv.DictWriter(file, fieldnames=fieldnames)\n    \n    # Write header\n    writer.writeheader()\n    \n    # Write data rows\n    writer.writerows(products)\n\nprint(\"✓ Created products.csv\\n\")\n\n# Read it back\nprint(\"Product catalog:\")\nwith open(\"products.csv\", \"r\") as file:\n    reader = csv.DictReader(file)\n    for row in reader:\n        print(f\"  {row[\u0027Product\u0027]}: ${row[\u0027Price\u0027]} ({row[\u0027Stock\u0027]} in stock)\")\n\nprint(\"\")\n\n# Example 6: Filtering and processing CSV data\nprint(\"=== Filtering CSV Data ===\")\n\nprint(\"Students with grade \u0027A\u0027:\")\nwith open(\"students.csv\", \"r\") as file:\n    reader = csv.DictReader(file)\n    \n    for row in reader:\n        if row[\u0027Grade\u0027] == \u0027A\u0027:\n            print(f\"  - {row[\u0027Name\u0027]} from {row[\u0027City\u0027]}\")\n\nprint(\"\")\n\n# Example 7: Handling special characters\nprint(\"=== Handling Special Characters ===\")\n\nspecial_data = [\n    [\"Name\", \"Company\", \"Salary\"],\n    [\"Smith, John\", \"Tech Corp\", \"75,000\"],  # Commas in data!\n    [\u0027Jane \"JJ\" Doe\u0027, \"StartUp Inc\", \"80,000\"],  # Quotes in data!\n]\n\nwith open(\"special.csv\", \"w\", newline=\u0027\u0027) as file:\n    writer = csv.writer(file)\n    writer.writerows(special_data)\n\nprint(\"✓ Written data with commas and quotes\")\n\n# Read it back - csv module handles it correctly\nwith open(\"special.csv\", \"r\") as file:\n    reader = csv.reader(file)\n    next(reader)  # Skip header\n    \n    print(\"\\nData read correctly:\")\n    for row in reader:\n        print(f\"  Name: {row[0]}\")\n        print(f\"  Company: {row[1]}\")\n        print(f\"  Salary: {row[2]}\\n\")\n\nprint(\"✓ CSV module handled special characters correctly!\")",
                                "language":  "python"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "Syntax Breakdown: CSV Operations",
                                "content":  "**Importing csv module:**\n```python\nimport csv\n```\n\n**Writing CSV - Basic (lists):**\n```python\nwith open(\u0027data.csv\u0027, \u0027w\u0027, newline=\u0027\u0027) as file:\n    writer = csv.writer(file)\n    \n    # Write one row\n    writer.writerow([\u0027Name\u0027, \u0027Age\u0027, \u0027City\u0027])\n    \n    # Write multiple rows\n    rows = [\n        [\u0027Alice\u0027, 25, \u0027NYC\u0027],\n        [\u0027Bob\u0027, 30, \u0027LA\u0027]\n    ]\n    writer.writerows(rows)\n```\n\n**Reading CSV - Basic (lists):**\n```python\nwith open(\u0027data.csv\u0027, \u0027r\u0027) as file:\n    reader = csv.reader(file)\n    \n    for row in reader:\n        # row is a list: [\u0027Alice\u0027, \u002725\u0027, \u0027NYC\u0027]\n        print(row[0], row[1], row[2])\n```\n\n**Writing CSV - Dictionaries (recommended):**\n```python\nwith open(\u0027data.csv\u0027, \u0027w\u0027, newline=\u0027\u0027) as file:\n    fieldnames = [\u0027Name\u0027, \u0027Age\u0027, \u0027City\u0027]\n    writer = csv.DictWriter(file, fieldnames=fieldnames)\n    \n    writer.writeheader()  # Write column names\n    \n    writer.writerow({\u0027Name\u0027: \u0027Alice\u0027, \u0027Age\u0027: 25, \u0027City\u0027: \u0027NYC\u0027})\n    \n    # Or write multiple\n    data = [\n        {\u0027Name\u0027: \u0027Bob\u0027, \u0027Age\u0027: 30, \u0027City\u0027: \u0027LA\u0027},\n        {\u0027Name\u0027: \u0027Carol\u0027, \u0027Age\u0027: 28, \u0027City\u0027: \u0027Chicago\u0027}\n    ]\n    writer.writerows(data)\n```\n\n**Reading CSV - Dictionaries (recommended):**\n```python\nwith open(\u0027data.csv\u0027, \u0027r\u0027) as file:\n    reader = csv.DictReader(file)\n    \n    for row in reader:\n        # row is a dict: {\u0027Name\u0027: \u0027Alice\u0027, \u0027Age\u0027: \u002725\u0027, \u0027City\u0027: \u0027NYC\u0027}\n        print(row[\u0027Name\u0027], row[\u0027Age\u0027], row[\u0027City\u0027])\n```\n\n**Important: newline=\u0027\u0027 parameter**\n\nAlways use `newline=\u0027\u0027` when opening CSV files in write mode:\n```python\nwith open(\u0027data.csv\u0027, \u0027w\u0027, newline=\u0027\u0027) as file:  # newline=\u0027\u0027 required!\n```\n\nWithout it, you get extra blank lines on Windows.\n\n**Skipping header row:**\n```python\nwith open(\u0027data.csv\u0027, \u0027r\u0027) as file:\n    reader = csv.reader(file)\n    next(reader)  # Skip first row (header)\n    \n    for row in reader:\n        # Process data rows only\n        pass\n```\n\n**Common patterns:**\n\n**1. Read CSV into list of dictionaries:**\n```python\nwith open(\u0027data.csv\u0027, \u0027r\u0027) as file:\n    reader = csv.DictReader(file)\n    data = list(reader)  # Convert to list\n\n# Now you have: [{\u0027Name\u0027: \u0027Alice\u0027, ...}, {...}, ...]\n```\n\n**2. Filter CSV data:**\n```python\nwith open(\u0027data.csv\u0027, \u0027r\u0027) as file:\n    reader = csv.DictReader(file)\n    \n    for row in reader:\n        if int(row[\u0027Age\u0027]) \u003e= 25:  # Filter condition\n            print(row[\u0027Name\u0027])\n```\n\n**3. Transform CSV (read → modify → write):**\n```python\n# Read\nwith open(\u0027input.csv\u0027, \u0027r\u0027) as infile:\n    reader = csv.DictReader(infile)\n    data = list(reader)\n\n# Modify\nfor row in data:\n    row[\u0027Age\u0027] = int(row[\u0027Age\u0027]) + 1  # Add 1 to age\n\n# Write\nwith open(\u0027output.csv\u0027, \u0027w\u0027, newline=\u0027\u0027) as outfile:\n    writer = csv.DictWriter(outfile, fieldnames=data[0].keys())\n    writer.writeheader()\n    writer.writerows(data)\n```\n\n**CSV vs regular text files:**\n\n**Regular text file:**\n```python\nwith open(\u0027file.txt\u0027, \u0027w\u0027) as f:\n    f.write(\u0027Hello\\n\u0027)\n```\n\n**CSV file:**\n```python\nwith open(\u0027file.csv\u0027, \u0027w\u0027, newline=\u0027\u0027) as f:\n    writer = csv.writer(f)\n    writer.writerow([\u0027Hello\u0027, \u0027World\u0027])\n```\n\n**Key differences:**\n- CSV needs `import csv`\n- CSV needs `newline=\u0027\u0027` when writing\n- CSV handles commas/quotes in data automatically\n- DictReader/DictWriter make column access easier"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "Key Takeaways",
                                "content":  "- **CSV = Comma-Separated Values** - Universal format for tabular/spreadsheet data. Works with Excel, databases, and all programming languages.\n- **import csv** - Python\u0027s built-in module for reading and writing CSV files. Handles edge cases (commas in data, quotes) automatically.\n- **csv.writer()** writes lists to CSV. **csv.DictWriter()** writes dictionaries (recommended - easier to use).\n- **csv.reader()** reads CSV as lists. **csv.DictReader()** reads as dictionaries (recommended - access by column name).\n- **Always use newline=\u0027\u0027** when opening CSV files in write mode: open(\u0027file.csv\u0027, \u0027w\u0027, newline=\u0027\u0027). Required to avoid extra blank lines.\n- **writeheader()** - DictWriter method to write column names as first row. Don\u0027t forget to call this!\n- **CSV values are strings!** Even numbers come back as strings. Always convert: int(row[\u0027Age\u0027]), float(row[\u0027Price\u0027]).\n- **DictReader/DictWriter advantages:** Access columns by name (row[\u0027Name\u0027]) instead of index (row[0]). More readable and less error-prone.\n- **Never use split(\u0027,\u0027)** for CSV! Use csv module instead. It correctly handles commas in data, quotes, and other edge cases.\n- **Common pattern:** Read CSV with DictReader → Process/filter data → Write new CSV with DictWriter."
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "09_03-challenge-3",
                           "title":  "Interactive Exercise: Student Grade Manager",
                           "description":  "Create a student grade management system that:\n1. Writes student records to \"grades.csv\" (Name, Subject, Grade, Score)\n2. Reads and displays all records\n3. Calculates average score per student\n4. Finds all students with grade \u0027A\u0027\n\nUse DictWriter and DictReader for easier column access.\n\n**Your task:**\nImplement the functions below.\n\n**Starter code:**",
                           "instructions":  "Create a student grade management system that:\n1. Writes student records to \"grades.csv\" (Name, Subject, Grade, Score)\n2. Reads and displays all records\n3. Calculates average score per student\n4. Finds all students with grade \u0027A\u0027\n\nUse DictWriter and DictReader for easier column access.\n\n**Your task:**\nImplement the functions below.\n\n**Starter code:**",
                           "starterCode":  "import csv\n\ndef create_grades_file(records):\n    \"\"\"Write grade records to CSV.\n    \n    Args:\n        records: List of dictionaries with keys: Name, Subject, Grade, Score\n    \"\"\"\n    # TODO: Open grades.csv in write mode with newline=\u0027\u0027\n    # TODO: Create DictWriter with fieldnames\n    # TODO: Write header\n    # TODO: Write all records\n    pass\n\ndef read_all_grades():\n    \"\"\"Read and display all grade records.\"\"\"\n    # TODO: Open grades.csv in read mode\n    # TODO: Create DictReader\n    # TODO: Read and print each row\n    pass\n\ndef calculate_averages():\n    \"\"\"Calculate average score per student.\n    \n    Returns:\n        dict: {student_name: average_score}\n    \"\"\"\n    # TODO: Read CSV with DictReader\n    # TODO: Group scores by student name\n    # TODO: Calculate average for each student\n    # TODO: Return dictionary\n    pass\n\ndef find_a_students():\n    \"\"\"Find all students with grade \u0027A\u0027.\n    \n    Returns:\n        list: List of student names with grade A\n    \"\"\"\n    # TODO: Read CSV with DictReader\n    # TODO: Filter rows where Grade == \u0027A\u0027\n    # TODO: Return list of names\n    pass\n\n# Test data\ngrade_records = [\n    {\u0027Name\u0027: \u0027Alice\u0027, \u0027Subject\u0027: \u0027Math\u0027, \u0027Grade\u0027: \u0027A\u0027, \u0027Score\u0027: \u002795\u0027},\n    {\u0027Name\u0027: \u0027Alice\u0027, \u0027Subject\u0027: \u0027English\u0027, \u0027Grade\u0027: \u0027B\u0027, \u0027Score\u0027: \u002785\u0027},\n    {\u0027Name\u0027: \u0027Bob\u0027, \u0027Subject\u0027: \u0027Math\u0027, \u0027Grade\u0027: \u0027B\u0027, \u0027Score\u0027: \u002788\u0027},\n    {\u0027Name\u0027: \u0027Bob\u0027, \u0027Subject\u0027: \u0027English\u0027, \u0027Grade\u0027: \u0027A\u0027, \u0027Score\u0027: \u002792\u0027},\n    {\u0027Name\u0027: \u0027Carol\u0027, \u0027Subject\u0027: \u0027Math\u0027, \u0027Grade\u0027: \u0027A\u0027, \u0027Score\u0027: \u002797\u0027},\n    {\u0027Name\u0027: \u0027Carol\u0027, \u0027Subject\u0027: \u0027English\u0027, \u0027Grade\u0027: \u0027A\u0027, \u0027Score\u0027: \u002794\u0027},\n]\n\n# Test your functions\nprint(\"Creating grades file...\")\ncreate_grades_file(grade_records)\n\nprint(\"\\nAll grades:\")\nread_all_grades()\n\nprint(\"\\nAverage scores:\")\naverages = calculate_averages()\nfor name, avg in averages.items():\n    print(f\"  {name}: {avg:.1f}\")\n\nprint(\"\\nStudents with A grades:\")\na_students = find_a_students()\nfor name in a_students:\n    print(f\"  - {name}\")",
                           "solution":  "import csv\n\n# Student Grade Manager\n# This solution demonstrates CSV reading and writing with DictReader/DictWriter\n\ndef create_grades_file(records):\n    \"\"\"Write grade records to CSV.\"\"\"\n    # Step 1: Open file with newline=\u0027\u0027 for proper CSV handling\n    with open(\u0027grades.csv\u0027, \u0027w\u0027, newline=\u0027\u0027) as file:\n        # Step 2: Create DictWriter with column names\n        fieldnames = [\u0027Name\u0027, \u0027Subject\u0027, \u0027Grade\u0027, \u0027Score\u0027]\n        writer = csv.DictWriter(file, fieldnames=fieldnames)\n        \n        # Step 3: Write header row\n        writer.writeheader()\n        \n        # Step 4: Write all records\n        writer.writerows(records)\n    \n    print(f\"Created grades.csv with {len(records)} records\")\n\ndef read_all_grades():\n    \"\"\"Read and display all grade records.\"\"\"\n    with open(\u0027grades.csv\u0027, \u0027r\u0027) as file:\n        reader = csv.DictReader(file)\n        \n        # Print header\n        print(f\"{\u0027Name\u0027:\u003c10} {\u0027Subject\u0027:\u003c10} {\u0027Grade\u0027:\u003c6} {\u0027Score\u0027:\u003c6}\")\n        print(\"-\" * 32)\n        \n        # Print each row\n        for row in reader:\n            print(f\"{row[\u0027Name\u0027]:\u003c10} {row[\u0027Subject\u0027]:\u003c10} {row[\u0027Grade\u0027]:\u003c6} {row[\u0027Score\u0027]:\u003c6}\")\n\ndef calculate_averages():\n    \"\"\"Calculate average score per student.\"\"\"\n    # Step 1: Collect all scores per student\n    student_scores = {}\n    \n    with open(\u0027grades.csv\u0027, \u0027r\u0027) as file:\n        reader = csv.DictReader(file)\n        \n        for row in reader:\n            name = row[\u0027Name\u0027]\n            score = int(row[\u0027Score\u0027])\n            \n            if name not in student_scores:\n                student_scores[name] = []\n            student_scores[name].append(score)\n    \n    # Step 2: Calculate average for each student\n    averages = {}\n    for name, scores in student_scores.items():\n        averages[name] = sum(scores) / len(scores)\n    \n    return averages\n\ndef find_a_students():\n    \"\"\"Find all students with grade \u0027A\u0027.\"\"\"\n    a_students = set()  # Use set to avoid duplicates\n    \n    with open(\u0027grades.csv\u0027, \u0027r\u0027) as file:\n        reader = csv.DictReader(file)\n        \n        for row in reader:\n            if row[\u0027Grade\u0027] == \u0027A\u0027:\n                a_students.add(row[\u0027Name\u0027])\n    \n    return list(a_students)\n\n# Test data\ngrade_records = [\n    {\u0027Name\u0027: \u0027Alice\u0027, \u0027Subject\u0027: \u0027Math\u0027, \u0027Grade\u0027: \u0027A\u0027, \u0027Score\u0027: \u002795\u0027},\n    {\u0027Name\u0027: \u0027Alice\u0027, \u0027Subject\u0027: \u0027English\u0027, \u0027Grade\u0027: \u0027B\u0027, \u0027Score\u0027: \u002785\u0027},\n    {\u0027Name\u0027: \u0027Bob\u0027, \u0027Subject\u0027: \u0027Math\u0027, \u0027Grade\u0027: \u0027B\u0027, \u0027Score\u0027: \u002788\u0027},\n    {\u0027Name\u0027: \u0027Bob\u0027, \u0027Subject\u0027: \u0027English\u0027, \u0027Grade\u0027: \u0027A\u0027, \u0027Score\u0027: \u002792\u0027},\n    {\u0027Name\u0027: \u0027Carol\u0027, \u0027Subject\u0027: \u0027Math\u0027, \u0027Grade\u0027: \u0027A\u0027, \u0027Score\u0027: \u002797\u0027},\n    {\u0027Name\u0027: \u0027Carol\u0027, \u0027Subject\u0027: \u0027English\u0027, \u0027Grade\u0027: \u0027A\u0027, \u0027Score\u0027: \u002794\u0027},\n]\n\n# Test the functions\nprint(\"Creating grades file...\")\ncreate_grades_file(grade_records)\n\nprint(\"\\nAll grades:\")\nread_all_grades()\n\nprint(\"\\nAverage scores:\")\naverages = calculate_averages()\nfor name, avg in averages.items():\n    print(f\"  {name}: {avg:.1f}\")\n\nprint(\"\\nStudents with A grades:\")\na_students = find_a_students()\nfor name in a_students:\n    print(f\"  - {name}\")",
                           "language":  "python",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Code runs without errors",
                                                 "expectedOutput":  "",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "Use csv.DictWriter(file, fieldnames=[\u0027Name\u0027, \u0027Subject\u0027, \u0027Grade\u0027, \u0027Score\u0027]) for writing. Use csv.DictReader(file) for reading. Remember newline=\u0027\u0027 when opening for write!"
                                         }
                                     ],
                           "commonMistakes":  [
                                                  {
                                                      "mistake":  "Forgetting the colon after if/for/while",
                                                      "consequence":  "SyntaxError",
                                                      "correction":  "Add : at the end of the line"
                                                  },
                                                  {
                                                      "mistake":  "Using = instead of == for comparison",
                                                      "consequence":  "Assignment instead of comparison",
                                                      "correction":  "Use == for equality checks"
                                                  },
                                                  {
                                                      "mistake":  "Incorrect indentation",
                                                      "consequence":  "IndentationError",
                                                      "correction":  "Use consistent 4-space indentation"
                                                  }
                                              ],
                           "difficulty":  "beginner"
                       }
                   ],
    "difficulty":  "intermediate",
    "title":  "Working with CSV Files",
    "estimatedMinutes":  30
}

## Review Instructions

Perform the following analysis:

### 1. Accuracy Check
- Verify all code examples are syntactically correct
- Confirm technical explanations match current python documentation
- Search the web for the latest python version and verify examples work with it
- Flag any deprecated APIs, syntax, or patterns

### 2. Completeness Check
- Does the lesson cover all concepts needed for a beginner to understand this topic?
- Are there missing explanations between concepts that would confuse a learner?
- Does the lesson have:
  - [ ] A clear analogy or real-world example (ANALOGY section)
  - [ ] Theoretical explanation (THEORY section)
  - [ ] Working code example (EXAMPLE section)
  - [ ] Common mistakes to avoid (WARNING section)
  - [ ] At least one practice challenge

### 3. Freshness Check
- Search for "python Working with CSV Files 2024 2025" to find latest practices
- Compare lesson content against current best practices
- Identify any outdated patterns or recommendations

### 4. Pedagogical Gap Analysis
- What prerequisite knowledge is assumed but not explained?
- What follow-up questions would a learner likely have?
- What practical applications or use cases are missing?
- Are the challenges appropriately scaffolded for the difficulty level?

## Output Format

Provide your review as structured JSON:

```json
{
  "lessonId": "09_03",
  "reviewDate": "YYYY-MM-DD",
  "overallScore": 1-10,
  "accuracy": {
    "score": 1-10,
    "issues": ["issue 1", "issue 2"],
    "recommendations": ["fix 1", "fix 2"]
  },
  "completeness": {
    "score": 1-10,
    "missingSections": ["section type needed"],
    "gaps": ["gap 1", "gap 2"],
    "recommendations": ["add X", "expand Y"]
  },
  "freshness": {
    "score": 1-10,
    "outdatedItems": ["item 1"],
    "currentVersion": "language version checked",
    "recommendations": ["update X to Y"]
  },
  "pedagogicalGaps": {
    "score": 1-10,
    "missingPrerequisites": ["concept 1"],
    "unansweredQuestions": ["question learner would have"],
    "missingUseCases": ["practical application"],
    "recommendations": ["add section on X"]
  },
  "contentLengthIssues": {
    "shortSections": [
      {"sectionTitle": "title", "currentLength": 42, "recommendation": "expand to explain X"}
    ]
  },
  "suggestedNewContent": [
    {
      "sectionType": "THEORY|EXAMPLE|WARNING|etc",
      "title": "suggested title",
      "contentOutline": "what this section should cover"
    }
  ],
  "priority": "HIGH|MEDIUM|LOW"
}
```

