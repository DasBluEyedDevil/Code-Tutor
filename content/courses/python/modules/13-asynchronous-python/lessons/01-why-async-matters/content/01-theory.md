---
type: "THEORY"
title: "Understanding Blocking vs Non-Blocking I/O"
---

**The Problem: Waiting Around**

Imagine you're a waiter at a restaurant. With **synchronous** (blocking) code:
- You take an order from table 1
- You walk to the kitchen and WAIT there until the food is ready
- Only then do you go to table 2
- Meanwhile, tables 3, 4, 5... are all waiting!

With **asynchronous** (non-blocking) code:
- You take an order from table 1, send it to kitchen
- While kitchen cooks, you take orders from table 2, 3, 4...
- When food is ready, you deliver it
- Everyone gets served faster!

**In programming terms:**
- **Blocking I/O**: Your program WAITS for network/file operations
- **Non-blocking I/O**: Your program does OTHER work while waiting

**Common blocking operations:**
- Network requests (API calls, downloading files)
- File I/O (reading/writing large files)
- Database queries
- User input

**The key insight:**
Most of your program's time is spent WAITING, not computing. Async lets you use that waiting time productively.