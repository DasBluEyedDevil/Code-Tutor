from pathlib import Path

# Creating a Python Package
# This solution demonstrates package structure

# Step 1: Create the utils directory
utils_dir = Path('utils')
utils_dir.mkdir(exist_ok=True)

# Step 2: Create __init__.py (makes it a package)
init_content = '''"""Utils package for text and number operations."""

from .text import capitalize, reverse
from .numbers import is_even, is_prime

__all__ = ['capitalize', 'reverse', 'is_even', 'is_prime']
'''
(utils_dir / '__init__.py').write_text(init_content)

# Step 3: Create text.py module
text_content = '''"""Text utility functions."""

def capitalize(text):
    """Capitalize first letter of each word."""
    return text.title()

def reverse(text):
    """Reverse a string."""
    return text[::-1]
'''
(utils_dir / 'text.py').write_text(text_content)

# Step 4: Create numbers.py module
numbers_content = '''"""Number utility functions."""

def is_even(n):
    """Check if number is even."""
    return n % 2 == 0

def is_prime(n):
    """Check if number is prime."""
    if n < 2:
        return False
    for i in range(2, int(n ** 0.5) + 1):
        if n % i == 0:
            return False
    return True
'''
(utils_dir / 'numbers.py').write_text(numbers_content)

print("Created utils package with:")
print("  - utils/__init__.py")
print("  - utils/text.py")
print("  - utils/numbers.py")

# Step 5: Test the package
print("\nTesting the package:")

# Import from our new package
import utils

print(f"capitalize('hello world') = '{utils.capitalize('hello world')}'")
print(f"reverse('Python') = '{utils.reverse('Python')}'")
print(f"is_even(4) = {utils.is_even(4)}")
print(f"is_prime(17) = {utils.is_prime(17)}")