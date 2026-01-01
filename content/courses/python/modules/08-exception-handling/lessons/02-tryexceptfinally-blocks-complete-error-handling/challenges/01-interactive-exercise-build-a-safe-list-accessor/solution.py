# Safe List Accessor
# This solution demonstrates try/except/else/finally structure

def safe_get_item(items, index):
    """Safely access a list element with full error handling."""
    result = None
    
    try:
        # Step 1: Attempt to access the list at the given index
        result = items[index]
    except IndexError:
        # Step 2: Handle out-of-range index
        print(f"Error: Index {index} is out of range (list has {len(items)} items)")
    except TypeError:
        # Step 3: Handle wrong type for index
        print(f"Error: Index must be an integer, got {type(index).__name__}")
    else:
        # Step 4: Runs only if no exception occurred
        print(f"Success! Found value: {result}")
    finally:
        # Step 5: Always runs, good for logging
        print(f"Attempt to access index {index} completed.")
    
    return result

# Test cases
my_list = [10, 20, 30, 40, 50]
print(safe_get_item(my_list, 2))   # Valid index
print(safe_get_item(my_list, 10))  # Out of range
print(safe_get_item(my_list, "a")) # Wrong type