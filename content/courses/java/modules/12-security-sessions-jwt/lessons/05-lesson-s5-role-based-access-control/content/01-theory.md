---
type: "THEORY"
title: "Beyond Simple Authentication"
---

Authentication answers: "Who are you?"
Authorization answers: "What can you do?"

SIMPLE AUTHORIZATION:
- Authenticated vs Anonymous
- All logged-in users have same access

ROLE-BASED ACCESS CONTROL (RBAC):
- Users have ROLES (Admin, Manager, User)
- Roles have PERMISSIONS
- Resources require specific roles

Example Organization:

Admin Role:
- Manage users
- Access all data
- System configuration

Manager Role:
- View reports
- Manage team members
- Edit department data

User Role:
- View own data
- Edit own profile
- Submit requests

Benefits:
- Principle of least privilege
- Easy to audit (who can do what)
- Simple to update (change role, not individual permissions)