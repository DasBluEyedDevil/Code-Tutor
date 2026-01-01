---
type: "ANALOGY"
title: "The Concept: Spreadsheets as Text Files"
---

**CSV = Comma-Separated Values** - A simple way to store spreadsheet/table data in plain text.

**Real-world analogy: A Table Written in Text**

Imagine you have a spreadsheet:
```
Name       | Age | City
-----------|-----|----------
Alice      | 25  | NYC
Bob        | 30  | LA
Carol      | 28  | Chicago
```

In CSV format, this becomes:
```
Name,Age,City
Alice,25,NYC
Bob,30,LA
Carol,28,Chicago
```

Each row = one line, values separated by commas.

**Why CSV is everywhere:**
- Excel can open it
- Google Sheets can open it
- Databases can import/export it
- Every programming language can read it
- Simple, universal format

**Real-world uses:**
1. **Export data from apps** - Download your bank transactions, contacts, sales data
2. **Data exchange** - Share data between different programs
3. **Bulk uploads** - Import 1000 products into your store
4. **Data analysis** - Load data into Python for processing

**Python's csv module** handles:
- Reading CSV files (parsing commas correctly)
- Writing CSV files (formatting data correctly)
- Handling special cases (commas in data, quotes, etc.)

**Why use csv module instead of split(','):**

Basic split breaks with:
```
"Smith, John",25,NYC  # Name has a comma!
"Product ""Best"" 2000",19.99  # Quotes in name!
```

The csv module handles these edge cases automatically.

**Common operations:**
- Read CSV → Process rows → Display/analyze
- Create list of dicts → Write to CSV → Share file
- Read CSV → Modify data → Write new CSV
- Filter CSV data (select certain rows/columns)