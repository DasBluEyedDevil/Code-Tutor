---
type: "KEY_POINT"
title: "FBV vs CBV - Quick Reference"
---

**Function-Based View Pattern:**
```python
@login_required
def my_view(request, pk):
    obj = get_object_or_404(Model, pk=pk)
    return render(request, 'template.html', {'obj': obj})
```

**Class-Based View Pattern:**
```python
class MyView(LoginRequiredMixin, DetailView):
    model = Model
    template_name = 'template.html'
    context_object_name = 'obj'
```

**Choosing Between Them:**

- **FBV:** Full control, simple logic, unique behavior
- **CBV:** CRUD operations, inheritance, DRY code

**Pro Tip:** Django REST Framework uses CBVs extensively, so learning them prepares you for API development.