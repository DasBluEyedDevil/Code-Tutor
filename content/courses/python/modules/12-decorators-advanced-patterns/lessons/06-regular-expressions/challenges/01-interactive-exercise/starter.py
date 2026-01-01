import re

def parse_log_line(line):
    # TODO: Create regex pattern with named groups
    pattern = r''
    
    # TODO: Match and extract groups
    match = None
    
    if match:
        return match.groupdict()
    return None

logs = [
    "2024-01-15 10:30:45 [ERROR] Database connection failed",
    "2024-01-15 10:31:12 [INFO] Application started",
    "2024-01-15 10:35:22 [WARNING] High memory usage"
]

for log in logs:
    parsed = parse_log_line(log)
    print(parsed)