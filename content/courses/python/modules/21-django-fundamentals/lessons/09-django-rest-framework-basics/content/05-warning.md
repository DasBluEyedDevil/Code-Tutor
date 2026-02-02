---
type: "WARNING"
title: "Common DRF Pitfalls"
---

**1. Exposing Sensitive Data:**
```python
# BAD: Exposes password hash!
class UserSerializer(serializers.ModelSerializer):
    class Meta:
        model = User
        fields = '__all__'  # Never use __all__!

# GOOD: Explicit field list
class UserSerializer(serializers.ModelSerializer):
    class Meta:
        model = User
        fields = ['id', 'username', 'email']
        extra_kwargs = {'password': {'write_only': True}}
```

**2. N+1 Query Problem:**
```python
# BAD: Each transaction triggers a query for category
class TransactionSerializer(serializers.ModelSerializer):
    category_name = serializers.CharField(source='category.name')

# GOOD: Use select_related in ViewSet
class TransactionViewSet(viewsets.ModelViewSet):
    queryset = Transaction.objects.select_related('category')
```

**3. Missing Pagination:**
```python
# BAD: Returns all 10,000 records at once
class TransactionViewSet(viewsets.ModelViewSet):
    queryset = Transaction.objects.all()

# GOOD: Enable pagination
REST_FRAMEWORK = {
    'DEFAULT_PAGINATION_CLASS': 'rest_framework.pagination.LimitOffsetPagination',
    'PAGE_SIZE': 50,
}
```

**4. Ignoring Object-Level Permissions:**
```python
# BAD: Any user can delete any transaction
class TransactionViewSet(viewsets.ModelViewSet):
    permission_classes = [IsAuthenticated]

# GOOD: Check ownership
class IsOwner(permissions.BasePermission):
    def has_object_permission(self, request, view, obj):
        return obj.user == request.user

class TransactionViewSet(viewsets.ModelViewSet):
    permission_classes = [IsAuthenticated, IsOwner]
```