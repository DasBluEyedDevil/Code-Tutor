from functools import wraps
import time

class benchmark:
    # TODO: Implement __init__ to receive function
    # TODO: Track call_count and total_time
    
    def __init__(self, func):
        pass
    
    # TODO: Implement __call__ to wrap function
    def __call__(self, *args, **kwargs):
        pass
    
    # TODO: Add method to print stats
    def print_stats(self):
        pass

# Test the decorator
@benchmark
def calculate(n):
    total = sum(range(n))
    return total

# TODO: Call function multiple times
# TODO: Print statistics