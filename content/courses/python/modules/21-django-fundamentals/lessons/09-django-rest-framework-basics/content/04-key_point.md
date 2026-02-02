---
type: "KEY_POINT"
title: "DRF Best Practices"
---

**Serializer Best Practices:**

```python
# Use ModelSerializer for models
class TransactionSerializer(serializers.ModelSerializer):
    class Meta:
        model = Transaction
        fields = ['id', 'amount', 'description', 'category', 'created_at']
        read_only_fields = ['id', 'created_at']

# Nested serializers for related data
class TransactionDetailSerializer(TransactionSerializer):
    category = CategorySerializer(read_only=True)
```

**ViewSet Best Practices:**

```python
class TransactionViewSet(viewsets.ModelViewSet):
    queryset = Transaction.objects.all()
    serializer_class = TransactionSerializer
    permission_classes = [IsAuthenticated]
    filter_backends = [DjangoFilterBackend, OrderingFilter]
    filterset_fields = ['category', 'date']
    ordering_fields = ['amount', 'created_at']
    
    def get_queryset(self):
        # Filter to user's transactions only
        return self.queryset.filter(user=self.request.user)
    
    def perform_create(self, serializer):
        # Auto-set user on create
        serializer.save(user=self.request.user)
```

**URL Configuration:**

```python
# urls.py
from rest_framework.routers import DefaultRouter

router = DefaultRouter()
router.register('transactions', TransactionViewSet)
router.register('categories', CategoryViewSet)

urlpatterns = [
    path('api/v1/', include(router.urls)),
]
# Creates: /api/v1/transactions/, /api/v1/transactions/{id}/
```

**Authentication & Permissions:**

```python
# settings.py
REST_FRAMEWORK = {
    'DEFAULT_AUTHENTICATION_CLASSES': [
        'rest_framework.authentication.TokenAuthentication',
        'rest_framework.authentication.SessionAuthentication',
    ],
    'DEFAULT_PERMISSION_CLASSES': [
        'rest_framework.permissions.IsAuthenticated',
    ],
    'DEFAULT_PAGINATION_CLASS': 'rest_framework.pagination.PageNumberPagination',
    'PAGE_SIZE': 20,
}
```