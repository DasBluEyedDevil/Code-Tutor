---
type: KEY_POINT
---

- Optimistic UI updates the interface immediately based on expected success, then confirms or rolls back after the server responds
- Store the pre-update state before applying the optimistic change so you can restore it cleanly if the operation fails
- Show sync status indicators (checkmark for synced, spinner for pending, warning for failed) on each item so users know what is confirmed
- Most operations succeed (>99%), so optimistic updates make the app feel instant while the rare failure triggers a graceful rollback
- Combine optimistic UI with a local database: write locally, update UI, sync in background, rollback only on confirmed server rejection
