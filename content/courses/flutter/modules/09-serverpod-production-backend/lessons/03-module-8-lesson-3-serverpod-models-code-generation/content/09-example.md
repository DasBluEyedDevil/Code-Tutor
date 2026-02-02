---
type: "EXAMPLE"
title: "Enum Definitions"
---

Serverpod also supports enum types for fixed sets of values.

**File: my_project_server/protocol/user_role.yaml**


```yaml
# protocol/user_role.yaml
# An enum for user roles

enum: UserRole
values:
  - guest
  - user
  - moderator
  - admin

# This generates a Dart enum:
#
# enum UserRole with SerializableModel {
#   guest,
#   user,
#   moderator,
#   admin;
#
#   // Plus serialization methods
# }

# You can then use it in your models:
#
# class: User
# fields:
#   role: UserRole, default='UserRole.user'
```
