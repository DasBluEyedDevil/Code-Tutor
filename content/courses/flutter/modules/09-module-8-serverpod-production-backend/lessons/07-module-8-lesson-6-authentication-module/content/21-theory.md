---
type: "THEORY"
title: "User Info and Profile Management"
---

Serverpod's auth module provides a UserInfo class that stores basic user information. For most apps, you will want to extend this with additional profile fields.

**Built-in UserInfo Fields:**

- `id`: Unique user identifier (int)
- `userIdentifier`: External identifier (e.g., from Google)
- `userName`: Display name
- `fullName`: Full legal name
- `email`: Email address
- `imageUrl`: Profile picture URL
- `created`: Account creation timestamp
- `scopeNames`: List of permission scopes

**Extending User Profiles:**

For additional fields (bio, location, preferences), create a separate model that references the user. This approach keeps the auth module's user table clean while allowing unlimited custom fields.

