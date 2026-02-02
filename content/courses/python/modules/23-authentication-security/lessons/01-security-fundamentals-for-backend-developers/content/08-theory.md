---
type: "THEORY"
title: "Principle of Least Privilege"
---

Every user, process, and system should have only the minimum permissions needed to perform its function:

**Database Users:**
- App user: SELECT, INSERT, UPDATE (no DELETE, no DROP)
- Admin user: Full access, used only for migrations
- Backup user: SELECT only

**File System:**
- Application runs as non-root user
- Write access only to specific directories
- No access to system files

**API Endpoints:**
- Regular users: Access own data only
- Moderators: Access flagged content
- Admins: Full access with audit logging

**In our Finance Tracker:**
```
USER ROLES:
- user: View/edit own transactions, accounts
- accountant: View reports (no edit)
- admin: Manage users, view audit logs
```