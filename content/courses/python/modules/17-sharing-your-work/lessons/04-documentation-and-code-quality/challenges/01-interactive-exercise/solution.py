from typing import List, Optional


def double_even_numbers(numbers: List[int]) -> List[int]:
    """Double all even numbers in a list.
    
    Takes a list of integers and returns a new list containing
    only the even numbers, each multiplied by 2.
    
    Args:
        numbers: A list of integers to process
    
    Returns:
        A new list with even numbers doubled
    
    Example:
        >>> double_even_numbers([1, 2, 3, 4, 5, 6])
        [4, 8, 12]
        >>> double_even_numbers([1, 3, 5])
        []
    """
    doubled_evens = []
    
    for number in numbers:
        # Check if number is even (divisible by 2)
        if number % 2 == 0:
            doubled_evens.append(number * 2)
    
    return doubled_evens


class ConditionalValue:
    """A container that holds a value with an active/inactive state.
    
    This class stores a value that can only be retrieved when the
    instance is in an active state. If inactive, retrieval returns None.
    
    Attributes:
        name: The stored value/name
        is_active: Whether the value can be retrieved
    
    Example:
        >>> item = ConditionalValue('Alice', True)
        >>> item.get_value()
        'Alice'
        >>> inactive_item = ConditionalValue('Bob', False)
        >>> inactive_item.get_value() is None
        True
    """
    
    def __init__(self, name: str, is_active: bool) -> None:
        """Initialize a ConditionalValue instance.
        
        Args:
            name: The value to store
            is_active: Whether the value should be accessible
        """
        self.name = name
        self.is_active = is_active
    
    def get_value(self) -> Optional[str]:
        """Retrieve the stored value if active.
        
        Returns:
            The stored name if active, None otherwise
        """
        if self.is_active:
            return self.name
        return None
    
    def activate(self) -> None:
        """Set the instance to active state."""
        self.is_active = True
    
    def deactivate(self) -> None:
        """Set the instance to inactive state."""
        self.is_active = False
    
    def __repr__(self) -> str:
        """Return a string representation of the instance."""
        status = 'active' if self.is_active else 'inactive'
        return f"ConditionalValue(name='{self.name}', status={status})"


# Demonstration of the improved code
if __name__ == '__main__':
    print('Improved Code Demonstration')
    print('=' * 40)
    
    # Test double_even_numbers
    print('\n1. double_even_numbers function:')
    test_numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
    result = double_even_numbers(test_numbers)
    print(f'   Input:  {test_numbers}')
    print(f'   Output: {result}')
    
    # Test ConditionalValue
    print('\n2. ConditionalValue class:')
    
    active_item = ConditionalValue('Alice', is_active=True)
    print(f'   {active_item}')
    print(f'   get_value(): {active_item.get_value()}')
    
    inactive_item = ConditionalValue('Bob', is_active=False)
    print(f'   {inactive_item}')
    print(f'   get_value(): {inactive_item.get_value()}')
    
    # Demonstrate state change
    print('\n3. Changing state:')
    inactive_item.activate()
    print(f'   After activate(): {inactive_item.get_value()}')