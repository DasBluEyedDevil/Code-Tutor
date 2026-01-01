def batched_reader(n, batch_size=10):
    """Yield numbers in batches"""
    # TODO: Implement generator that yields batches
    pass

# Test your generator
for batch in batched_reader(25, batch_size=7):
    print(f"Batch: {batch}")