---
type: "THEORY"
title: "Ensuring Frontend and Backend Stay in Sync"
---


The fundamental challenge of distributed systems is keeping separate codebases synchronized. Your Flutter app and Dart backend are developed independently, often by different teams, with different release cycles.

**The Synchronization Problem:**

```
Flutter App (v2.3.1)          Backend API (v1.8.0)
     |                              |
     |--- expects User.email ------>|
     |                              |--- provides User.emailAddress
     |                              |
     X  MISMATCH = CRASH            X
```

**Traditional Approaches and Their Flaws:**

1. **Manual Documentation**: Markdown files or Confluence pages describing the API
   - Problem: Documentation drifts from reality
   - Problem: No automated enforcement

2. **Communication Channels**: Slack messages announcing API changes
   - Problem: Easy to miss notifications
   - Problem: No guarantee consumers updated

3. **End-to-End Testing**: Test the full stack together
   - Problem: Slow and expensive
   - Problem: Hard to pinpoint which side broke

**Contract Testing Approach:**

```
Flutter App                    Contract                    Backend API
     |                            |                            |
     |--- writes expectations --->|                            |
     |                            |<--- validates against -----|                            
     |                            |                            |
     |                      MISMATCH DETECTED                  |
     |                      (in CI, before deploy)             |
```

**Consumer-Driven Contracts:**

The most effective approach is **consumer-driven contracts** where the frontend (consumer) defines what it needs, and the backend (provider) proves it delivers that.

1. **Consumer writes contract**: Flutter app specifies expected request/response shapes
2. **Provider verifies contract**: Backend tests prove it satisfies all consumer contracts
3. **CI enforces**: Both sides run contract tests on every commit
4. **Deploy with confidence**: If contracts pass, integration will work

