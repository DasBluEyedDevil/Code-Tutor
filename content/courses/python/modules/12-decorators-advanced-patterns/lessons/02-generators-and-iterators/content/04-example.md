---
type: "EXAMPLE"
title: "Code Example: Generator Expressions and Pipelines"
---

**Generator expressions:**
- Syntax: `(expression for item in iterable)`
- Like list comprehension but with ()
- Lazy evaluation
- Much more memory efficient

**Generator pipelines:**
- Chain generators together
- Each stage processes one item at a time
- Very efficient for large datasets
- Data flows through on demand

**Benefits:**
```python
# Instead of:
data = read_all()           # 1GB in memory
filtered = filter_data(data) # 2GB in memory
result = process(filtered)   # 3GB in memory

# Do this:
result = process(filter_data(read_all()))
# Only processes one item at a time!
```

```python
print("=== Generator Expression vs List Comprehension ===")

import sys

# List comprehension - creates full list
squares_list = [x**2 for x in range(1000)]
print(f"List size: {sys.getsizeof(squares_list)} bytes")

# Generator expression - lazy evaluation
squares_gen = (x**2 for x in range(1000))
print(f"Generator size: {sys.getsizeof(squares_gen)} bytes")
print(f"Memory savings: {sys.getsizeof(squares_list) / sys.getsizeof(squares_gen):.1f}x\n")

print("=== Generator Pipeline ===")

def read_numbers():
    """Simulate reading data"""
    for i in range(1, 11):
        print(f"  Reading: {i}")
        yield i

def square(numbers):
    """Square each number"""
    for n in numbers:
        print(f"  Squaring: {n}")
        yield n ** 2

def filter_large(numbers, threshold=50):
    """Filter numbers above threshold"""
    for n in numbers:
        if n > threshold:
            print(f"  Filtering: {n} (kept)")
            yield n
        else:
            print(f"  Filtering: {n} (dropped)")

# Build pipeline (no execution yet!)
print("Building pipeline (lazy - nothing happens yet)...")
pipeline = filter_large(square(read_numbers()), threshold=50)
print(f"Pipeline created: {pipeline}\n")

print("Executing pipeline (pulling values)...")
results = list(pipeline)
print(f"\nFinal results: {results}")

print("\n=== Practical Example: Data Processing ===")

def read_log_lines(filename):
    """Read log file line by line"""
    with open(filename) as f:
        for line in f:
            yield line.strip()

def parse_log_line(lines):
    """Parse log lines into structured data"""
    for line in lines:
        parts = line.split('|')
        if len(parts) >= 3:
            yield {
                'timestamp': parts[0],
                'level': parts[1],
                'message': parts[2]
            }

def filter_errors(logs):
    """Filter only ERROR level logs"""
    for log in logs:
        if log['level'] == 'ERROR':
            yield log

# Create sample log file
with open('app.log', 'w') as f:
    f.write('2024-01-01 10:00|INFO|Application started\n')
    f.write('2024-01-01 10:05|ERROR|Database connection failed\n')
    f.write('2024-01-01 10:10|INFO|Retrying connection\n')
    f.write('2024-01-01 10:15|ERROR|Authentication failed\n')
    f.write('2024-01-01 10:20|INFO|Application stopped\n')

print("Processing logs (memory efficient):")
log_pipeline = filter_errors(parse_log_line(read_log_lines('app.log')))

for error in log_pipeline:
    print(f"  ERROR at {error['timestamp']}: {error['message']}")

print("\n=== Generator with Cleanup ===")

def managed_resource():
    """Generator with setup and teardown"""
    print("  Setting up resource...")
    resource = "Database Connection"
    try:
        for i in range(3):
            print(f"  Using resource: {i}")
            yield resource
    finally:
        print("  Cleaning up resource...")

print("Using generator with cleanup:")
for item in managed_resource():
    print(f"  Got: {item}")

import os
os.remove('app.log')
```
