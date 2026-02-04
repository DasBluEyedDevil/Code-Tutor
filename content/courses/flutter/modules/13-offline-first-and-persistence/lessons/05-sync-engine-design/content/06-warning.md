---
type: WARNING
---

**Offline edits can conflict with server state.** When two devices edit the same record offline, or when a user edits offline while someone else edits online, both changes arrive at the server simultaneously. Without a conflict resolution strategy, one set of changes is silently overwritten.

Common conflict scenarios:
- User edits a note on phone (offline), then edits the same note on tablet (online) -- phone syncs later and overwrites tablet changes
- Two users edit a shared document offline at the same time
- A deleted record on the server still has pending edits on the client

Design your sync engine with an explicit conflict resolution strategy from the start. Last-Write-Wins is acceptable for simple data (settings, preferences) but unacceptable for user-generated content where changes are meaningful. For important data, detect conflicts by comparing version numbers or timestamps and either merge automatically or show the user both versions to choose from.
