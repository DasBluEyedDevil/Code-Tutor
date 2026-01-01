---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **CSV = Comma-Separated Values** - Universal format for tabular/spreadsheet data. Works with Excel, databases, and all programming languages.
- **import csv** - Python's built-in module for reading and writing CSV files. Handles edge cases (commas in data, quotes) automatically.
- **csv.writer()** writes lists to CSV. **csv.DictWriter()** writes dictionaries (recommended - easier to use).
- **csv.reader()** reads CSV as lists. **csv.DictReader()** reads as dictionaries (recommended - access by column name).
- **Always use newline=''** when opening CSV files in write mode: open('file.csv', 'w', newline=''). Required to avoid extra blank lines.
- **writeheader()** - DictWriter method to write column names as first row. Don't forget to call this!
- **CSV values are strings!** Even numbers come back as strings. Always convert: int(row['Age']), float(row['Price']).
- **DictReader/DictWriter advantages:** Access columns by name (row['Name']) instead of index (row[0]). More readable and less error-prone.
- **Never use split(',')** for CSV! Use csv module instead. It correctly handles commas in data, quotes, and other edge cases.
- **Common pattern:** Read CSV with DictReader → Process/filter data → Write new CSV with DictWriter.