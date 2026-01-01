from functools import wraps
import time

# Benchmark Decorator Class
# This solution demonstrates class-based decorators

class benchmark:
    """Decorator that benchmarks function execution."""
    
    def __init__(self, func):
        """Store the decorated function."""
        self.func = func
        self.call_count = 0
        self.total_time = 0.0
        # Preserve function metadata
        wraps(func)(self)
    
    def __call__(self, *args, **kwargs):
        """Execute function and track metrics."""
        # Measure execution time
        start = time.time()
        result = self.func(*args, **kwargs)
        elapsed = time.time() - start
        
        # Update statistics
        self.call_count += 1
        self.total_time += elapsed
        
        # Print timing info
        print(f"[{self.func.__name__}] Call #{self.call_count}: {elapsed:.6f}s")
        
        return result
    
    def print_stats(self):
        """Print accumulated statistics."""
        avg_time = self.total_time / self.call_count if self.call_count > 0 else 0
        print(f"\n=== Benchmark Stats for '{self.func.__name__}' ===")
        print(f"  Total calls: {self.call_count}")
        print(f"  Total time: {self.total_time:.6f}s")
        print(f"  Avg time: {avg_time:.6f}s")
    
    def reset(self):
        """Reset statistics."""
        self.call_count = 0
        self.total_time = 0.0

# Test the decorator
@benchmark
def calculate(n):
    """Calculate sum of range."""
    total = sum(range(n))
    return total

@benchmark
def slow_function():
    """Simulated slow function."""
    time.sleep(0.1)
    return "done"

# Call functions multiple times
print("=== Testing calculate() ===")
for i in [100000, 500000, 1000000]:
    result = calculate(i)
    print(f"  Result: {result:,}")

print("\n=== Testing slow_function() ===")
for _ in range(3):
    slow_function()

# Print statistics
calculate.print_stats()
slow_function.print_stats()