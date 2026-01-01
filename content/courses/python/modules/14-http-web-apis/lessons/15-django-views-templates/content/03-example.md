---
type: "EXAMPLE"
title: "Class-Based Views - The Generic View Hierarchy"
---

**Django's Class-Based View Hierarchy**

Django provides a rich hierarchy of generic views that handle common patterns.

**Base Classes:**

```
View
  |-- TemplateView (renders a template)
  |-- RedirectView (redirects to URL)
  |
  |-- ListView (displays list of objects)
  |-- DetailView (displays single object)
  |
  |-- CreateView (form to create object)
  |-- UpdateView (form to update object)
  |-- DeleteView (confirms and deletes object)
  |
  |-- FormView (generic form handling)
```

**Key Attributes:**

- `model` - The Django model to work with
- `template_name` - Template to render
- `context_object_name` - Name for object(s) in template
- `queryset` - Custom queryset (alternative to model)
- `success_url` - Where to redirect after success
- `fields` - Form fields to include (CreateView/UpdateView)

**Key Methods to Override:**

- `get_queryset()` - Customize which objects to display
- `get_context_data()` - Add extra context to template
- `form_valid()` - Handle successful form submission
- `get_success_url()` - Dynamic success URL

```python
# views.py - Class-Based Views for Finance Tracker
from django.views.generic import (
    ListView, DetailView, 
    CreateView, UpdateView, DeleteView
)
from django.contrib.auth.mixins import LoginRequiredMixin
from django.urls import reverse_lazy
from .models import Transaction, Category


class TransactionListView(LoginRequiredMixin, ListView):
    """Display all transactions for the current user."""
    model = Transaction
    template_name = 'transactions/list.html'
    context_object_name = 'transactions'
    paginate_by = 20
    
    def get_queryset(self):
        """Only show current user's transactions."""
        return Transaction.objects.filter(
            user=self.request.user
        ).order_by('-created_at')
    
    def get_context_data(self, **kwargs):
        """Add extra context for the template."""
        context = super().get_context_data(**kwargs)
        context['title'] = 'My Transactions'
        context['total_count'] = self.get_queryset().count()
        return context


class TransactionDetailView(LoginRequiredMixin, DetailView):
    """Display a single transaction."""
    model = Transaction
    template_name = 'transactions/detail.html'
    context_object_name = 'transaction'
    
    def get_queryset(self):
        """Ensure user can only view their own transactions."""
        return Transaction.objects.filter(user=self.request.user)


class TransactionCreateView(LoginRequiredMixin, CreateView):
    """Create a new transaction."""
    model = Transaction
    fields = ['amount', 'category', 'description', 'transaction_type']
    template_name = 'transactions/form.html'
    success_url = reverse_lazy('transaction-list')
    
    def form_valid(self, form):
        """Set the user before saving."""
        form.instance.user = self.request.user
        return super().form_valid(form)
    
    def get_context_data(self, **kwargs):
        context = super().get_context_data(**kwargs)
        context['title'] = 'New Transaction'
        return context


class TransactionUpdateView(LoginRequiredMixin, UpdateView):
    """Update an existing transaction."""
    model = Transaction
    fields = ['amount', 'category', 'description', 'transaction_type']
    template_name = 'transactions/form.html'
    success_url = reverse_lazy('transaction-list')
    
    def get_queryset(self):
        """Ensure user can only edit their own transactions."""
        return Transaction.objects.filter(user=self.request.user)
    
    def get_context_data(self, **kwargs):
        context = super().get_context_data(**kwargs)
        context['title'] = 'Edit Transaction'
        return context


class TransactionDeleteView(LoginRequiredMixin, DeleteView):
    """Delete a transaction with confirmation."""
    model = Transaction
    template_name = 'transactions/confirm_delete.html'
    success_url = reverse_lazy('transaction-list')
    
    def get_queryset(self):
        """Ensure user can only delete their own transactions."""
        return Transaction.objects.filter(user=self.request.user)


# urls.py - Much cleaner with CBVs
from django.urls import path
from . import views

urlpatterns = [
    path('', 
         views.TransactionListView.as_view(), 
         name='transaction-list'),
    path('<int:pk>/', 
         views.TransactionDetailView.as_view(), 
         name='transaction-detail'),
    path('new/', 
         views.TransactionCreateView.as_view(), 
         name='transaction-create'),
    path('<int:pk>/edit/', 
         views.TransactionUpdateView.as_view(), 
         name='transaction-update'),
    path('<int:pk>/delete/', 
         views.TransactionDeleteView.as_view(), 
         name='transaction-delete'),
]


print("=== Class-Based Views ===")

print("\nGeneric Views Used:")
print("  ListView    - Display list of objects")
print("  DetailView  - Display single object")
print("  CreateView  - Form to create object")
print("  UpdateView  - Form to update object")
print("  DeleteView  - Confirm and delete")

print("\nKey Mixins:")
print("  LoginRequiredMixin - Require authentication")

print("\nCommon Overrides:")
print("  get_queryset()     - Filter accessible objects")
print("  get_context_data() - Add template context")
print("  form_valid()       - Handle form success")
```
