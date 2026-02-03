---
type: "KEY_POINT"
title: "Role-Based Authorization"
---

## Key Takeaways

- **Assign roles during user creation or via admin panel** -- `UserManager.AddToRoleAsync(user, "Admin")` assigns a role. `[Authorize(Roles = "Admin")]` restricts access to users with that role.

- **Keep the number of roles small (3-7)** -- too many roles become unmanageable. Common set: Admin, Manager, User, Guest. If you need more granularity, switch to claims or policies.

- **Roles can be combined** -- `[Authorize(Roles = "Admin,Manager")]` allows either role. Use policies when you need "Admin AND Manager" (both required) instead of "Admin OR Manager."
