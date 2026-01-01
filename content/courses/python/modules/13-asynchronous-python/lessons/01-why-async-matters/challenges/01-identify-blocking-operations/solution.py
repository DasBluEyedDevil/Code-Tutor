# Identify which operations would benefit from async

import time

def process_data():
    # Operation 1: Read a file
    # BLOCKING I/O - async would help (use aiofiles)
    with open('data.txt') as f:
        data = f.read()
    
    # Operation 2: Calculate statistics
    # CPU-BOUND - async won't help (use multiprocessing)
    total = sum(range(1000000))
    
    # Operation 3: Make API request
    # BLOCKING I/O - async would help (use httpx or aiohttp)
    # response = requests.get('https://api.example.com/data')
    
    # Operation 4: Sort a large list
    # CPU-BOUND - async won't help (pure computation)
    sorted_data = sorted(range(100000), reverse=True)
    
    # Operation 5: Save to database
    # BLOCKING I/O - async would help (use async DB driver)
    # db.save(data)
    
    return total

# Summary:
# - File I/O: Blocking (async helps)
# - API requests: Blocking (async helps)
# - Database: Blocking (async helps)
# - Sorting/math: CPU-bound (async doesn't help)

print("I/O-bound operations benefit from async:")
print("  - File reading/writing")
print("  - Network requests")
print("  - Database queries")
print("\nCPU-bound operations need multiprocessing:")
print("  - Calculations")
print("  - Sorting")
print("  - Data processing")