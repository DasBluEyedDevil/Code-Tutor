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
    required: bool = True
    label: Optional[str] = None
    initial: Any = None
    help_text: str = ""
    validators: List[Callable] = field(default_factory=list)
    
    def to_python(self, value: Any) -> Any:
        return value
    
    def validate(self, value: Any) -> None:
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
            if self.min_length and len(value) < self.min_length:
                raise ValidationError(f"Must be at least {self.min_length} characters.")
            if self.max_length and len(value) > self.max_length:
                raise ValidationError(f"Must be at most {self.max_length} characters.")

@dataclass 
class DecimalField(Field):
    min_value: Optional[Decimal] = None
    max_value: Optional[Decimal] = None
    decimal_places: int = 2
    
    def to_python(self, value: Any) -> Optional[Decimal]:
        if value is None or value == '':
            return None
        try:
            dec_value = Decimal(str(value))
            return round(dec_value, self.decimal_places)
        except (InvalidOperation, ValueError):
            raise ValidationError("Enter a valid decimal number.")
    
    def validate(self, value: Any) -> None:
        super().validate(value)
        if value is not None:
            if self.min_value is not None and value < self.min_value:
                raise ValidationError(f"Must be at least {self.min_value}.")
            if self.max_value is not None and value > self.max_value:
                raise ValidationError(f"Must be at most {self.max_value}.")

@dataclass
class DateField(Field):
    input_formats: List[str] = field(default_factory=lambda: ['%Y-%m-%d', '%m/%d/%Y'])
    
    def to_python(self, value: Any) -> Optional[date]:
        if value is None or value == '':
            return None
        if isinstance(value, date):
            return value
        for fmt in self.input_formats:
            try:
                return datetime.strptime(value, fmt).date()
            except ValueError:
                continue
        raise ValidationError(f"Invalid date format. Use: {', '.join(self.input_formats)}")

@dataclass
class ChoiceField(Field):
    choices: List[tuple] = field(default_factory=list)
    
    def validate(self, value: Any) -> None:
        super().validate(value)
        if value is not None and value != '':
            valid_values = [c[0] for c in self.choices]
            if value not in valid_values:
                raise ValidationError(f"Invalid choice. Options: {valid_values}")


class Form:
    def __init__(self, data: Dict[str, Any] = None):
        self.data = data or {}
        self.cleaned_data: Dict[str, Any] = {}
        self.errors: Dict[str, List[str]] = {}
        self._is_bound = data is not None
        self._fields = self._get_fields()
    
    def _get_fields(self) -> Dict[str, Field]:
        fields = {}
        for name in dir(self.__class__):
            value = getattr(self.__class__, name)
            if isinstance(value, Field):
                fields[name] = value
        return fields
    
    def is_valid(self) -> bool:
        if not self._is_bound:
            return False
        
        self.errors = {}
        self.cleaned_data = {}
        
        for name, field_obj in self._fields.items():
            raw_value = self.data.get(name, field_obj.initial)
            
            try:
                # Convert to Python type
                value = field_obj.to_python(raw_value)
                
                # Run field validation
                field_obj.validate(value)
                
                # Run clean_<fieldname> method if exists
                clean_method = getattr(self, f'clean_{name}', None)
                if clean_method:
                    value = clean_method(value)
                
                self.cleaned_data[name] = value
            except ValidationError as e:
                self.errors.setdefault(name, []).append(e.message)
        
        if not self.errors:
            try:
                self.clean()
            except ValidationError as e:
                self.errors.setdefault('__all__', []).append(e.message)
        
        return len(self.errors) == 0
    
    def clean(self) -> None:
        pass
    
    def render(self) -> str:
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


class TransactionForm(Form):
    description = CharField(min_length=3, max_length=200, label="Description")
    amount = DecimalField(min_value=Decimal('0.01'), decimal_places=2)
    category = ChoiceField(choices=[
        ('food', 'Food'), ('transport', 'Transport'), ('other', 'Other')
    ])
    date = DateField()
    
    def clean_description(self, value: str) -> str:
        if value and 'spam' in value.lower():
            raise ValidationError("Description cannot contain 'spam'.")
        return value
    
    def clean(self) -> None:
        if self.cleaned_data.get('amount', 0) > 1000 and \
           self.cleaned_data.get('category') == 'food':
            raise ValidationError(
                "Food transactions over $1000 require manager approval."
            )


print("=== Form Validation Tests ===")

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