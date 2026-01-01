---
type: "THEORY"
title: "Django Forms System"
---

Django forms handle rendering, validation, and processing of HTML forms. They protect against common attacks and reduce boilerplate code.

**Two Types of Forms:**

1. **Form** - Standalone form, not tied to a model
   ```python
   from django import forms
   
   class ContactForm(forms.Form):
       name = forms.CharField(max_length=100)
       email = forms.EmailField()
       message = forms.CharField(widget=forms.Textarea)
   ```

2. **ModelForm** - Automatically generated from a model
   ```python
   from django.forms import ModelForm
   from .models import Transaction
   
   class TransactionForm(ModelForm):
       class Meta:
           model = Transaction
           fields = ['amount', 'description', 'category', 'date']
   ```

**Common Field Types:**
- `CharField` - Text input
- `EmailField` - Email with validation
- `IntegerField` / `DecimalField` - Numbers
- `DateField` / `DateTimeField` - Dates
- `BooleanField` - Checkbox
- `ChoiceField` - Dropdown select
- `FileField` - File upload
- `ModelChoiceField` - Foreign key dropdown

**Field Options:**
- `required=False` - Optional field
- `label="Custom Label"` - Override label
- `initial=value` - Default value
- `help_text="..."` - Helper text
- `widget=forms.Textarea` - Custom widget
- `error_messages={...}` - Custom errors