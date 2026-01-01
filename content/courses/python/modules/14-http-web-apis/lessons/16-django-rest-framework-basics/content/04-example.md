---
type: "EXAMPLE"
title: "ViewSets and Routers"
---

**ViewSets - All CRUD Operations in One Class**

`ModelViewSet` provides complete CRUD operations:
- `list()` - GET /transactions/
- `create()` - POST /transactions/
- `retrieve()` - GET /transactions/{id}/
- `update()` - PUT /transactions/{id}/
- `partial_update()` - PATCH /transactions/{id}/
- `destroy()` - DELETE /transactions/{id}/

**Key Methods to Override:**

**get_queryset()** - Filter what users can see:
```python
def get_queryset(self):
    return Transaction.objects.filter(user=self.request.user)
```

**perform_create()** - Add extra data on create:
```python
def perform_create(self, serializer):
    serializer.save(user=self.request.user)
```

**Routers - Auto-Generate URLs:**
```python
router = DefaultRouter()
router.register('transactions', TransactionViewSet)
urlpatterns = router.urls
```

This creates:
- `GET/POST /transactions/`
- `GET/PUT/PATCH/DELETE /transactions/{id}/`

**DefaultRouter Bonus:**
Adds an API root view at `/` listing all endpoints.

```python
# tracker/views.py
from rest_framework import viewsets, status
from rest_framework.decorators import action
from rest_framework.permissions import IsAuthenticated
from rest_framework.response import Response
from .models import Transaction, Category
from .serializers import TransactionSerializer, CategorySerializer


class CategoryViewSet(viewsets.ModelViewSet):
    """API endpoint for categories."""
    queryset = Category.objects.all()
    serializer_class = CategorySerializer
    permission_classes = [IsAuthenticated]


class TransactionViewSet(viewsets.ModelViewSet):
    """API endpoint for user transactions."""
    serializer_class = TransactionSerializer
    permission_classes = [IsAuthenticated]
    
    def get_queryset(self):
        """Return only current user's transactions."""
        return Transaction.objects.filter(
            user=self.request.user
        ).select_related('category')
    
    def perform_create(self, serializer):
        """Set user when creating transaction."""
        serializer.save(user=self.request.user)
    
    @action(detail=False, methods=['get'])
    def summary(self, request):
        """Custom action: GET /transactions/summary/"""
        from django.db.models import Sum
        
        qs = self.get_queryset()
        income = qs.filter(
            transaction_type='income'
        ).aggregate(total=Sum('amount'))['total'] or 0
        expenses = qs.filter(
            transaction_type='expense'
        ).aggregate(total=Sum('amount'))['total'] or 0
        
        return Response({
            'income': income,
            'expenses': expenses,
            'balance': income - expenses
        })


# tracker/urls.py
from rest_framework.routers import DefaultRouter
from .views import TransactionViewSet, CategoryViewSet

router = DefaultRouter()
router.register('transactions', TransactionViewSet, basename='transaction')
router.register('categories', CategoryViewSet, basename='category')

urlpatterns = router.urls


print("=== DRF ViewSets and Routers ===")

print("\nGenerated Endpoints:")
print("  GET    /transactions/         - List all")
print("  POST   /transactions/         - Create new")
print("  GET    /transactions/{id}/    - Get one")
print("  PUT    /transactions/{id}/    - Full update")
print("  PATCH  /transactions/{id}/    - Partial update")
print("  DELETE /transactions/{id}/    - Delete")
print("  GET    /transactions/summary/ - Custom action")

print("\nKey ViewSet Methods:")
print("  get_queryset() - Filter accessible objects")
print("  perform_create() - Add data before save")
print("  @action() - Add custom endpoints")

print("\nRouter Setup:")
print("  router = DefaultRouter()")
print("  router.register('transactions', TransactionViewSet)")
print("  urlpatterns = router.urls")
```
