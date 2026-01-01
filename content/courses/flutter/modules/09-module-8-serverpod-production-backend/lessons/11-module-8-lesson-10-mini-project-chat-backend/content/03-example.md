---
type: "EXAMPLE"
title: "Step 1: Define the User Model"
---

Let us start by defining our User model. This extends Serverpod's authentication system with chat-specific fields.



```yaml
// File: lib/src/protocol/chat_user.yaml
// This defines the ChatUser model with all user-related fields

# Chat user profile extending base authentication
class: ChatUser
table: chat_users
fields:
  # Link to Serverpod's built-in auth user
  userInfoId: int
  
  # Display information
  username: String
  displayName: String?
  avatarUrl: String?
  bio: String?
  
  # Status tracking
  isOnline: bool
  lastSeenAt: DateTime?
  
  # Metadata
  createdAt: DateTime

indexes:
  # Fast lookup by userInfoId (from auth system)
  chat_user_user_info_idx:
    fields: userInfoId
    unique: true
  
  # Fast username search
  chat_user_username_idx:
    fields: username
    unique: true

# Run: serverpod generate
# Then: serverpod create-migration
# Then: dart run bin/main.dart --apply-migrations
```
