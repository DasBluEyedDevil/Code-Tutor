---
type: "WARNING"
title: "Common Routing Mistakes"
---

**1. Order Matters - More Specific First:**
```python
# BAD: 'create/' never matches because '<int:pk>/' catches it
urlpatterns = [
    path('<int:pk>/', views.detail),
    path('create/', views.create),  # Never reached!
]

# GOOD: Specific patterns before generic
urlpatterns = [
    path('create/', views.create),
    path('<int:pk>/', views.detail),
]
```

**2. Trailing Slashes - Be Consistent:**
```python
# Django default: trailing slashes
path('transactions/', ...)  # Matches /transactions/

# If you want both, add APPEND_SLASH = True (default)
```

**3. Hardcoded URLs - Use reverse():**
```python
# BAD: Hardcoded URL breaks if pattern changes
return redirect('/transactions/5/')

# GOOD: Use reverse with URL name
return redirect(reverse('transaction_detail', kwargs={'pk': 5}))
```