---
type: "THEORY"
title: "Local-First Data Patterns"
---


**The Local-First Approach:**

1. **Write to local storage first** - User actions immediately update local database
2. **Show local data immediately** - UI reflects local state, not server state
3. **Sync in background** - Push changes to server when network available
4. **Handle conflicts gracefully** - Merge remote changes with local changes

**Data Flow:**
```
User Action -> Local DB -> UI Update -> Background Sync -> Server
                                              |
                                              v
                                        Conflict Resolution
```

**Key Principles:**
- Local database is the source of truth for UI
- Server is the source of truth for sync
- Changes are queued when offline
- Sync status is visible to users

