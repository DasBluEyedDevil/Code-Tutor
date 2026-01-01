# Python 3.10+ - No imports needed for basic type hints!

# TODO: Add type hints to all functions

def calculate_bmi(weight, height):
    """Calculate Body Mass Index."""
    return weight / (height ** 2)

def get_initials(first_name, last_name):
    """Get initials from first and last name."""
    return f"{first_name[0]}.{last_name[0]}."

def find_longest(words):
    """Find the longest word in a list."""
    longest = ""
    for word in words:
        if len(word) > len(longest):
            longest = word
    return longest

def merge_dicts(dict1, dict2):
    """Merge two dictionaries, adding values for common keys."""
    result = dict1.copy()
    for key, value in dict2.items():
        if key in result:
            result[key] += value
        else:
            result[key] = value
    return result

def safe_divide(a, b):
    """Divide a by b, return None if b is zero."""
    if b == 0:
        return None
    return a / b

# Test your type-hinted functions
print(f"BMI: {calculate_bmi(70.0, 1.75):.1f}")
print(f"Initials: {get_initials('John', 'Doe')}")
print(f"Longest: {find_longest(['cat', 'elephant', 'dog'])}")
print(f"Merged: {merge_dicts({'a': 1, 'b': 2}, {'b': 3, 'c': 4})}")
print(f"Safe divide: {safe_divide(10, 3):.2f}")
print(f"Safe divide by zero: {safe_divide(10, 0)}")