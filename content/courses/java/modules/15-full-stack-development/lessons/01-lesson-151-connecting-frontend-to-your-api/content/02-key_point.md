---
type: "KEY_POINT"
title: "Frontend is the Storefront, Backend is the Warehouse"
---

BACKEND (Your Java API):
= Warehouse
- Stores all products (data)
- Processes orders (business logic)
- Customers never see it directly

FRONTEND (HTML/JS/React):
= Storefront
- Beautiful display
- Shopping cart UI
- Checkout forms
- What customers interact with

They communicate:
Customer clicks "Buy" button (frontend)
→ Sends request to API (backend)
→ Backend processes order
→ Sends confirmation (frontend displays it)