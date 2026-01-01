from contextlib import contextmanager

# List Modifier Context Manager
# This solution demonstrates transactional list operations

@contextmanager
def list_modifier(lst):
    """Context manager for transactional list modifications.
    
    Backs up the list and restores it if an exception occurs.
    Keeps changes if no exception.
    """
    # Step 1: Create backup of the list
    backup = lst.copy()
    
    try:
        # Step 2: Yield control to the context block
        yield lst
        # Step 3: If we get here, no exception - keep changes
        print("  (Changes committed)")
    except Exception as e:
        # Step 4: Exception occurred - restore backup
        lst[:] = backup  # Modify list in-place
        print(f"  (Changes rolled back due to: {e})")
        raise  # Re-raise the exception

# Test the context manager
print("=== List Modifier Demo ===")

my_list = [1, 2, 3]
print(f"\nOriginal: {my_list}")

# Test 1: Successful modification
print("\nTest 1: Successful modification")
with list_modifier(my_list):
    my_list.append(4)
    my_list.append(5)

print(f"After success: {my_list}")

# Test 2: Failed modification (should restore)
print("\nTest 2: Failed modification")
try:
    with list_modifier(my_list):
        my_list.append(6)
        my_list.append(7)
        print(f"  During modification: {my_list}")
        raise ValueError("Simulated error!")
except ValueError:
    pass

print(f"After failure: {my_list}")
print("\nNotice: list was restored to state before the failed modification!")