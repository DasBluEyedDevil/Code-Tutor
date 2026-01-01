---
type: "WARNING"
title: "Common Branching Mistakes"
---

MISTAKES TO AVOID:

1. WORKING DIRECTLY ON MAIN:
   Problem: Your incomplete work affects everyone
   Solution: Always create a feature branch for new work

2. LONG-LIVED FEATURE BRANCHES:
   Problem: The longer a branch lives, the harder it is to merge
   Solution: Keep branches short-lived (days, not weeks)
   Best practice: Merge to main at least once per week

3. NOT PULLING BEFORE BRANCHING:
   Problem: Your feature branch starts from outdated code
   Solution: Always pull latest main before creating a branch:
   $ git switch main
   $ git pull origin main
   $ git switch -c feature/new-feature

4. FORGETTING TO DELETE MERGED BRANCHES:
   Problem: Branch list becomes cluttered and confusing
   Solution: Delete branches immediately after merging:
   $ git branch -d feature/completed-work

5. MERGE CONFLICT PANIC:
   Problem: Seeing conflicts causes stress and mistakes
   Solution: Conflicts are normal! Take your time, understand both sides, test after resolving.

6. FORCE PUSHING SHARED BRANCHES:
   Problem: Overwrites teammates' work
   Solution: Never 'git push --force' on shared branches
   Only force push on your personal feature branches