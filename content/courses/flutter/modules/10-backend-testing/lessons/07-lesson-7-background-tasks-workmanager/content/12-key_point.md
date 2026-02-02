---
type: "KEY_POINT"
title: "Answer Key"
---


**Answer 1:** C) 15 minutes

Android's WorkManager has a minimum periodic interval of **15 minutes**. You can request more frequent intervals, but the OS will enforce this minimum. This is to preserve battery life and prevent abuse.

**Answer 2:** B) For large uploads/downloads to save cellular data

`NetworkType.unmetered` means WiFi or unlimited data connections (not cellular metered data). Use this for large file operations to avoid expensive cellular data charges for users. For small API calls, `NetworkType.connected` (any connection) is fine.

**Answer 3:** B) Causes the task to retry with backoff policy

Returning `false` signals failure, and WorkManager will automatically retry the task according to the configured `backoffPolicy` (exponential or linear delay). Returning `true` means success - task won't retry.

