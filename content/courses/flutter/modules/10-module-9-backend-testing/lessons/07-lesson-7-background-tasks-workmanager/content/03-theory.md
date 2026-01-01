---
type: "THEORY"
title: "Background Execution: The Challenges"
---


### Platform Restrictions

Modern mobile OSes heavily restrict background work to save battery:

**iOS:**
- ❌ No continuous background execution (with exceptions)
- ✅ BGTaskScheduler for periodic tasks
- ⏰ Tasks run at OS discretion (not guaranteed timing)
- 🔋 Tasks killed if battery is low

**Android:**
- ✅ WorkManager for reliable scheduled work
- ⏰ Minimum 15-minute intervals for periodic work
- 🔋 Doze mode limits background tasks
- ✅ More flexibility than iOS

**Key Takeaway:** Background tasks are **not real-time**. Use them for deferrable work, not time-critical operations!

