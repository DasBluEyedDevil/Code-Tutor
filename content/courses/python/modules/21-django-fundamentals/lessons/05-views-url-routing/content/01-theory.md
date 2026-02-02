---
type: "THEORY"
title: "Function-Based vs Class-Based Views"
---

Django offers two approaches to writing views:

**Function-Based Views (FBV):**
```python
def transaction_list(request):
    transactions = Transaction.objects.all()
    return render(request, 'transactions/list.html', {'transactions': transactions})
```

**Class-Based Views (CBV):**
```python
class TransactionListView(ListView):
    model = Transaction
    template_name = 'transactions/list.html'
```

**When to Use Each:**

| Use FBV When | Use CBV When |
|--------------|-------------|
| Simple, custom logic | Standard CRUD operations |
| Learning Django | DRY across similar views |
| One-off endpoints | Need mixins/inheritance |
| Complex conditionals | Following REST patterns |

**Common Generic CBVs:**
- `ListView` - Display list of objects
- `DetailView` - Display single object
- `CreateView` - Form to create object
- `UpdateView` - Form to update object
- `DeleteView` - Confirm and delete object
- `TemplateView` - Render static template
- `RedirectView` - Redirect to another URL