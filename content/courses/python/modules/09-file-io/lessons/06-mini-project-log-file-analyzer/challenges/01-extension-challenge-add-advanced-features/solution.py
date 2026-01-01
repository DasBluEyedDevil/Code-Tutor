from datetime import datetime
from collections import defaultdict

# Advanced Log Analyzer
# This solution demonstrates time-based analysis with datetime

def parse_timestamp(timestamp_str):
    """Parse timestamp string to datetime object."""
    try:
        return datetime.strptime(timestamp_str, '%Y-%m-%d %H:%M:%S')
    except ValueError:
        return None

def group_by_hour(entries):
    """Group log entries by hour."""
    # Use defaultdict for automatic list creation
    grouped = defaultdict(list)
    
    for entry in entries:
        # Parse the timestamp
        dt = parse_timestamp(entry['timestamp'])
        if dt:
            # Extract hour and group
            hour = dt.hour
            grouped[hour].append(entry)
    
    return dict(grouped)

def find_peak_error_hour(entries):
    """Find hour with most errors."""
    # Step 1: Filter errors only
    errors = [e for e in entries if e.get('level') == 'ERROR']
    
    if not errors:
        return (None, 0)
    
    # Step 2: Count errors per hour
    hour_counts = defaultdict(int)
    for entry in errors:
        dt = parse_timestamp(entry['timestamp'])
        if dt:
            hour_counts[dt.hour] += 1
    
    # Step 3: Find hour with max errors
    peak_hour = max(hour_counts, key=hour_counts.get)
    return (peak_hour, hour_counts[peak_hour])

def filter_by_date_range(entries, start_date, end_date):
    """Filter entries by date range."""
    filtered = []
    for entry in entries:
        dt = parse_timestamp(entry['timestamp'])
        if dt and start_date <= dt <= end_date:
            filtered.append(entry)
    return filtered

def filter_by_level(entries, level):
    """Filter entries by log level."""
    return [e for e in entries if e.get('level') == level.upper()]

def calculate_error_rate(entries):
    """Calculate error rate percentage."""
    if not entries:
        return 0.0
    error_count = sum(1 for e in entries if e.get('level') == 'ERROR')
    return (error_count / len(entries)) * 100

# Test the functions
test_entries = [
    {'timestamp': '2024-01-15 09:00:00', 'level': 'INFO', 'message': 'Start'},
    {'timestamp': '2024-01-15 09:30:00', 'level': 'ERROR', 'message': 'Failed'},
    {'timestamp': '2024-01-15 10:00:00', 'level': 'ERROR', 'message': 'Timeout'},
    {'timestamp': '2024-01-15 10:15:00', 'level': 'ERROR', 'message': 'Connection'},
    {'timestamp': '2024-01-15 11:00:00', 'level': 'INFO', 'message': 'OK'},
]

print("=== Advanced Log Analysis ===")

print("\n1. Group by hour:")
for hour, items in group_by_hour(test_entries).items():
    print(f"  Hour {hour}:00 - {len(items)} entries")

print("\n2. Peak error hour:")
hour, count = find_peak_error_hour(test_entries)
print(f"  Hour {hour}:00 with {count} errors")

print("\n3. Error rate:")
rate = calculate_error_rate(test_entries)
print(f"  {rate:.1f}% of entries are errors")

print("\n4. Filter by level (ERROR):")
errors = filter_by_level(test_entries, 'ERROR')
for e in errors:
    print(f"  {e['timestamp']}: {e['message']}")