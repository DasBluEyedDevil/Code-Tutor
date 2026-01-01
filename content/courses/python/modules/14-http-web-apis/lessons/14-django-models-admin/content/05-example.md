---
type: "EXAMPLE"
title: "Querying Data"
---

**Django ORM Queries**

Django provides an intuitive query API that translates to SQL.

**Basic Queries:**
```python
Model.objects.all()           # SELECT * FROM model
Model.objects.get(id=1)       # Single object (raises if not found)
Model.objects.first()         # First object or None
Model.objects.count()         # COUNT(*)
```

**Filtering:**
```python
.filter(field=value)          # WHERE field = value
.exclude(field=value)         # WHERE field != value
.filter(field__gte=100)       # >= 100 (greater than or equal)
.filter(field__lt=50)         # < 50 (less than)
.filter(field__contains='x')  # LIKE '%x%'
.filter(field__isnull=True)   # IS NULL
```

**Ordering:**
```python
.order_by('field')            # ASC
.order_by('-field')           # DESC
.order_by('field1', '-field2')# Multiple columns
```

**Related Objects:**
```python
.filter(category__name='Food')     # Join and filter
.select_related('category')        # Eager load (1 query)
.prefetch_related('transactions')  # Batch load (2 queries)
```

**Aggregation:**
```python
from django.db.models import Sum, Avg, Count
.aggregate(total=Sum('amount'))
.values('category').annotate(count=Count('id'))
```

```python
# Django ORM Query Examples

print("=== Django ORM Queries ===")

print("\n1. Basic Queries:")
queries = '''
# Get all transactions
Transaction.objects.all()

# Filter by type
Transaction.objects.filter(transaction_type='expense')

# Filter by amount (greater than or equal)
Transaction.objects.filter(amount__gte=100)

# Filter by user and order by date (descending)
Transaction.objects.filter(user=request.user).order_by('-created_at')
'''
print(queries)

print("\n2. Field Lookups (double underscore syntax):")
lookups = '''
__exact     ->  = value (default)
__iexact    ->  = value (case-insensitive)
__contains  ->  LIKE '%value%'
__icontains ->  LIKE '%value%' (case-insensitive)
__gt        ->  > value
__gte       ->  >= value
__lt        ->  < value
__lte       ->  <= value
__in        ->  IN (list of values)
__isnull    ->  IS NULL / IS NOT NULL
__startswith->  LIKE 'value%'
__endswith  ->  LIKE '%value'
'''
print(lookups)

print("\n3. Chaining Queries:")
chaining = '''
# Multiple conditions (AND)
Transaction.objects.filter(
    transaction_type='expense',
    amount__gte=50
).order_by('-created_at')[:10]

# OR conditions
from django.db.models import Q
Transaction.objects.filter(
    Q(transaction_type='income') | Q(amount__gte=1000)
)
'''
print(chaining)

print("\n4. Aggregation:")
aggregation = '''
from django.db.models import Sum, Avg, Count

# Total expenses
Transaction.objects.filter(
    transaction_type='expense'
).aggregate(total=Sum('amount'))
# Returns: {'total': Decimal('1234.56')}

# Count by category
Transaction.objects.values('category__name').annotate(
    count=Count('id'),
    total=Sum('amount')
)
# Returns: [{'category__name': 'Food', 'count': 5, 'total': 250}, ...]
'''
print(aggregation)
```
