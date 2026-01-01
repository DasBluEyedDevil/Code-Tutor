---
type: "EXAMPLE"
title: "Django's Batteries-Included Philosophy"
---

**What You Get Out of the Box:**

Django follows the "batteries-included" philosophy - everything you need is built-in.

**1. Admin Interface (Auto-generated CRUD)**
```python
# Just register your model
from django.contrib import admin
from .models import Product

admin.site.register(Product)
# Now you have a full admin UI at /admin/
```

**2. ORM (Simpler than SQLAlchemy)**
```python
# Define model
class Product(models.Model):
    name = models.CharField(max_length=100)
    price = models.DecimalField(max_digits=10, decimal_places=2)

# Query (no session management needed)
Product.objects.filter(price__lt=50).order_by('name')
```

**3. Built-in Authentication**
```python
from django.contrib.auth.decorators import login_required

@login_required
def dashboard(request):
    return render(request, 'dashboard.html')
```

**4. Forms and Validation**
```python
from django import forms

class ProductForm(forms.ModelForm):
    class Meta:
        model = Product
        fields = ['name', 'price']
# Auto-validates, auto-generates HTML
```

**5. Template Engine**
```html
{% for product in products %}
  <h2>{{ product.name }}</h2>
  <p>${{ product.price }}</p>
{% endfor %}
```

**6. Security Features**
- CSRF protection (automatic)
- XSS protection (auto-escaped templates)
- SQL injection prevention (ORM)
- Clickjacking protection
- Password hashing (bcrypt by default)