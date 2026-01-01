import re

# Log Parser with Regex
# This solution demonstrates named groups in regular expressions

def parse_log_line(line):
    """Parse a log line and extract components.
    
    Expected format: YYYY-MM-DD HH:MM:SS [LEVEL] Message
    """
    # Create regex pattern with named groups
    pattern = r'''
        (?P<timestamp>\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2})  # Date and time
        \s+                                                     # Whitespace
        \[(?P<level>\w+)\]                                     # Log level in brackets
        \s+                                                     # Whitespace
        (?P<message>.+)                                         # Message (rest of line)
    '''
    
    # Match using VERBOSE flag for readable pattern
    match = re.match(pattern, line, re.VERBOSE)
    
    if match:
        return match.groupdict()
    return None

# Alternative simpler pattern (single line)
def parse_log_simple(line):
    """Simpler version without verbose mode."""
    pattern = r'(?P<timestamp>[\d-]+\s[\d:]+)\s\[(?P<level>\w+)\]\s(?P<message>.+)'
    match = re.match(pattern, line)
    return match.groupdict() if match else None

# Test logs
logs = [
    "2024-01-15 10:30:45 [ERROR] Database connection failed",
    "2024-01-15 10:31:12 [INFO] Application started",
    "2024-01-15 10:35:22 [WARNING] High memory usage",
    "2024-01-15 11:00:00 [DEBUG] Processing request #42"
]

print("=== Log Parser Demo ===")

# Parse and display each log
for log in logs:
    parsed = parse_log_line(log)
    if parsed:
        print(f"\nTimestamp: {parsed['timestamp']}")
        print(f"Level:     {parsed['level']}")
        print(f"Message:   {parsed['message']}")

# Count by level
print("\n=== Summary ===")
levels = [parse_log_line(log)['level'] for log in logs]
for level in set(levels):
    count = levels.count(level)
    print(f"  {level}: {count}")