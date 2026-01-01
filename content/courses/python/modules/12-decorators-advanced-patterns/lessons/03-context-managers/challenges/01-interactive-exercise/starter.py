from contextlib import contextmanager

@contextmanager
def list_modifier(lst):
    # TODO: Backup the list
    # TODO: Yield to allow modifications
    # TODO: If exception, restore backup
    # TODO: If no exception, keep changes
    pass

# Test your context manager
my_list = [1, 2, 3]

print(f"Original: {my_list}")

# Successful modification
with list_modifier(my_list):
    my_list.append(4)
    my_list.append(5)

print(f"After success: {my_list}")

# Failed modification (should restore)
try:
    with list_modifier(my_list):
        my_list.append(6)
        raise ValueError("Oops!")
except ValueError:
    pass

print(f"After failure: {my_list}")