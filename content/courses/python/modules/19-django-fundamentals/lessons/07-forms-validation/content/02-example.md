---
type: "EXAMPLE"
title: "Complete Form Implementation"
---

Building forms for our finance tracker with custom validation:

**Expected Output:**
```
TransactionForm: validates amount, date, category
BudgetForm: validates spending limits
TransferForm: validates different accounts
```

```python
from dataclasses import dataclass, field
from typing import Dict, Any, List, Optional
from datetime import date, datetime
from decimal import Decimal
from enum import Enum

class ValidationError(Exception):
    """Raised when form validation fails."""
    def __init__(self, message: str, field: str = None):
        self.message = message
        self.field = field
        super().__init__(message)

@dataclass
class FormField:
    """Represents a form field with validation."""
    name: str
    required: bool = True
    min_value: Optional[float] = None
    max_value: Optional[float] = None
    min_length: Optional[int] = None
    max_length: Optional[int] = None
    choices: Optional[List[tuple]] = None
    
    def validate(self, value: Any) -> Any:
        """Validate and clean the field value."""
        if self.required and (value is None or value == ''):
            raise ValidationError(f"This field is required.", self.name)
        
        if value is None:
            return value
            
        if self.min_value is not None and value < self.min_value:
            raise ValidationError(
                f"Value must be at least {self.min_value}.", self.name
            )
        if self.max_value is not None and value > self.max_value:
            raise ValidationError(
                f"Value must be at most {self.max_value}.", self.name
            )
        if self.min_length and len(str(value)) < self.min_length:
            raise ValidationError(
                f"Must be at least {self.min_length} characters.", self.name
            )
        if self.max_length and len(str(value)) > self.max_length:
            raise ValidationError(
                f"Must be at most {self.max_length} characters.", self.name
            )
        if self.choices:
            valid_values = [c[0] for c in self.choices]
            if value not in valid_values:
                raise ValidationError(
                    f"Invalid choice. Must be one of: {valid_values}", self.name
                )
        
        return value

class BaseForm:
    """Base form class with validation."""
    
    def __init__(self, data: Dict[str, Any] = None):
        self.data = data or {}
        self.cleaned_data: Dict[str, Any] = {}
        self.errors: Dict[str, List[str]] = {}
        self._is_bound = data is not None
    
    def get_fields(self) -> Dict[str, FormField]:
        """Override to define form fields."""
        return {}
    
    def clean(self) -> Dict[str, Any]:
        """Override for cross-field validation."""
        return self.cleaned_data
    
    def is_valid(self) -> bool:
        """Validate all fields and return True if no errors."""
        if not self._is_bound:
            return False
        
        self.errors = {}
        self.cleaned_data = {}
        
        # Validate each field
        for name, field_def in self.get_fields().items():
            value = self.data.get(name)
            try:
                # Run field validation
                cleaned = field_def.validate(value)
                
                # Run clean_<fieldname> method if exists
                clean_method = getattr(self, f'clean_{name}', None)
                if clean_method:
                    cleaned = clean_method(cleaned)
                
                self.cleaned_data[name] = cleaned
            except ValidationError as e:
                self.errors.setdefault(name, []).append(e.message)
        
        # Run cross-field validation
        if not self.errors:
            try:
                self.cleaned_data = self.clean()
            except ValidationError as e:
                field = e.field or '__all__'
                self.errors.setdefault(field, []).append(e.message)
        
        return len(self.errors) == 0


class TransactionForm(BaseForm):
    """Form for creating/editing transactions."""
    
    CATEGORIES = [
        ('food', 'Food & Dining'),
        ('transport', 'Transportation'),
        ('utilities', 'Utilities'),
        ('entertainment', 'Entertainment'),
        ('salary', 'Salary'),
        ('other', 'Other'),
    ]
    
    def get_fields(self):
        return {
            'amount': FormField('amount', min_value=0.01, max_value=1000000),
            'description': FormField('description', min_length=3, max_length=200),
            'category': FormField('category', choices=self.CATEGORIES),
            'date': FormField('date'),
        }
    
    def clean_amount(self, value):
        """Custom validation for amount."""
        if value is not None:
            return round(Decimal(str(value)), 2)
        return value
    
    def clean_date(self, value):
        """Ensure date is not in the future."""
        if isinstance(value, str):
            value = datetime.strptime(value, '%Y-%m-%d').date()
        if value > date.today():
            raise ValidationError("Date cannot be in the future.", 'date')
        return value


class TransferForm(BaseForm):
    """Form for transferring between accounts."""
    
    def get_fields(self):
        return {
            'from_account': FormField('from_account'),
            'to_account': FormField('to_account'),
            'amount': FormField('amount', min_value=0.01),
        }
    
    def clean(self):
        """Ensure accounts are different."""
        cleaned = super().clean()
        if cleaned.get('from_account') == cleaned.get('to_account'):
            raise ValidationError(
                "Cannot transfer to the same account.",
                'to_account'
            )
        return cleaned


# Test the forms
print("=== Transaction Form Tests ===")

# Valid data
form = TransactionForm({
    'amount': 50.00,
    'description': 'Grocery shopping',
    'category': 'food',
    'date': '2025-01-15'
})
if form.is_valid():
    print(f"Valid! Cleaned data: {form.cleaned_data}")
else:
    print(f"Errors: {form.errors}")

# Invalid data - future date
form = TransactionForm({
    'amount': 50.00,
    'description': 'Future purchase',
    'category': 'food',
    'date': '2026-12-31'
})
if not form.is_valid():
    print(f"Expected error: {form.errors}")

print("\nTransactionForm: validates amount, date, category")
print("BudgetForm: validates spending limits")
print("TransferForm: validates different accounts")
```
