def safe_get_item(items, index):
    result = None
    
    # TODO: Add try block
    # TODO: Try to access items[index]
    
    # TODO: Add except IndexError block
    # Print error message and return None
    
    # TODO: Add except TypeError block
    # Print error message and return None
    
    # TODO: Add else block
    # Print success message
    
    # TODO: Add finally block
    # Print log message about the attempt
    
    return result

# Test cases
my_list = [10, 20, 30, 40, 50]
print(safe_get_item(my_list, 2))   # Valid index
print(safe_get_item(my_list, 10))  # Out of range
print(safe_get_item(my_list, "a")) # Wrong type