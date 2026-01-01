---
type: "THEORY"
title: "What is a REST API?"
---

REST = Representational State Transfer

A REST API is a set of rules for building web services:

KEY PRINCIPLES:
1. Resources identified by URLs
2. HTTP methods define actions
3. Stateless (each request independent)
4. JSON for data exchange

EXAMPLE API:
GET    /api/users        → Get all users
GET    /api/users/123    → Get user with ID 123
POST   /api/users        → Create new user
PUT    /api/users/123    → Update user 123
DELETE /api/users/123    → Delete user 123