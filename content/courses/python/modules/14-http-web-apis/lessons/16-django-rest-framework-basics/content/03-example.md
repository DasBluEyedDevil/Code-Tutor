---
type: "EXAMPLE"
title: "Serializers"
---

**Serializers - Data Transformation Layer**

Serializers convert complex data (Django models) to Python primitives that can be rendered to JSON.

**ModelSerializer - Quick Setup:**
```python
class CategorySerializer(serializers.ModelSerializer):
    class Meta:
        model = Category
        fields = ['id', 'name', 'description']
```

**Key Concepts:**

**1. fields - Which Fields to Include**
- Use `fields = '__all__'` for all fields
- Use `fields = ['id', 'name']` for specific fields
- Use `exclude = ['password']` to exclude fields

**2. read_only_fields - Non-Editable Fields**
```python
read_only_fields = ['created_at', 'id']
```

**3. Custom Fields - Computed or Nested Data**
```python
category_name = serializers.CharField(
    source='category.name', 
    read_only=True
)
```

**4. Nested Serializers**
```python
category = CategorySerializer(read_only=True)
```

**Validation:**
Serializers validate data automatically based on model constraints. Add custom validation with:
```python
def validate_amount(self, value):
    if value <= 0:
        raise serializers.ValidationError("Must be positive")
    return value
```

```python
# tracker/serializers.py
from rest_framework import serializers
from .models import Transaction, Category

class CategorySerializer(serializers.ModelSerializer):
    """Serializer for Category model."""
    
    class Meta:
        model = Category
        fields = ['id', 'name', 'description']


class TransactionSerializer(serializers.ModelSerializer):
    """Serializer for Transaction model with nested category name."""
    
    # Read-only field from related model
    category_name = serializers.CharField(
        source='category.name', 
        read_only=True
    )
    
    class Meta:
        model = Transaction
        fields = [
            'id', 
            'amount', 
            'category',        # Foreign key (ID)
            'category_name',   # Computed field
            'transaction_type', 
            'description', 
            'created_at'
        ]
        read_only_fields = ['created_at']
    
    def validate_amount(self, value):
        """Ensure amount is positive."""
        if value <= 0:
            raise serializers.ValidationError(
                "Amount must be greater than zero."
            )
        return value


print("=== DRF Serializers ===")

print("\nCategorySerializer:")
print("  - Simple ModelSerializer")
print("  - Exposes: id, name, description")

print("\nTransactionSerializer:")
print("  - category_name: Computed from related model")
print("  - read_only_fields: created_at (auto-set)")
print("  - validate_amount: Custom validation")

print("\nSerialization Examples:")
print("  # Model to JSON")
print("  serializer = TransactionSerializer(transaction)")
print("  serializer.data  # {'id': 1, 'amount': '50.00', ...}")

print("\n  # JSON to Model")
print("  serializer = TransactionSerializer(data=request.data)")
print("  serializer.is_valid(raise_exception=True)")
print("  serializer.save()  # Creates/updates model")
```
