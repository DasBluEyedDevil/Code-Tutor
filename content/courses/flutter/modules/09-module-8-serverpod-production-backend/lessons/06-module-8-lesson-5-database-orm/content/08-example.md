---
type: "EXAMPLE"
title: "Creating a User Model"
---

Let us create a complete User model with database persistence:



```yaml
# lib/src/protocol/user.yaml

# Define the class and table
class: User
table: users

# Define all fields
fields:
  # The 'id' field is automatically added for database tables
  # You don't need to define it - Serverpod handles it
  
  # Required string fields
  email: String
  username: String
  
  # Optional fields use ? suffix
  displayName: String?
  avatarUrl: String?
  bio: String?
  
  # DateTime for timestamps
  createdAt: DateTime
  lastLoginAt: DateTime?
  
  # Boolean for flags
  isActive: bool
  isVerified: bool
  
  # Integer for counts
  postCount: int
  followerCount: int

# After running 'serverpod generate', you get:
# 1. A User class with all fields typed
# 2. User.db object with database methods
# 3. Serialization to/from JSON
# 4. Client-side User class for Flutter
```
