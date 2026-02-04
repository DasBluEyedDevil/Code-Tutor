---
type: KEY_POINT
---

- Offline-first means writing to local storage first, showing local data immediately, and syncing to the server in the background
- Last-Write-Wins is the simplest conflict resolution strategy but risks overwriting valid changes -- use it only for non-critical data like preferences
- CRDTs (Conflict-free Replicated Data Types) enable automatic merge without conflicts but add implementation complexity
- Design your data model with a `syncStatus` field (pending, synced, conflict) to track each record's synchronization state
- Offline-first improves perceived performance dramatically because the UI never waits for network round-trips
