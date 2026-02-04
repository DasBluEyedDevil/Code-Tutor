---
type: KEY_POINT
---

- Set up a Drift database with tables for notes and a sync queue to persist both content and pending operations
- Implement a repository pattern that reads from local database first, fetches from server when online, and caches responses locally
- Use `connectivity_plus` to detect network state changes and trigger sync automatically when connectivity returns
- Display sync status on each note (synced, pending, conflict) so users can see which changes have reached the server
- Test the full offline flow: create notes offline, verify they persist across app restarts, and confirm they sync when connectivity returns
