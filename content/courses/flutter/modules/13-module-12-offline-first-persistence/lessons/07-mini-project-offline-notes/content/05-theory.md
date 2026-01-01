---
type: "THEORY"
title: "Project Completion Checklist"
---


**Core Features:**
- [ ] Create notes offline
- [ ] Edit notes offline
- [ ] Delete notes offline
- [ ] View notes without network
- [ ] Sync when connection available

**Sync Features:**
- [ ] Sync queue persists across app restarts
- [ ] Retry failed syncs with backoff
- [ ] Handle conflicts (last-write-wins or merge)
- [ ] Show sync status per note

**UI Features:**
- [ ] Offline banner when disconnected
- [ ] Sync progress indicator
- [ ] Visual distinction for pending items
- [ ] Retry button for failed syncs

**Testing:**
- [ ] Unit tests for database operations
- [ ] Unit tests for sync logic
- [ ] Integration tests for offline scenarios
- [ ] Test conflict resolution

**Bonus Challenges:**
- Add full-text search with Isar
- Implement collaborative editing with CRDTs
- Add attachment support with offline caching
- Build a web version with IndexedDB

