---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine transferring money between bank accounts:

Without transactions:
- Step 1: Subtract $100 from Account A
- Step 2: Add $100 to Account B
- If Step 2 fails... the money vanishes!
- Account A lost $100, Account B didn't get it
- Disaster!

With transactions (all-or-nothing):
- START TRANSACTION
- Step 1: Subtract $100 from Account A
- Step 2: Add $100 to Account B
- If BOTH succeed: COMMIT (make it permanent)
- If EITHER fails: ROLLBACK (undo everything)
- Money is NEVER lost!

Prisma transactions ensure that multiple database operations either ALL succeed together, or ALL fail together. This is called atomicity - one of the most important database concepts. Use transactions whenever you need multiple related operations to be treated as a single unit of work!