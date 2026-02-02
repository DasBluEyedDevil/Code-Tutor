---
type: "WARNING"
title: "Form Validation Security"
---

**Never Trust Client-Side Validation Alone:**
```python
# BAD: Only checking in JavaScript
# Attacker can disable JS or use curl

# GOOD: Always validate server-side
def create_transaction(request):
    form = TransactionForm(request.POST)
    if form.is_valid():  # Server-side validation!
        form.save()
```

**Dangerous Patterns:**

1. **Mass Assignment Vulnerability:**
   ```python
   # BAD: Allows setting any field including is_admin!
   User.objects.create(**request.POST)
   
   # GOOD: Explicit fields via ModelForm
   class UserForm(ModelForm):
       class Meta:
           model = User
           fields = ['username', 'email']  # Whitelist only!
   ```

2. **SQL Injection via raw queries:**
   ```python
   # BAD: User input in raw SQL
   User.objects.raw(f"SELECT * WHERE name='{name}'")
   
   # GOOD: Parameterized queries
   User.objects.raw("SELECT * WHERE name=%s", [name])
   ```

3. **Forgetting to validate file uploads:**
   ```python
   # Always validate file type, size, and content
   def clean_file(self):
       f = self.cleaned_data['file']
       if f.size > 5 * 1024 * 1024:  # 5MB limit
           raise ValidationError("File too large")
       if not f.name.endswith(('.pdf', '.jpg', '.png')):
           raise ValidationError("Invalid file type")
       return f
   ```