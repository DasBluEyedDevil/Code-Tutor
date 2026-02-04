---
type: KEY_POINT
---

- Write all user actions to the local database first, then enqueue a sync operation that runs when the network is available
- Maintain a sync queue with operation type (create, update, delete), payload, and retry count for reliable background processing
- Resolve conflicts by comparing timestamps, showing a merge UI, or applying domain-specific rules (e.g., server wins for shared data)
- Use exponential backoff for failed sync attempts to avoid hammering the server when connectivity is intermittent
- Track `lastSyncedAt` per record to efficiently fetch only changes since the last successful sync (delta sync)
