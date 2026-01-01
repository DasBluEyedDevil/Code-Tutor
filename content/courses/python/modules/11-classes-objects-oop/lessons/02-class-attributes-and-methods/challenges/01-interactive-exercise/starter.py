class Employee:
    # TODO: Add class attributes
    
    def __init__(self, name, position, salary):
        # TODO: Set instance attributes
        # TODO: Increment employee_count
        pass
    
    def give_raise(self, amount):
        # TODO: Increase salary
        pass
    
    @classmethod
    def from_dict(cls, emp_dict):
        # TODO: Create employee from dictionary
        # Example: {'name': 'John', 'position': 'Dev', 'salary': 75000}
        pass
    
    @staticmethod
    def is_valid_salary(amount):
        # TODO: Return True if salary between 20k and 500k
        pass

# TODO: Create employees and test methods