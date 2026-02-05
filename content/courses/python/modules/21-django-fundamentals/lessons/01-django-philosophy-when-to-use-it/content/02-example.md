---
type: "EXAMPLE"
title: "Django vs FastAPI Comparison"
---

See how the same endpoint looks in both frameworks:

**Expected Output (conceptual):**
```
Django: Full-featured web framework with admin, ORM, templates
FastAPI: Lightweight, async-first API framework
```

```python
# FastAPI approach - minimal, explicit
from fastapi import FastAPI
from pydantic import BaseModel

app = FastAPI()

class Transaction(BaseModel):
    amount: float
    description: str

@app.post("/transactions/")
async def create_transaction(transaction: Transaction):
    # You must set up database, validation, auth yourself
    return {"id": 1, **transaction.model_dump()}


# Django approach - batteries included
# models.py
from django.db import models

class Transaction(models.Model):
    amount = models.DecimalField(max_digits=10, decimal_places=2)
    description = models.CharField(max_length=200)
    created_at = models.DateTimeField(auto_now_add=True)
    # Automatically gets: admin interface, migrations, 
    # form validation, CRUD operations

# views.py
from django.views.generic import CreateView
from .models import Transaction

class TransactionCreateView(CreateView):
    model = Transaction
    fields = ['amount', 'description']
    # Automatically handles: form rendering, validation,
    # CSRF protection, database save, redirect
```
