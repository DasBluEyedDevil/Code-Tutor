---
type: "EXAMPLE"
title: "Complete Implementation: Log File Analyzer"
---

This production-ready log analyzer demonstrates:

**File I/O Concepts:**
1. **Reading files** with `with` statement
2. **Writing files** for reports
3. **CSV export** with csv module
4. **JSON export** for structured data
5. **pathlib** for cross-platform paths
6. **Error handling** throughout

**Data Processing:**
- Parsing log lines into structured objects
- Grouping and counting with Counter and defaultdict
- Filtering data (errors, warnings)
- Statistical analysis

**Real-world Features:**
- Handles malformed log lines
- Generates multiple output formats
- Creates directory structure
- Professional report formatting
- Reusable functions

**Why this matters:**
Log analysis is essential in DevOps, security, and troubleshooting. This project teaches real skills used daily by software engineers.

```python
"""Log File Analyzer - Complete File I/O Application

Demonstrates: file reading, parsing, CSV export, JSON export,
path operations, error handling, and data analysis.
"""

from pathlib import Path
import csv
import json
from datetime import datetime
from collections import defaultdict, Counter

# ============================================================================
# Log Entry Class
# ============================================================================

class LogEntry:
    """Represents a single log entry."""
    
    def __init__(self, timestamp, level, message):
        self.timestamp = timestamp
        self.level = level
        self.message = message
    
    def __repr__(self):
        return f"LogEntry({self.timestamp}, {self.level}, {self.message[:30]}...)"

# ============================================================================
# Log Parser
# ============================================================================

def parse_log_line(line):
    """Parse a single log line.
    
    Format: YYYY-MM-DD HH:MM:SS [LEVEL] Message
    
    Returns:
        LogEntry or None if parsing fails
    """
    try:
        # Split by brackets to get level
        parts = line.strip().split('[')
        if len(parts) < 2:
            return None
        
        # Extract timestamp (before first [)
        timestamp_str = parts[0].strip()
        
        # Extract level and message
        level_and_msg = parts[1].split(']', 1)
        if len(level_and_msg) < 2:
            return None
        
        level = level_and_msg[0].strip()
        message = level_and_msg[1].strip()
        
        return LogEntry(timestamp_str, level, message)
    
    except Exception:
        return None

def read_log_file(filepath):
    """Read and parse entire log file.
    
    Returns:
        list: List of LogEntry objects
    """
    entries = []
    
    try:
        with open(filepath, 'r') as file:
            for line_num, line in enumerate(file, 1):
                entry = parse_log_line(line)
                if entry:
                    entries.append(entry)
    
    except FileNotFoundError:
        print(f"Error: Log file '{filepath}' not found")
        return []
    except Exception as e:
        print(f"Error reading log file: {e}")
        return []
    
    return entries

# ============================================================================
# Analysis Functions
# ============================================================================

def analyze_logs(entries):
    """Analyze log entries and generate statistics.
    
    Returns:
        dict: Analysis results
    """
    # Count by level
    level_counts = Counter(entry.level for entry in entries)
    
    # Count messages
    message_counts = Counter(entry.message for entry in entries)
    
    # Group by level
    by_level = defaultdict(list)
    for entry in entries:
        by_level[entry.level].append(entry)
    
    # Statistics
    analysis = {
        'total_entries': len(entries),
        'level_counts': dict(level_counts),
        'error_count': level_counts.get('ERROR', 0),
        'warning_count': level_counts.get('WARNING', 0),
        'info_count': level_counts.get('INFO', 0),
        'top_errors': message_counts.most_common(5),
        'entries_by_level': {level: len(items) for level, items in by_level.items()}
    }
    
    return analysis

def find_errors(entries):
    """Find all error entries.
    
    Returns:
        list: Error LogEntry objects
    """
    return [entry for entry in entries if entry.level == 'ERROR']

def find_warnings(entries):
    """Find all warning entries.
    
    Returns:
        list: Warning LogEntry objects
    """
    return [entry for entry in entries if entry.level == 'WARNING']

# ============================================================================
# Export Functions
# ============================================================================

def export_to_csv(entries, filepath):
    """Export log entries to CSV."""
    with open(filepath, 'w', newline='') as file:
        writer = csv.writer(file)
        
        # Header
        writer.writerow(['Timestamp', 'Level', 'Message'])
        
        # Data
        for entry in entries:
            writer.writerow([entry.timestamp, entry.level, entry.message])
    
    print(f"✓ Exported {len(entries)} entries to {filepath}")

def export_analysis_to_json(analysis, filepath):
    """Export analysis results to JSON."""
    # Convert Counter objects to regular dicts for JSON
    json_data = {
        'total_entries': analysis['total_entries'],
        'level_counts': analysis['level_counts'],
        'error_count': analysis['error_count'],
        'warning_count': analysis['warning_count'],
        'info_count': analysis['info_count'],
        'top_errors': [[msg, count] for msg, count in analysis['top_errors']]
    }
    
    with open(filepath, 'w') as file:
        json.dump(json_data, file, indent=2)
    
    print(f"✓ Exported analysis to {filepath}")

# ============================================================================
# Report Generator
# ============================================================================

def generate_report(entries, analysis, output_dir):
    """Generate comprehensive text report."""
    report_path = Path(output_dir) / 'report.txt'
    
    with open(report_path, 'w') as file:
        file.write("=" * 70 + "\n")
        file.write("LOG ANALYSIS REPORT\n")
        file.write("=" * 70 + "\n\n")
        
        # Summary
        file.write("SUMMARY\n")
        file.write("-" * 70 + "\n")
        file.write(f"Total Entries: {analysis['total_entries']}\n")
        file.write(f"Errors: {analysis['error_count']}\n")
        file.write(f"Warnings: {analysis['warning_count']}\n")
        file.write(f"Info: {analysis['info_count']}\n\n")
        
        # Level breakdown
        file.write("LEVEL BREAKDOWN\n")
        file.write("-" * 70 + "\n")
        for level, count in analysis['level_counts'].items():
            percentage = (count / analysis['total_entries']) * 100
            file.write(f"{level:12} {count:6} ({percentage:.1f}%)\n")
        file.write("\n")
        
        # Top errors
        file.write("TOP 5 ERRORS\n")
        file.write("-" * 70 + "\n")
        for i, (message, count) in enumerate(analysis['top_errors'], 1):
            file.write(f"{i}. [{count}x] {message}\n")
        file.write("\n")
        
        file.write("=" * 70 + "\n")
    
    print(f"✓ Generated report: {report_path}")
    return report_path

# ============================================================================
# Main Application
# ============================================================================

def create_sample_log():
    """Create sample log file for testing."""
    log_entries = [
        "2024-01-15 10:00:00 [INFO] Server started",
        "2024-01-15 10:00:05 [INFO] User login: alice@example.com",
        "2024-01-15 10:00:10 [ERROR] Database connection failed",
        "2024-01-15 10:00:15 [WARNING] High memory usage: 85%",
        "2024-01-15 10:00:20 [INFO] User login: bob@example.com",
        "2024-01-15 10:00:25 [ERROR] Database connection failed",
        "2024-01-15 10:00:30 [INFO] Processing request: /api/users",
        "2024-01-15 10:00:35 [WARNING] Slow query: 2.5s",
        "2024-01-15 10:00:40 [ERROR] File not found: config.yaml",
        "2024-01-15 10:00:45 [INFO] Request completed: 200 OK",
        "2024-01-15 10:00:50 [ERROR] Database connection failed",
        "2024-01-15 10:00:55 [WARNING] Cache miss rate: 45%",
        "2024-01-15 10:01:00 [INFO] User logout: alice@example.com",
        "2024-01-15 10:01:05 [ERROR] Invalid API key",
        "2024-01-15 10:01:10 [INFO] Backup completed successfully",
    ]
    
    Path('logs').mkdir(exist_ok=True)
    log_file = Path('logs') / 'server.log'
    
    with open(log_file, 'w') as file:
        for entry in log_entries:
            file.write(entry + '\n')
    
    print(f"✓ Created sample log: {log_file}")
    return log_file

def run_analyzer():
    """Main analyzer workflow."""
    print("=" * 70)
    print("LOG FILE ANALYZER")
    print("=" * 70)
    
    # Create directories
    for dir_name in ['logs', 'reports', 'exports']:
        Path(dir_name).mkdir(exist_ok=True)
    
    # Create sample log
    print("\n1. Creating sample log file...")
    log_file = create_sample_log()
    
    # Read and parse
    print("\n2. Reading and parsing log file...")
    entries = read_log_file(log_file)
    print(f"✓ Parsed {len(entries)} log entries")
    
    # Analyze
    print("\n3. Analyzing logs...")
    analysis = analyze_logs(entries)
    print(f"✓ Found {analysis['error_count']} errors")
    print(f"✓ Found {analysis['warning_count']} warnings")
    
    # Generate report
    print("\n4. Generating report...")
    report_path = generate_report(entries, analysis, 'reports')
    
    # Export to CSV
    print("\n5. Exporting data...")
    errors = find_errors(entries)
    export_to_csv(errors, 'exports/errors.csv')
    export_to_csv(entries, 'exports/all_logs.csv')
    
    # Export analysis to JSON
    export_analysis_to_json(analysis, 'exports/analysis.json')
    
    # Display report
    print("\n6. Report Contents:")
    print("=" * 70)
    with open(report_path, 'r') as f:
        print(f.read())
    
    print("\n" + "=" * 70)
    print("✓ Analysis complete!")
    print(f"  - Report: {report_path}")
    print(f"  - CSV exports: exports/")
    print(f"  - JSON analysis: exports/analysis.json")
    print("=" * 70)

# Run the analyzer
if __name__ == '__main__':
    run_analyzer()
```
