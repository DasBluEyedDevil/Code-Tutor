---
type: "THEORY"
title: "User Model Design"
---


**User Model Requirements**

The User model is the foundation of our application. It must support:

- **Authentication**: Email, password hash, verification status
- **Profile**: Display name, username, avatar, bio
- **Status**: Online/offline, last seen timestamp
- **Settings**: Notification preferences, privacy settings
- **Relationships**: Friends, blocked users

**Serverpod Auth Integration**

Serverpod provides a built-in authentication module (`serverpod_auth`) that handles:

- User sessions and tokens
- Password hashing
- Email verification
- Social login (Google, Apple, etc.)

We extend this with our own `UserProfile` model for app-specific data.

**Field Considerations**

| Field | Type | Purpose | Constraints |
|-------|------|---------|-------------|
| `id` | int | Primary key | Auto-generated |
| `userInfoId` | int | Links to serverpod_auth | Foreign key, unique |
| `username` | String | Unique identifier | 3-30 chars, alphanumeric |
| `displayName` | String | Shown in UI | 1-50 chars |
| `email` | String | Contact/login | Valid email format |
| `avatarUrl` | String? | Profile picture | Optional, valid URL |
| `bio` | String? | User description | Max 500 chars |
| `isOnline` | bool | Current status | Default false |
| `lastSeenAt` | DateTime? | Last activity | Updated on disconnect |
| `isVerified` | bool | Email verified | Default false |
| `createdAt` | DateTime | Registration date | Auto-set |

