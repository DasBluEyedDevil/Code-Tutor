---
type: "KEY_POINT"
title: "URL Pattern Best Practices"
---

**RESTful URL Design:**
```python
# urls.py - Recommended patterns
urlpatterns = [
    # Collection endpoints
    path('transactions/', views.list, name='transaction_list'),
    path('transactions/create/', views.create, name='transaction_create'),
    
    # Instance endpoints
    path('transactions/<int:pk>/', views.detail, name='transaction_detail'),
    path('transactions/<int:pk>/edit/', views.update, name='transaction_update'),
    path('transactions/<int:pk>/delete/', views.delete, name='transaction_delete'),
    
    # Action endpoints
    path('transactions/<int:pk>/duplicate/', views.duplicate, name='transaction_duplicate'),
    
    # Filtered views
    path('transactions/category/<slug:category>/', views.by_category, name='by_category'),
]
```

**Path Converters:**
- `<int:pk>` - Matches integers
- `<str:name>` - Matches any string (except /)
- `<slug:slug>` - Matches slugs (letters, numbers, hyphens)
- `<uuid:id>` - Matches UUIDs
- `<path:file_path>` - Matches path including /

**Always Use Named URLs:**
```python
# In templates:
<a href="{% url 'transaction_detail' pk=transaction.id %}">View</a>

# In views:
from django.urls import reverse
return redirect(reverse('transaction_list'))
```