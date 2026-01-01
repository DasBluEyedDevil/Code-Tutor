---
type: "THEORY"
title: "When to Use Each"
---

**Use Promise.try() when:**
- Calling functions that might throw synchronously
- Normalizing sync/async function returns
- Building robust error handling chains

**Use Promise.withResolvers() when:**
- Building event-to-promise adapters
- Creating deferred/lazy promises
- Passing resolve/reject to callbacks or other functions
- Implementing timeout patterns