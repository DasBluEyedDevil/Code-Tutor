from dataclasses import dataclass, field
from typing import Dict, Any, List, Optional, Callable
from datetime import date, datetime
from decimal import Decimal, InvalidOperation
import re

class ValidationError(Exception):
    def __init__(self, message: str):
        self.message = message
        super().__init__(message)

@dataclass
class Field:
    """Base field class."""
    required: bool = True
    label: Optional[str] = None
    initial: Any = None
    help_text: str = ""
    validators: List[Callable] = field(default_factory=list)
    
    def to_python(self, value: Any) -> Any:
        """Convert input to Python type."""
        return value
    
    def validate(self, value: Any) -> None:
        """Run validation, raise ValidationError if invalid."""
        if self.required and (value is None or value == ''):
            raise ValidationError("This field is required.")
        for validator in self.validators:
            validator(value)

@dataclass
class CharField(Field):
    min_length: Optional[int] = None
    max_length: Optional[int] = None
    
    def to_python(self, value: Any) -> str:
        if value is None:
            return ''
        return str(value).strip()
    
    def validate(self, value: Any) -> None:
        super().validate(value)
        if value:
            # TODO: Check min_length and max_length
            pass

@dataclass 
class DecimalField(Field):
    min_value: Optional[Decimal] = None
    max_value: Optional[Decimal] = None
    decimal_places: int = 2
    
    def to_python(self, value: Any) -> Optional[Decimal]:
        if value is None or value == '':
            return None
        # TODO: Convert to Decimal, raise ValidationError if invalid
        pass
    
    def validate(self, value: Any) -> None:
        super().validate(value)
        # TODO: Check min_value and max_value
        pass

@dataclass
class DateField(Field):
    input_formats: List[str] = field(default_factory=lambda: ['%Y-%m-%d'])
    
    def to_python(self, value: Any) -> Optional[date]:
        if value is None or value == '':
            return None
        if isinstance(value, date):
            return value
        # TODO: Try parsing with each format, raise if none work
        pass

@dataclass
class ChoiceField(Field):
    choices: List[tuple] = field(default_factory=list)
    
    def validate(self, value: Any) -> None:
        super().validate(value)
        # TODO: Check value is in choices
        pass


class Form:
    """Base form class - subclass and define fields as class attributes."""
    
    def __init__(self, data: Dict[str, Any] = None):
        self.data = data or {}
        self.cleaned_data: Dict[str, Any] = {}
        self.errors: Dict[str, List[str]] = {}
        self._is_bound = data is not None
        self._fields = self._get_fields()
    
    def _get_fields(self) -> Dict[str, Field]:
        """Collect field definitions from class."""
        fields = {}
        for name in dir(self.__class__):
            value = getattr(self.__class__, name)
            if isinstance(value, Field):
                fields[name] = value
        return fields
    
    def is_valid(self) -> bool:
        """Validate all fields and return True if valid."""
        if not self._is_bound:
            return False
        
        self.errors = {}
        self.cleaned_data = {}
        
        for name, field_obj in self._fields.items():
            raw_value = self.data.get(name, field_obj.initial)
            
            try:
                # TODO: Convert to Python type
                # TODO: Run field validation
                # TODO: Run clean_<fieldname> method if exists
                # TODO: Store in cleaned_data
                pass
            except ValidationError as e:
                self.errors.setdefault(name, []).append(e.message)
        
        # Run form-level validation
        if not self.errors:
            try:
                self.clean()
            except ValidationError as e:
                self.errors.setdefault('__all__', []).append(e.message)
        
        return len(self.errors) == 0
    
    def clean(self) -> None:
        """Override for cross-field validation."""
        pass
    
    def render(self) -> str:
        """Render form as HTML."""
        html_parts = []
        for name, field_obj in self._fields.items():
            label = field_obj.label or name.replace('_', ' ').title()
            value = self.data.get(name, field_obj.initial or '')
            errors = self.errors.get(name, [])
            
            error_html = ''.join(f'<span class="error">{e}</span>' for e in errors)
            html_parts.append(
                f'<div class="field">'
                f'<label>{label}</label>'
                f'<input name="{name}" value="{value}">'
                f'{error_html}'
                f'</div>'
            )
        return '\n'.join(html_parts)


# Create a transaction form
class TransactionForm(Form):
    description = CharField(min_length=3, max_length=200, label="Description")
    amount = DecimalField(min_value=Decimal('0.01'), decimal_places=2)
    category = ChoiceField(choices=[
        ('food', 'Food'), ('transport', 'Transport'), ('other', 'Other')
    ])
    date = DateField()
    
    def clean_description(self, value: str) -> str:
        """Custom validation for description."""
        if value and 'spam' in value.lower():
            raise ValidationError("Description cannot contain 'spam'.")
        return value
    
    def clean(self) -> None:
        """Cross-field validation."""
        if self.cleaned_data.get('amount', 0) > 1000 and \
           self.cleaned_data.get('category') == 'food':
            raise ValidationError(
                "Food transactions over $1000 require manager approval."
            )


# Test the form
print("=== Form Validation Tests ===")

# Test 1: Valid data
form = TransactionForm({
    'description': 'Lunch at restaurant',
    'amount': '45.50',
    'category': 'food',
    'date': '2025-01-15'
})
print(f"\nTest 1 - Valid data:")
if form.is_valid():
    print(f"  Valid! {form.cleaned_data}")
else:
    print(f"  Errors: {form.errors}")

# Test 2: Missing required field
form = TransactionForm({
    'description': '',
    'amount': '45.50',
    'category': 'food',
    'date': '2025-01-15'
})
print(f"\nTest 2 - Missing description:")
if form.is_valid():
    print(f"  Valid! {form.cleaned_data}")
else:
    print(f"  Errors: {form.errors}")

# Test 3: Invalid amount (negative)
form = TransactionForm({
    'description': 'Test transaction',
    'amount': '-50.00',
    'category': 'food',
    'date': '2025-01-15'
})
print(f"\nTest 3 - Negative amount:")
if form.is_valid():
    print(f"  Valid! {form.cleaned_data}")
else:
    print(f"  Errors: {form.errors}")