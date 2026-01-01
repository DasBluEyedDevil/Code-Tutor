---
type: "THEORY"
title: "Real-World Partial Failures"
---

In production systems, partial success is often acceptable:

**Examples:**
- Sending notifications to 1000 users - 5 fail, 995 succeed
- Uploading 50 files - 2 timeout, 48 succeed
- Validating 20 fields - 3 have errors, 17 are valid

**Finance Tracker scenarios:**
- Syncing 5 bank accounts - 1 times out, 4 succeed
- Categorizing 100 transactions - 3 have invalid formats, 97 categorized
- Sending monthly reports to 10 recipients - 2 email bounces, 8 delivered

**The pattern:**
1. Attempt all operations
2. Collect successes and failures separately
3. Report both to the caller
4. Let the caller decide what to do

This is where ExceptionGroups truly shine.