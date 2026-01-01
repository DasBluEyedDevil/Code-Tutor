---
type: "ANALOGY"
title: "Project Overview: The System Detective"
---

**The Challenge:** Build a log analyzer that reads server log files, extracts insights, and generates reports.

**Real-world scenario:** You're a DevOps engineer. Your web server generates thousands of log entries daily. You need to:
- Find all errors and warnings
- Identify which pages are most visited
- Track response times
- Generate daily reports
- Export data for analysis

**What you'll build:**

1. **Log Parser:**
   - Read log files (text format)
   - Parse each line into structured data
   - Handle different log formats

2. **Analysis Engine:**
   - Count errors, warnings, info messages
   - Find slowest requests
   - Identify most common errors
   - Calculate statistics

3. **Report Generator:**
   - Summary statistics
   - Top errors
   - Export to CSV
   - Save analysis to JSON

**Sample log format:**
```
2024-01-15 10:23:45 [ERROR] Database connection failed
2024-01-15 10:23:46 [INFO] User login: alice@example.com
2024-01-15 10:23:47 [WARNING] Slow query: 2.5s
```

**Skills applied:**
- Reading text files with `with` statement
- String parsing and pattern matching
- Working with CSV for exports
- Working with JSON for structured data
- Using pathlib for file operations
- Error handling throughout

**Project structure:**
```
log_analyzer/
  ├── logs/           # Input log files
  ├── reports/        # Generated reports
  ├── exports/        # CSV/JSON exports
  └── analyzer.py     # Main program
```