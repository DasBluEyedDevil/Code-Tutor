---
type: "EXAMPLE"
title: "Your First YAML Model Definition"
---

Let's create a User model. In Serverpod, models are defined in YAML files within the protocol/ folder.

**File: my_project_server/protocol/user.yaml**


```yaml
# protocol/user.yaml
# This YAML file defines the User model for Serverpod

class: User
table: users  # Creates a database table named 'users'
fields:
  # Primary key (id) is added automatically by Serverpod

  name: String
  # A required String field. Cannot be null.

  email: String
  # Another required String field.

  age: int?
  # An optional integer. The ? makes it nullable.

  isActive: bool
  # A required boolean field.

  createdAt: DateTime
  # Stores when the user was created.

  profileImageUrl: String?
  # Optional URL for profile image.

  role: String, default="'user'"
  # String with a default value. Note the nested quotes.

# After running 'serverpod generate', this creates:
# 1. Server model: lib/src/generated/user.dart
# 2. Client model: my_project_client/lib/src/protocol/user.dart
# 3. Database table: 'users' with all these columns
```
