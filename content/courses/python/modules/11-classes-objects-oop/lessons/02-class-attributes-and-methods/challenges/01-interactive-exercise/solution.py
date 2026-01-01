# Employee Class with Class/Static Methods
# This solution demonstrates different types of methods

class Employee:
    """Employee class with class and static methods."""
    
    # Class attributes - shared by all instances
    company_name = "TechCorp"
    employee_count = 0
    
    def __init__(self, name, position, salary):
        """Initialize employee with name, position, and salary."""
        self.name = name
        self.position = position
        self.salary = salary
        # Increment class-level counter
        Employee.employee_count += 1
    
    def give_raise(self, amount):
        """Instance method: Give employee a raise."""
        if amount > 0:
            self.salary += amount
            print(f"{self.name} received a ${amount:,.2f} raise!")
            print(f"New salary: ${self.salary:,.2f}")
    
    @classmethod
    def from_dict(cls, emp_dict):
        """Class method: Create employee from dictionary."""
        return cls(
            name=emp_dict['name'],
            position=emp_dict['position'],
            salary=emp_dict['salary']
        )
    
    @staticmethod
    def is_valid_salary(amount):
        """Static method: Check if salary is in valid range."""
        return 20000 <= amount <= 500000
    
    def __str__(self):
        return f"{self.name} - {self.position} (${self.salary:,.2f})"

# Test the Employee class
print(f"=== {Employee.company_name} Employee System ===")

# Create employees
emp1 = Employee("Alice", "Developer", 75000)
emp2 = Employee("Bob", "Manager", 90000)

# Create from dictionary (class method)
emp_data = {'name': 'Carol', 'position': 'Designer', 'salary': 65000}
emp3 = Employee.from_dict(emp_data)

# Print all employees
print(f"\nTotal employees: {Employee.employee_count}")
for emp in [emp1, emp2, emp3]:
    print(f"  - {emp}")

# Test give_raise
print("\n--- Giving raises ---")
emp1.give_raise(5000)

# Test static method
print("\n--- Salary validation ---")
test_salaries = [15000, 50000, 600000]
for sal in test_salaries:
    valid = Employee.is_valid_salary(sal)
    print(f"${sal:,}: {'Valid' if valid else 'Invalid'}")