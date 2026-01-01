---
type: "EXAMPLE"
title: "Function-Based Views in Action"
---

**Building Transaction Views with FBVs**

Function-based views are straightforward: a function that takes a request and returns a response.

**Key Imports:**
- `render()` - Renders a template with context
- `get_object_or_404()` - Gets an object or returns 404
- `redirect()` - Redirects to another URL

**Common Decorators:**
- `@login_required` - Requires authentication
- `@require_http_methods(['GET', 'POST'])` - Limits HTTP methods
- `@csrf_exempt` - Disables CSRF protection (use carefully!)

**Request Object Properties:**
- `request.user` - Current logged-in user
- `request.method` - HTTP method (GET, POST, etc.)
- `request.POST` - Form data
- `request.GET` - Query parameters

```python
# views.py - Function-Based Views for Finance Tracker
from django.shortcuts import render, get_object_or_404, redirect
from django.http import HttpResponse
from django.contrib.auth.decorators import login_required
from django.views.decorators.http import require_http_methods
from .models import Transaction, Category
from .forms import TransactionForm


@login_required
def transaction_list(request):
    """Display all transactions for the current user."""
    transactions = Transaction.objects.filter(
        user=request.user
    ).order_by('-created_at')
    
    return render(request, 'transactions/list.html', {
        'transactions': transactions,
        'title': 'My Transactions'
    })


@login_required
def transaction_detail(request, pk):
    """Display a single transaction."""
    transaction = get_object_or_404(
        Transaction, 
        pk=pk, 
        user=request.user
    )
    
    return render(request, 'transactions/detail.html', {
        'transaction': transaction
    })


@login_required
@require_http_methods(['GET', 'POST'])
def transaction_create(request):
    """Create a new transaction."""
    if request.method == 'POST':
        form = TransactionForm(request.POST)
        if form.is_valid():
            transaction = form.save(commit=False)
            transaction.user = request.user
            transaction.save()
            return redirect('transaction-list')
    else:
        form = TransactionForm()
    
    return render(request, 'transactions/form.html', {
        'form': form,
        'title': 'New Transaction'
    })


@login_required
def transaction_delete(request, pk):
    """Delete a transaction."""
    transaction = get_object_or_404(
        Transaction, 
        pk=pk, 
        user=request.user
    )
    
    if request.method == 'POST':
        transaction.delete()
        return redirect('transaction-list')
    
    return render(request, 'transactions/confirm_delete.html', {
        'transaction': transaction
    })


# urls.py
from django.urls import path
from . import views

urlpatterns = [
    path('', views.transaction_list, name='transaction-list'),
    path('<int:pk>/', views.transaction_detail, name='transaction-detail'),
    path('new/', views.transaction_create, name='transaction-create'),
    path('<int:pk>/delete/', views.transaction_delete, name='transaction-delete'),
]


print("=== Function-Based Views ===")

print("\nView Pattern:")
print("  def view_name(request, *args, **kwargs):")
print("      # Process request")
print("      return render(request, 'template.html', context)")

print("\nKey Functions:")
print("  render()           - Render template with context")
print("  get_object_or_404  - Get object or return 404")
print("  redirect()         - Redirect to URL or view name")

print("\nCommon Decorators:")
print("  @login_required    - Require authentication")
print("  @require_http_methods - Limit HTTP methods")
```
