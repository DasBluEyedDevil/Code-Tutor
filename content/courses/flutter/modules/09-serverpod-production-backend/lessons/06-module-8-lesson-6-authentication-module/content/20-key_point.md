---
type: "KEY_POINT"
title: "Authentication vs Authorization"
---

These two concepts are often confused but are fundamentally different:

**Authentication (AuthN):**
Verifying WHO someone is.
- Is this person who they claim to be?
- Done through passwords, OAuth, biometrics
- Results in: User is signed in or not

**Authorization (AuthZ):**
Verifying WHAT someone can do.
- Can this user perform this action?
- Done through roles, permissions, policies
- Results in: User is allowed or denied

**Serverpod's Auth Module Handles:**
- Authentication (sign in, sign out, sessions)
- Basic user info (id, email, name, image)

**You Need to Implement:**
- Authorization (roles, permissions)
- Business rules (who can edit what)
- Resource ownership (users can only edit their own posts)

For authorization, you typically create a separate roles/permissions system that checks user capabilities before allowing actions.

