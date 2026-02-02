---
type: "THEORY"
title: "Domain-Driven Design for Finance"
---

Our Finance Tracker has four core domain entities:

**1. User** - Account holder
- id, email, hashed_password
- created_at, updated_at
- Owns transactions, categories, budgets

**2. Category** - Transaction classification
- id, name, type (income/expense), icon
- user_id (categories are per-user)
- Built-in vs custom categories

**3. Transaction** - Money in or out
- id, amount, description, date
- category_id, user_id
- type derived from category

**4. Budget** - Spending limits
- id, category_id, amount_limit, period
- user_id, start_date, end_date
- Tracks spending against limit