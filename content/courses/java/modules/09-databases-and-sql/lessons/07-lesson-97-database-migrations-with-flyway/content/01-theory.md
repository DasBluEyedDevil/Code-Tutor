---
type: "THEORY"
title: "The Problem: Schema Changes in Production"
---

Your app works locally. You deploy to production.

Then you need to add a new column:

ALTER TABLE users ADD COLUMN phone VARCHAR(20);

QUESTIONS:
- Did you run this on production?
- Did you run it on staging?
- What about your teammate's database?
- What about the test environment?

PROBLEMS:
- Manual SQL execution = human error
- No record of what changed when
- Different environments get out of sync
- Rollbacks are a nightmare

You version control your CODE. Why not your DATABASE?