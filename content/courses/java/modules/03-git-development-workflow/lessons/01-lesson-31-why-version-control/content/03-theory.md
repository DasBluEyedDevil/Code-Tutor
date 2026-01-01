---
type: "THEORY"
title: "Mental Model: Snapshots, Not Changes"
---

Unlike some older version control systems, Git thinks in SNAPSHOTS, not changes.

Imagine photographing your desk every hour:
- 9 AM: Photo shows laptop, coffee, notebook
- 10 AM: Photo shows laptop, coffee (notebook moved)
- 11 AM: Photo shows laptop, water, notebook (coffee replaced with water)

Each photo is a complete picture of your desk at that moment. Git works the same way - each 'commit' is a complete snapshot of your entire project.

Key Git concepts:

1. REPOSITORY (repo): Your project folder with Git tracking enabled
   - Contains all your code AND its complete history
   - The '.git' folder stores all the magic

2. COMMIT: A saved snapshot of your project at a point in time
   - Has a unique ID (like a fingerprint)
   - Includes a message describing what changed
   - Points to its parent commit(s)

3. WORKING DIRECTORY: Your current files that you're editing
   - What you see in your file explorer
   - Can be different from the last commit

4. STAGING AREA (Index): A preparation zone for your next commit
   - You choose which changes to include in the next snapshot
   - Gives you control over what gets saved