# Identify which operations would benefit from async

import time

def process_data():
    # Operation 1: Read a file
    with open('data.txt') as f:
        data = f.read()
    
    # Operation 2: Calculate statistics
    total = sum(range(1000000))
    
    # Operation 3: Make API request
    # response = requests.get('https://api.example.com/data')
    
    # Operation 4: Sort a large list
    sorted_data = sorted(range(100000), reverse=True)
    
    # Operation 5: Save to database
    # db.save(data)
    
    return total

# TODO: Add comments above each operation:
# - "BLOCKING I/O - async would help" for I/O operations
# - "CPU-BOUND - async won't help" for computation