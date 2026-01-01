# Batched Reader Generator
# This solution demonstrates generators for batch processing

def batched_reader(n, batch_size=10):
    """Yield numbers 1 to n in batches.
    
    Args:
        n: Upper limit (inclusive)
        batch_size: Size of each batch
        
    Yields:
        Lists of numbers in batches
    """
    batch = []
    
    for num in range(1, n + 1):
        batch.append(num)
        
        # Yield when batch is full
        if len(batch) == batch_size:
            yield batch
            batch = []  # Start new batch
    
    # Don't forget the final partial batch
    if batch:
        yield batch

# Alternative implementation using list slicing
def batched_reader_v2(n, batch_size=10):
    """Alternative implementation using range slicing."""
    numbers = list(range(1, n + 1))
    for i in range(0, len(numbers), batch_size):
        yield numbers[i:i + batch_size]

# Test the generator
print("=== Batched Reader Demo ===")

print("\nBatches of 7 from 1-25:")
for batch in batched_reader(25, batch_size=7):
    print(f"  Batch: {batch}")

print("\nBatches of 5 from 1-12:")
for batch in batched_reader(12, batch_size=5):
    print(f"  Batch: {batch}")

print("\nBatches of 3 from 1-10:")
for batch in batched_reader(10, batch_size=3):
    print(f"  Batch: {batch}")

# Show memory efficiency
print("\n=== Memory Efficiency ===")
print("Generator yields batches one at a time,")
print("not loading all data into memory at once!")