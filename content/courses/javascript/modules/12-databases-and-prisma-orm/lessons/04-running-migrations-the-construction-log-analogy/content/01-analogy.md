---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine you're building a house over several months:

Without a construction log:
- Workers forget what was done yesterday
- No record of foundation changes
- Can't rebuild if something breaks
- No way to share changes with other teams
- Every site does things differently

With a construction log (Prisma migrations):
- Detailed record of every change
- Day 1: Poured foundation
- Day 5: Built first floor walls
- Day 10: Added plumbing
- Any worker can see the full history
- Can replay changes to build identical houses

Prisma migrations are a construction log for your database. Each migration file records ONE specific change (like adding a table or field). These files:
1. Track database evolution over time
2. Can be replayed on other computers
3. Make it easy to update production databases
4. Keep your team synchronized

Instead of manually writing SQL to update your database, Prisma generates migration files automatically from your schema changes!