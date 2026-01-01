---
type: "EXAMPLE"
title: "User Protocol Definition"
---


**Creating the User Protocol**

Create the protocol file in your server's protocol directory:



```yaml
# server/lib/src/protocol/user_profile.yaml
class: UserProfile
table: user_profiles
fields:
  # Link to Serverpod's built-in user info
  userInfoId: int, relation(parent=serverpod_auth_server:UserInfo)
  
  # Profile information
  username: String
  displayName: String
  email: String
  avatarUrl: String?
  bio: String?
  
  # Status tracking
  isOnline: bool
  lastSeenAt: DateTime?
  
  # Account state
  isVerified: bool
  isDeleted: bool
  
  # Timestamps
  createdAt: DateTime
  updatedAt: DateTime?

indexes:
  user_profile_username_idx:
    fields: username
    unique: true
  user_profile_email_idx:
    fields: email
    unique: true
  user_profile_user_info_idx:
    fields: userInfoId
    unique: true

---

# server/lib/src/protocol/user_settings.yaml
class: UserSettings
table: user_settings
fields:
  # Owner reference
  userProfileId: int, relation(parent=user_profiles)
  
  # Notification preferences
  pushNotificationsEnabled: bool
  emailNotificationsEnabled: bool
  messagePreviewsEnabled: bool
  
  # Privacy settings
  showOnlineStatus: bool
  showLastSeen: bool
  allowDirectMessages: String  # 'everyone', 'friends', 'nobody'
  
  # App preferences
  theme: String  # 'light', 'dark', 'system'
  language: String
  
indexes:
  user_settings_profile_idx:
    fields: userProfileId
    unique: true
```
