---
type: "EXAMPLE"
title: "Serializers - Converting Data"
---

Serializers are the bridge between Python objects and JSON:

**Expected Output:**
```
Serializer output: {'id': 1, 'amount': '150.00', 'description': 'Groceries'}
Deserialization valid: True
Validation error caught: amount must be positive
```

```python
from dataclasses import dataclass, field, asdict
from typing import Dict, Any, List, Optional
from datetime import date, datetime
from decimal import Decimal
import json

class ValidationError(Exception):
    def __init__(self, errors: Dict[str, List[str]]):
        self.errors = errors
        super().__init__(str(errors))


class Field:
    """Base serializer field."""
    def __init__(self, required: bool = True, read_only: bool = False,
                 write_only: bool = False, default: Any = None):
        self.required = required
        self.read_only = read_only
        self.write_only = write_only
        self.default = default
    
    def to_representation(self, value: Any) -> Any:
        """Convert Python value to JSON-serializable format."""
        return value
    
    def to_internal_value(self, data: Any) -> Any:
        """Convert JSON data to Python value."""
        return data
    
    def validate(self, value: Any) -> Any:
        """Run validation on the value."""
        return value


class CharField(Field):
    def __init__(self, max_length: int = None, min_length: int = None, **kwargs):
        super().__init__(**kwargs)
        self.max_length = max_length
        self.min_length = min_length
    
    def to_internal_value(self, data: Any) -> str:
        if data is None:
            return None
        return str(data)
    
    def validate(self, value: str) -> str:
        if value and self.max_length and len(value) > self.max_length:
            raise ValueError(f"Max length is {self.max_length}")
        if value and self.min_length and len(value) < self.min_length:
            raise ValueError(f"Min length is {self.min_length}")
        return value


class DecimalField(Field):
    def __init__(self, max_digits: int = 10, decimal_places: int = 2, **kwargs):
        super().__init__(**kwargs)
        self.max_digits = max_digits
        self.decimal_places = decimal_places
    
    def to_representation(self, value: Decimal) -> str:
        if value is None:
            return None
        return str(value)
    
    def to_internal_value(self, data: Any) -> Decimal:
        if data is None:
            return None
        try:
            return Decimal(str(data))
        except:
            raise ValueError("Invalid decimal value")


class IntegerField(Field):
    def to_internal_value(self, data: Any) -> int:
        if data is None:
            return None
        try:
            return int(data)
        except:
            raise ValueError("Invalid integer")


class DateField(Field):
    def to_representation(self, value: date) -> str:
        if value is None:
            return None
        return value.isoformat()
    
    def to_internal_value(self, data: Any) -> date:
        if data is None:
            return None
        if isinstance(data, date):
            return data
        try:
            return datetime.strptime(data, '%Y-%m-%d').date()
        except:
            raise ValueError("Invalid date format (use YYYY-MM-DD)")


class Serializer:
    """Base serializer class."""
    
    def __init__(self, instance=None, data=None, many: bool = False):
        self.instance = instance
        self.initial_data = data
        self.many = many
        self._validated_data = None
        self._errors = {}
    
    def get_fields(self) -> Dict[str, Field]:
        """Override to define fields."""
        return {}
    
    @property
    def data(self) -> Dict[str, Any]:
        """Serialize instance to dictionary."""
        if self.many:
            return [self._serialize_one(item) for item in self.instance]
        return self._serialize_one(self.instance)
    
    def _serialize_one(self, obj) -> Dict[str, Any]:
        result = {}
        for name, field_obj in self.get_fields().items():
            if field_obj.write_only:
                continue
            value = getattr(obj, name, None)
            result[name] = field_obj.to_representation(value)
        return result
    
    def is_valid(self, raise_exception: bool = False) -> bool:
        """Validate input data."""
        self._errors = {}
        self._validated_data = {}
        
        for name, field_obj in self.get_fields().items():
            if field_obj.read_only:
                continue
            
            value = self.initial_data.get(name, field_obj.default)
            
            # Check required
            if field_obj.required and value is None:
                self._errors.setdefault(name, []).append("This field is required")
                continue
            
            try:
                # Convert to internal value
                internal = field_obj.to_internal_value(value)
                # Run field validation
                validated = field_obj.validate(internal)
                # Run validate_<field> method
                validator = getattr(self, f'validate_{name}', None)
                if validator:
                    validated = validator(validated)
                self._validated_data[name] = validated
            except ValueError as e:
                self._errors.setdefault(name, []).append(str(e))
        
        # Run cross-field validation
        if not self._errors:
            try:
                self._validated_data = self.validate(self._validated_data)
            except ValueError as e:
                self._errors['non_field_errors'] = [str(e)]
        
        if self._errors and raise_exception:
            raise ValidationError(self._errors)
        
        return not self._errors
    
    def validate(self, attrs: Dict[str, Any]) -> Dict[str, Any]:
        """Cross-field validation."""
        return attrs
    
    @property
    def validated_data(self) -> Dict[str, Any]:
        if self._validated_data is None:
            raise ValueError("Call is_valid() first")
        return self._validated_data
    
    @property
    def errors(self) -> Dict[str, List[str]]:
        return self._errors


# Create a Transaction serializer
@dataclass
class Transaction:
    id: int
    amount: Decimal
    description: str
    category: str = "other"
    date: date = None


class TransactionSerializer(Serializer):
    def get_fields(self):
        return {
            'id': IntegerField(read_only=True),
            'amount': DecimalField(max_digits=10, decimal_places=2),
            'description': CharField(max_length=200, min_length=3),
            'category': CharField(required=False, default='other'),
            'date': DateField(required=False),
        }
    
    def validate_amount(self, value: Decimal) -> Decimal:
        if value <= 0:
            raise ValueError("amount must be positive")
        return value


# Demo
print("=== Serializer Demo ===")

# Serialization (object -> JSON)
print("\n1. Serialization (Python -> JSON):")
tx = Transaction(id=1, amount=Decimal('150.00'), description='Groceries', date=date.today())
serializer = TransactionSerializer(instance=tx)
print(f"   Serializer output: {serializer.data}")
print(f"   JSON: {json.dumps(serializer.data)}")

# Deserialization (JSON -> object)
print("\n2. Deserialization (JSON -> Python):")
data = {'amount': '75.50', 'description': 'Lunch', 'category': 'food'}
serializer = TransactionSerializer(data=data)
print(f"   Deserialization valid: {serializer.is_valid()}")
if serializer.is_valid():
    print(f"   Validated data: {serializer.validated_data}")

# Validation error
print("\n3. Validation Error:")
bad_data = {'amount': '-50', 'description': 'Test'}
serializer = TransactionSerializer(data=bad_data)
if not serializer.is_valid():
    print(f"   Validation error caught: {serializer.errors.get('amount', [''])[0]}")

# Serialize many objects
print("\n4. Serialize Multiple Objects:")
transactions = [
    Transaction(id=1, amount=Decimal('50'), description='Coffee'),
    Transaction(id=2, amount=Decimal('100'), description='Books'),
]
serializer = TransactionSerializer(instance=transactions, many=True)
print(f"   List output: {serializer.data}")
```
