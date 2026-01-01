---
type: "THEORY"
title: "API Design for Finance Tracker"
---

**RESTful API Endpoints:**

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | /auth/register | Create new account |
| POST | /auth/login | Get access token |
| GET | /users/me | Get current user |
| GET | /transactions | List transactions |
| POST | /transactions | Create transaction |
| GET | /transactions/{id} | Get single transaction |
| PUT | /transactions/{id} | Update transaction |
| DELETE | /transactions/{id} | Delete transaction |
| GET | /transactions/summary | Get summary stats |
| GET | /categories | List categories |
| POST | /categories | Create category |
| GET | /budgets | List active budgets |
| POST | /budgets | Create budget |
| GET | /budgets/{id}/status | Check budget status |

**Authentication Flow:**
1. User registers with email/password
2. Password is hashed with bcrypt
3. Login returns JWT access token
4. Token sent in Authorization header
5. Protected routes validate token