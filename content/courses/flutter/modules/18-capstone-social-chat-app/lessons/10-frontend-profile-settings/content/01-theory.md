---
type: "THEORY"
title: "Profile Architecture"
---


**User Profile Data Management**

A well-designed profile system requires careful consideration of data flow, caching strategies, and user experience. The architecture must handle both viewing and editing modes efficiently while maintaining consistency across the application.

**Profile State Management Layers**

| Layer | Responsibility | Provider Type |
|-------|---------------|---------------|
| **Current User** | Logged-in user's profile | `StateNotifier` |
| **Other Users** | Profiles viewed by current user | `FamilyAsyncNotifier` |
| **Edit State** | Temporary edits before save | `StateNotifier` |
| **Validation** | Real-time field validation | `Provider` |

**Edit Mode Patterns**

When implementing profile editing, isolate changes from the display state:

```
┌─────────────────────────────────────────────────────────────┐
│                  Profile Edit Flow                          │
├─────────────────────────────────────────────────────────────┤
│                                                             │
│   View Mode  ──>  Copy to Edit State                        │
│        │                 │                                  │
│        v                 v                                  │
│   Display    <──   Edit Fields (isolated)                   │
│   Original              │                                   │
│        │                 v                                  │
│        │          Validate Changes                          │
│        │                 │                                  │
│        v                 v                                  │
│   Cancel?    ──>  Discard Edit State                        │
│        │                                                    │
│        v                                                    │
│   Save?      ──>  API Call ──> Update Original              │
│                                                             │
└─────────────────────────────────────────────────────────────┘
```

**Image Upload Integration**

Profile picture updates require special handling:

| Step | Action | UI Feedback |
|------|--------|-------------|
| **Select** | Pick from gallery/camera | Show picker modal |
| **Preview** | Display local file | Overlay on current avatar |
| **Crop** | Optional image adjustment | Circular crop interface |
| **Upload** | Send to server | Progress indicator |
| **Confirm** | Update profile | Replace avatar |

**Settings Persistence Patterns**

Settings fall into two categories with different persistence strategies:

| Category | Storage | Sync Strategy |
|----------|---------|---------------|
| **Local Only** | SharedPreferences | Device-specific |
| **Synced** | Backend API | Cross-device |
| **Hybrid** | Both | Local + eventual sync |

**Local Settings Examples:**
- Theme preference (dark/light)
- Notification sounds
- Language preference
- Cache settings

**Synced Settings Examples:**
- Privacy settings
- Account visibility
- Notification preferences
- Blocked users

**Optimistic Updates**

For better UX, apply settings changes immediately while syncing in background:

1. Update local state instantly
2. Persist to local storage
3. Queue API sync
4. Handle sync failure gracefully
5. Retry on next app launch if needed

