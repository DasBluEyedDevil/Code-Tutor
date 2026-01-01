---
type: "EXAMPLE"
title: "Code Example: Writing Good Documentation"
---

**Documentation best practices:**

**Docstring format (Google style):**
```python
def function(arg1, arg2):
    """Short description.
    
    Longer description if needed.
    
    Args:
        arg1: Description of arg1
        arg2: Description of arg2
    
    Returns:
        Description of return value
    
    Raises:
        ValueError: When this error occurs
    """
```

**Type hints:**
```python
from typing import List, Optional

def greet(name: str, times: int = 1) -> str:
    return ("Hello " + name + "! ") * times
```

**Comments:**
- Explain WHY, not WHAT
- Complex logic needs explanation
- TODO/FIXME for future work

**README sections:**
1. What it does
2. Installation
3. Quick start
4. Examples
5. Configuration
6. Contributing
7. License

```python
print("=== Example 1: Poor vs Good Documentation ===")

# BAD: No documentation
def calc(x, y, z):
    return (x + y) * z

# GOOD: Clear documentation
def calculate_total_with_tax(subtotal, tax_rate, quantity):
    """Calculate final price including tax for multiple items.
    
    Args:
        subtotal (float): Price of single item before tax
        tax_rate (float): Tax rate as decimal (e.g., 0.08 for 8%)
        quantity (int): Number of items to purchase
    
    Returns:
        float: Total price including tax
    
    Example:
        >>> calculate_total_with_tax(10.00, 0.08, 3)
        32.4
    """
    return (subtotal + subtotal * tax_rate) * quantity

print("\nBAD function:")
print(f"calc(10, 0.08, 3) = {calc(10, 0.08, 3)}")
print("What does this do? 🤔")

print("\nGOOD function:")
print(f"calculate_total_with_tax(10.00, 0.08, 3) = {calculate_total_with_tax(10.00, 0.08, 3)}")
print("Clear what this calculates! ✓")

print("\n=== Example 2: Comprehensive Docstrings ===")

from typing import List, Optional

class User:
    """Represents a user in the system.
    
    Attributes:
        username (str): Unique username for login
        email (str): User's email address
        is_active (bool): Whether account is active
    """
    
    def __init__(self, username: str, email: str):
        """Initialize a new user.
        
        Args:
            username: Unique identifier for the user
            email: Contact email address
        
        Raises:
            ValueError: If username or email is empty
        """
        if not username or not email:
            raise ValueError("Username and email are required")
        
        self.username = username
        self.email = email
        self.is_active = True
    
    def deactivate(self) -> None:
        """Deactivate the user account.
        
        This prevents the user from logging in but preserves
        their data for potential reactivation.
        """
        self.is_active = False
    
    def send_notification(self, message: str) -> bool:
        """Send notification email to user.
        
        Args:
            message: The notification message to send
        
        Returns:
            True if notification sent successfully, False otherwise
        
        Note:
            This simulates sending an email. In production,
            integrate with actual email service.
        """
        print(f"[Email to {self.email}] {message}")
        return True

print("User class with comprehensive docstrings")
user = User("alice", "alice@example.com")
print(f"Created user: {user.username}")
user.send_notification("Welcome to the platform!")

print("\n=== Example 3: README.md Template ===")

readme_template = '''
# Project Name

One-line description of what this project does.

## Features

- Feature 1: Brief description
- Feature 2: Brief description
- Feature 3: Brief description

## Installation

```bash
# Clone the repository
git clone https://github.com/username/project.git
cd project

# Create virtual environment
python -m venv venv
source venv/bin/activate  # Windows: venv\\Scripts\\activate

# Install dependencies
pip install -r requirements.txt
```

## Quick Start

```python
from project import MainClass

# Basic usage example
obj = MainClass()
result = obj.do_something()
print(result)
```

## Usage

### Example 1: Basic Usage

```python
# Code example
```

### Example 2: Advanced Usage

```python
# More complex example
```

## Configuration

Create a `.env` file:

```
DATABASE_URL=postgresql://user:pass@localhost/db
SECRET_KEY=your-secret-key
DEBUG=False
```

## Running Tests

```bash
pytest tests/
```

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

MIT License - see LICENSE file for details

## Contact

Your Name - your.email@example.com

Project Link: https://github.com/username/project
'''

print(readme_template)

print("\n=== Example 4: Code Quality Tools ===")

print("\n1. Black (Auto-formatter)")
print("""
# Install
pip install black

# Format all Python files
black .

# Check without modifying
black --check .

# Configuration: pyproject.toml
[tool.black]
line-length = 88
target-version = ['py38']
""")

print("\n2. flake8 (Style checker)")
print("""
# Install
pip install flake8

# Check all files
flake8 .

# Configuration: .flake8
[flake8]
max-line-length = 88
exclude = .git,__pycache__,venv
ignore = E203, W503
""")

print("\n3. mypy (Type checker)")
print("""
# Install
pip install mypy

# Check types
mypy src/

# Configuration: mypy.ini
[mypy]
python_version = 3.8
warn_return_any = True
warn_unused_configs = True
""")

print("\n=== Example 5: Type Hints ===")

def process_users(users: List[dict], active_only: bool = True) -> List[str]:
    """Extract usernames from user dictionaries.
    
    Args:
        users: List of user dictionaries with 'username' and 'is_active' keys
        active_only: If True, only return active users
    
    Returns:
        List of usernames
    """
    result = []
    for user in users:
        if not active_only or user.get('is_active', True):
            result.append(user['username'])
    return result

# Better with type hints
from typing import TypedDict

class UserDict(TypedDict):
    username: str
    email: str
    is_active: bool

def process_users_typed(users: List[UserDict], active_only: bool = True) -> List[str]:
    """Extract usernames from user dictionaries (type-safe)."""
    result = []
    for user in users:
        if not active_only or user['is_active']:
            result.append(user['username'])
    return result

print("\nType hints make code more maintainable:")
test_users: List[UserDict] = [
    {'username': 'alice', 'email': 'alice@example.com', 'is_active': True},
    {'username': 'bob', 'email': 'bob@example.com', 'is_active': False},
]

active_users = process_users_typed(test_users, active_only=True)
print(f"Active users: {active_users}")
```
