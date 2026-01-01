# string_utils.py module
# This solution demonstrates creating a custom module

def reverse(text):
    """Reverse a string.
    
    Args:
        text: String to reverse
        
    Returns:
        Reversed string
    """
    return text[::-1]

def count_vowels(text):
    """Count vowels in a string (case-insensitive).
    
    Args:
        text: String to analyze
        
    Returns:
        Number of vowels (a, e, i, o, u)
    """
    vowels = 'aeiou'
    return sum(1 for char in text.lower() if char in vowels)

def is_palindrome(text):
    """Bonus: Check if text is a palindrome."""
    cleaned = ''.join(c.lower() for c in text if c.isalnum())
    return cleaned == cleaned[::-1]

# Test code - only runs when file is executed directly
if __name__ == "__main__":
    print("=== Testing string_utils module ===")
    
    # Test reverse
    print(f"\nreverse('hello') = '{reverse('hello')}'")
    print(f"reverse('Python') = '{reverse('Python')}'")
    
    # Test count_vowels
    print(f"\ncount_vowels('hello') = {count_vowels('hello')}")
    print(f"count_vowels('AEIOU') = {count_vowels('AEIOU')}")
    print(f"count_vowels('Python') = {count_vowels('Python')}")
    
    # Test palindrome (bonus)
    print(f"\nis_palindrome('radar') = {is_palindrome('radar')}")
    print(f"is_palindrome('hello') = {is_palindrome('hello')}")